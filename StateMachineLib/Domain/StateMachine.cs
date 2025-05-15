namespace StateMachineLib.Domain;

public class StateMachine
{
    private readonly Dictionary<string, object> _transitions = [];

    public StateMachine Configure<S,T>(Transition<S,T> transition) where S : IState where T : ITrigger
    {
        var key = $"{typeof(S)}&{typeof(T)}";
        _transitions[key] = transition;
        return this;
    }

    public IState Handle<S,T>(S state, T trigger) where S : IState where T : ITrigger
    {
        var key = $"{typeof(S)}&{typeof(T)}";
        if (_transitions.TryGetValue(key, out var transition))
            return ((Transition<S, T>)transition).Invoke(state, trigger);
        
        return state;
    }
}