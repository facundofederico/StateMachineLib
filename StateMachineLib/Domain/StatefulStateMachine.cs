using System.Reflection;

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
        var concreteType = State.GetType();
        MethodInfo genericMethod = typeof(StateMachine)
            .GetMethod(nameof(StateMachine.Handle))!
            .MakeGenericMethod(concreteType, typeof(T));

        State = (IState)genericMethod.Invoke(_stateMachine, [State, trigger])!;
        return this;
    }
}