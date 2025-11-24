public interface IParameterizedState<TParams>
{
    void Inject(TParams parameters);
}