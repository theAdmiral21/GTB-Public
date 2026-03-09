# AI StateMachine

    After writing this, I am considering forcing context implementations to create the state and transition list. That way when you want to use a state machine all you need to do is instantiate the context you want, possibly from a factory, and then instantiate your state machine with the context. How would that work if the states need a context in order to be instantiated? <.<

In an effort to develop a simple reusable behavior system for in game entities I have written a basic StateMachine. This StateMachine implements the `IStateMachine<T>` interface.

## StateMachine
`IStateMachine<T>` contains methods for entering/exiting/updating a state, a method for changing states, a property returning the current state, and an event for when a state completes.
```c#
    public interface IStateMachine<T>
    {
        public IState<T> CurrentState { get; }
        public event Action<IState<T>> OnStateComplete;
        public void ChangeState(IState<T> newState, T context);
        public void Enter(T context);
        public void Update(T context);
        public void Exit(T context);
    }
```
The state machine is responsible for changing between states, evaluating transitions, and updating the current state. The state machine is configured on a per-context basis. That means when setting up a state machine for a given entity you must define the context, state list, and transition list that define the behavior of the state machine.

Objects with state machines can be unique. They can have different states, contexts, and transitions. This makes the state machine very flexible. 

## States
All states within a given state machine implement the `IState<T>` class.
```c#
    public interface IState<T>
    {
        public StateType State { get; }
        public void Enter(T context);
        public void Update(T context);
        public void Exit(T context);
    }
```
States define what happens when you transition into and out of a given state as well as what happens while a given state is active.

## Contexts
In order to reuse as much of the state machine as possible, the state machine and its states implement generics (`<T>`). In practice this generic is replaced with a **context**. Contexts in this case, are classes that implement the required interfaces found in the concrete implementations of `IState<T>`. 

For example: if create an instance of a state machine with just the `IdleState<T>`, whatever context I create to customize the behavior must implement the `ITimerContext` interface.
```c#
public class IdleState<T> : IState<T> where T : ITimerContext
```
This gradually becomes more complicated more states that are added. This is because the context you implement must inherit from all of the interfaces used within the state machine.

For example if I had a state machine with two states:
```c#
public class IdleState<T> : IState<T> where T : ITimerContext

public class MoveToState<T> : IState<T> where T : IPositionContext
```
Then whatever context I create to interface with those states must implement `ITimerContext` and `IPositionContext`.
```c#
public class PlatformContext : IPositionContext, ITimerContext, IPassengerContext
```
Only writing the context can be very powerful though. By allowing any context to be used within the state machine, I can limit the number of unique states I need to implement because the context handles all of the timing and movement details while the state just calls the methods within the context in Enter, Exit, and Update.

Essentially, the state defines the minimum implementation requirements to use said state and the context decides *how* to implement those requirements.

## Transitions
Finally, in addition to states and contexts, the state machine requires a list of `ITransition<T>` as well. `ITransition<T>` contains properties defining which states it can change between, what direction it changes in, and finally a method to evaluate if the transition is true.
```c#
    public interface ITransition<T>
    {
        public IState<T> ToState { get; }
        public IState<T> FromState { get; }
        public bool EvaluateTransition(T context);
    }
```
Transitions are just conditions that monitor the current context and raise `true` if the state machine should change *from* the current state *to* the new state.
```c#
        public void Update(T context)
        {
            _currentState.Update(context);
            
            // Check if we should change from the current state to another state
            foreach (var transition in _transitions)
            {
                if (CurrentState == transition.FromState)
                {
                    bool res = transition.EvaluateTransition(context);
                    if (res)
                    {
                        OnStateComplete?.Invoke(CurrentState);
                        ChangeState(transition.ToState, context);
                        break;
                    }
                }
            }
        }
```
