# Модуль Worker

Отвечает за поведение рабочего: получение задания, перемещение, выполнение действия, возврат.
Рабочий умеет собирать ресурсы, строить здания и выполнять ручные команды игрока.

---

## Компоненты и их роли

```
WorkerSpawner             — создаёт Workers из пула (PoolBase<Worker>)
    └─ Worker             — корневой MonoBehaviour; содержит FSM, WorkContext, схему переходов
        ├─ JobRunner      — получает задания из JobBoard, вызывает Worker.ExecuteJob(plan)
        ├─ WorkerStateMachine  — регистрирует состояния, тикает через StateMachineBase
        │   ├─ WorkerIdleState
        │   ├─ MoveToResourceState   — цель: context.Resource.Position
        │   ├─ ReturnToBaseState     — цель: context.Building.GetLandingPoint(pos)
        │   ├─ MoveToPointState      — цель: context.ManualTarget (ручной режим)
        │   ├─ WorkerGrabState       — вызывает context.Resource.Collect → FireSignal(Collected)
        │   └─ WorkerDeliverState    — вызывает context.Building.TakeResource → FireSignal(Delivered)
        ├─ WorkContext    — общий контекст: Resource, Building, ManualTarget
        └─ ResourceHolder — Transform-якорь для удержания ресурса
```

---

## Классический FSM: как работают переходы

Состояния **не знают друг о друге**. Когда состояние завершается, оно фаерит `WorkerSignal`:

```
WorkerGrabState.OnEnter()
    → context.Resource.Collect(holder, () => Machine.FireSignal(Collected))

StateMachineBase.FireSignal(Collected)
    → смотрит в TransitionScheme: (Grab, Collected) → ReturnToBase
    → ChangeState(ReturnToBase)
```

---

## Жизненный цикл задания (GatheringJob)

```
JobBoard.Changed
    └─> JobRunner.TryGetJob()
        └─> JobRunner.AssignJob(GatheringJob)
            └─> Worker.ExecuteJob(plan)
                ├─ plan.Configure(context, scheme)  — заполняет WorkContext + TransitionScheme
                ├─ stateMachine.LoadScheme(scheme)
                └─ stateMachine.ChangeState(MoveToResource)

Схема переходов (GatheringJob):
    MoveToResource + Arrived   → Grab
    Grab           + Collected → ReturnToBase
    ReturnToBase   + Arrived   → Deliver
    Deliver        + Delivered → Idle

Idle → Worker.BecameIdle → JobRunner берёт следующее задание
```

---

## Ручной режим

Схема переходов очищается — состояние выполняется один раз, затем Worker уходит в Idle:

```csharp
worker.MoveToPoint(position);   // идёт к точке → Idle
worker.GrabResource(resource);  // подбирает → Idle
worker.ReturnToBase();          // возвращается → Idle
```

---

## Добавление нового типа задания

1. Создать класс `XxxJob : IJob, IJobPlan`
2. Реализовать `Configure(WorkContext, TransitionScheme)` — заполнить контекст и схему
3. Реализовать `EntryState` — начальное состояние
4. **Brain не трогается. Worker не трогается. Состояния не трогаются.**

---

## Зависимости модуля

| Зависимость | Откуда |
|---|---|
| `StateMachineBase`, `StateBase`, `IState` | `Assets/Scripts/Core/StateMachine` |
| `TransitionScheme` | `Assets/Scripts/Core/StateMachine` |
| `JobRunner`, `JobBoard`, `IJob`, `IJobPlan` | `Assets/Scripts/Jobs` |
| `SplinePath`, `SplineContainer` | `Assets/Scripts/Splines` / External |
| `MainBuilding`, `ICollectable`, `Resource` | `Assets/Scripts/MainBuilding`, `Assets/Scripts/Resource` |
| `PoolBase<T>`, `IPoolable<T>` | `Assets/Scripts/Pool` |
