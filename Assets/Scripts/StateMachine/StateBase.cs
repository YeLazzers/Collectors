using System;

public abstract class StateBase : IState
{
    protected StateMachineBase ParentMachine;
    public StateBase(StateMachineBase machine)
    {
        ParentMachine = machine;
    }

    public abstract void OnEnter(Action onComplete);
    public abstract void OnUpdate(float deltaTime);
    public abstract void OnExit();
}