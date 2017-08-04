﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine <T> {
    private T Owner;
    public FSMState<T> CurrentState;
    private FSMState<T> PreviousState;
    private FSMState<T> GlobalState;

    public void Awake()
    {
        CurrentState = null;
        PreviousState = null;
        GlobalState = null;
    }

    public void Configure(T owner, FSMState<T> InitialState)
    {
        Owner = owner;
        ChangeState(InitialState);
    }

    public void Update()
    {
        if (GlobalState != null) GlobalState.Execute(Owner);
        if (CurrentState != null) CurrentState.Execute(Owner);
    }

    public void ChangeState(FSMState<T> NewState)
    {
        PreviousState = CurrentState;
        if (CurrentState != null)
            CurrentState.Exit(Owner);
        CurrentState = NewState;
        if (CurrentState != null)
            CurrentState.Enter(Owner);
    }

    public void RevertToPreviousState()
    {
        if (PreviousState != null)
            ChangeState(PreviousState);
    }
};
