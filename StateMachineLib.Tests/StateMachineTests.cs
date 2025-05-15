using StateMachineLib.Domain;

namespace StateMachineLib.Tests;

public class StateMachineTests
{
    [Fact]
    public void StateMachine_HandleValidTrigger_ShouldTransition()
    {
        // Arrange
        var state = new ConcreteState();
        var trigger = new ConcreteTrigger();
        var stateMachine = new StateMachine();
        stateMachine.Configure((ConcreteState s, ConcreteTrigger t) => new ConcreteState());

        // Act
        var newState = stateMachine.Handle(state, trigger);

        // Assert
        Assert.Equal(state.GetType(), newState.GetType());
        Assert.False(ReferenceEquals(state, newState));
    }
    
    [Fact]
    public void StateMachine_HandleInvalidTrigger_ShouldNotTransition()
    {
        // Arrange
        var state = new ConcreteState();
        var trigger = new ConcreteTrigger();
        var stateMachine = new StateMachine();

        // Act
        var newState = stateMachine.Handle(state, trigger);

        // Assert
        Assert.Equal(state.GetType(), newState.GetType());
        Assert.True(ReferenceEquals(state, newState));
    }

    private class ConcreteState : IState;
    private class ConcreteTrigger : ITrigger;
}