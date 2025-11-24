using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineBase : MonoBehaviour
{
    private Dictionary<Enum, IState> _states = new();
    private IState _currentState;
    private Enum _defaultStateId;

    private void Update()
    {
        _currentState.OnUpdate(Time.deltaTime);
    }

    public delegate void StateCallback();

    public void RegisterState(Enum id, IState state)
    {
        _states[id] = state;
    }
    public void Init(Enum defaultStateId, Enum initialStateId = null)
    {
        _defaultStateId = defaultStateId;

        ChangeState(initialStateId == null ? defaultStateId : initialStateId);
    }

    public void ChangeState(Enum id, Action onComplete = null)
    {
        _currentState?.OnExit();


        _currentState = _states[id];
        _currentState.OnEnter(() =>
        {
            if (onComplete == null)
                ChangeStateToDefault();
            else
                onComplete?.Invoke();
        });
    }
    public void ChangeState<TParams>(Enum id, TParams param, Action onComplete = null)
    {
        if (_states[id] is IParameterizedState<TParams> paramState)
        {
            paramState.Inject(param);
        }
        else
        {
            ChangeStateToDefault();
            throw new Exception(
                $"State '{id}' does not support parameters '{typeof(TParams)}'"
            );
        }

        ChangeState(id, onComplete);
    }

    public void ChangeStateToDefault()
    {
        ChangeState(_defaultStateId);
    }
}