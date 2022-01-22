using UnityEngine;
using System.Collections;
public class StateMachine : MonoBehaviour
{
    public virtual State CurrentState
    {
        get { return _currentState; }
        set { Transition(value); }
    }
    protected State _currentState;
    protected bool _inTransition;

    //get the current state
    public virtual T GetState<T>() where T : State
    {
        T target = GetComponent<T>();
        if (target == null)
            target = gameObject.AddComponent<T>();
        return target;
    }

    //can tell the state machine to change state
    public virtual void ChangeState<T>() where T : State
    {
        CurrentState = GetState<T>();
    }

    //keep track of transitions and disallow extra state changes during transitions between states
    protected virtual void Transition(State value)
    {
        if (_currentState == value || _inTransition)
            return;
        _inTransition = true;

        if (_currentState != null)
            _currentState.Exit();

        _currentState = value;

        if (_currentState != null)
            _currentState.Enter();

        _inTransition = false;
    }
}