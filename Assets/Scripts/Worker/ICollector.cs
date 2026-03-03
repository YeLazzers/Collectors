using DG.Tweening;

public interface ICollector
{
    void Collect(ICollectable collectable, TweenCallback onComplete = null);
}
