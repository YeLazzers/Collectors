using System;
using System.Collections.Generic;

public class TransitionScheme
{
    private readonly Dictionary<(Enum state, Enum signal), Enum> _transitions = new();

    public TransitionScheme Add(Enum fromState, Enum signal, Enum toState)
    {
        _transitions[(fromState, signal)] = toState;
        return this;
    }

    public bool TryResolve(Enum currentState, Enum signal, out Enum nextState)
    {
        return _transitions.TryGetValue((currentState, signal), out nextState);
    }

    public void Clear()
    {
        _transitions.Clear();
    }
}
