namespace StateMachineLib.Domain;

public class StatefulStateMachine(IState state)
{
    private readonly StateMachine _stateMachine = new();
    public IState State { get; private set; } = state;

    public StatefulStateMachine Configure<S,T>(Transition<S,T> transition) 
        where S : IState 
        where T : ITrigger
    {
        _stateMachine.Configure(transition);
        return this;
    }

    public StatefulStateMachine Handle<T>(T trigger)
        where T : ITrigger
    {
        var result = _stateMachine.Handle(State, trigger);
        State = result;
        return this;
    }
}