using System;

public interface IState
{
    void OnEnter(Action onComplete);
    void OnUpdate(float deltaTime);
    void OnExit();
}