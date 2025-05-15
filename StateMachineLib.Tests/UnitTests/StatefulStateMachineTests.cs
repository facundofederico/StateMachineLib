using StateMachineLib.Domain;

namespace StateMachineLib.Tests.UnitTests;

public class StatefulStateMachineTests
{
    [Fact]
    public void StatefulStateMachine()
    {
        // Arrange
        var stateMachine = new StatefulStateMachine(new ConcreteState1());
        stateMachine
            .Configure((ConcreteState1 s, Advance t) => new ConcreteState2())
            .Configure((ConcreteState2 s, Advance t) => new ConcreteState3())
            .Configure((ConcreteState2 s, GoBack t) => new ConcreteState1())
            .Configure((ConcreteState3 s, GoBack t) => new ConcreteState2());

        // Act - Assert
        Assert.IsType<ConcreteState1>(stateMachine.State);

        // Advance
        stateMachine.Handle(new Advance());
        Assert.IsType<ConcreteState2>(stateMachine.State);
        
        stateMachine.Handle(new Advance());
        Assert.IsType<ConcreteState3>(stateMachine.State);

        stateMachine.Handle(new Advance());
        Assert.IsType<ConcreteState3>(stateMachine.State);
        
        // Go Back
        stateMachine.Handle(new GoBack());
        Assert.IsType<ConcreteState2>(stateMachine.State);

        stateMachine.Handle(new GoBack());
        Assert.IsType<ConcreteState1>(stateMachine.State);
        
        stateMachine.Handle(new GoBack());
        Assert.IsType<ConcreteState1>(stateMachine.State);
    }

    private class ConcreteState1 : IState;
    private class ConcreteState2 : IState;
    private class ConcreteState3 : IState;
    private class Advance : ITrigger;
    private class GoBack : ITrigger;
}