namespace StateMachineLib.Domain;

public class StateMachineWithHistory : StateMachine
{
    private record TransitionEvent(DateTime Timestamp, IState OriginalState, ITrigger Trigger, IState NewState);
    private readonly List<TransitionEvent> events = [];

    public override IState Handle<S, T>(S state, T trigger)
    {
        var result = base.Handle(state, trigger);

        if (!ReferenceEquals(state, result))
            events.Add(new TransitionEvent(DateTime.UtcNow, state, trigger, result));

        return result;
    }
}