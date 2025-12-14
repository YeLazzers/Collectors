public interface IHoverable
{
    string Name { get; }

    void OnHoverEnter();
    void OnHoverExit();
}