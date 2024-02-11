using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private readonly List<IState> _states = new();
    private IState _activeState;

    public void Register(IState state)
    {
        if (_states.Contains(state))
            return;

        _states.Add(state);
    }

    public void Update()
    {
        _activeState.OnUpdate();
    }
}
