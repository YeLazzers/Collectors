public abstract class StateBase : IState
{
    protected StateMachineBase Machine;

    protected StateBase(StateMachineBase machine)
    {
        Machine = machine;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate(float deltaTime);
    public abstract void OnExit();
}
