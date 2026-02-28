using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineBase : MonoBehaviour
{
    private readonly Dictionary<Enum, IState> _states = new();

    private IState _currentState;
    private Enum _currentStateId;
    private Enum _defaultStateId;
    private TransitionScheme _scheme;

    public event Action<Enum> StateEntered;

    private void Update()
    {
        _currentState?.OnUpdate(Time.deltaTime);
    }

    public void RegisterState(Enum id, IState state)
    {
        _states[id] = state;
    }

    public void Init(Enum defaultStateId, Enum initialStateId = null)
    {
        _defaultStateId = defaultStateId;
        ChangeState(initialStateId ?? defaultStateId);
    }

    public void LoadScheme(TransitionScheme scheme)
    {
        _scheme = scheme;
    }

    public void ClearScheme()
    {
        _scheme = null;
    }

    public void ChangeState(Enum id)
    {
        _currentState?.OnExit();
        _currentStateId = id;
        _currentState = _states[id];
        _currentState.OnEnter();
        StateEntered?.Invoke(id);
    }

    public void ChangeStateToDefault()
    {
        ChangeState(_defaultStateId);
    }

    public void FireSignal(Enum signal)
    {
        if (_scheme != null && _scheme.TryResolve(_currentStateId, signal, out Enum nextId))
        {
            ChangeState(nextId);
            return;
        }

        ChangeState(_defaultStateId);
    }
}
