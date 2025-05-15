using StateMachineLib.Domain;

namespace StateMachineLib.Tests.UnitTests;

public class StatefulStateMachineTests
{
    [Fact]
    public void StatefulStateMachine_Transitions()
    {
        // Arrange
        var stateMachine = new StatefulStateMachine(new ConcreteState1(0));
        stateMachine
            .Configure((ConcreteState1 s, Advance t) => new ConcreteState2(s.Iteration + 1))
            .Configure((ConcreteState2 s, Advance t) => new ConcreteState3(s.Iteration + 1))
            .Configure((ConcreteState2 s, GoBack t) => new ConcreteState1(s.Iteration + 1))
            .Configure((ConcreteState3 s, GoBack t) => new ConcreteState2(s.Iteration + 1));

        // Act - Assert
        Assert.IsType<ConcreteState1>(stateMachine.State);
        Assert.Equal(0, ((ConcreteState1)stateMachine.State).Iteration);

        // Advance
        stateMachine.Handle(new Advance());
        Assert.IsType<ConcreteState2>(stateMachine.State);
        Assert.Equal(1, ((ConcreteState2)stateMachine.State).Iteration);

        stateMachine.Handle(new Advance());
        Assert.IsType<ConcreteState3>(stateMachine.State);
        Assert.Equal(2, ((ConcreteState3)stateMachine.State).Iteration);

        stateMachine.Handle(new Advance());
        Assert.IsType<ConcreteState3>(stateMachine.State);
        Assert.Equal(2, ((ConcreteState3)stateMachine.State).Iteration);

        // Go Back
        stateMachine.Handle(new GoBack());
        Assert.IsType<ConcreteState2>(stateMachine.State);
        Assert.Equal(3, ((ConcreteState2)stateMachine.State).Iteration);

        stateMachine.Handle(new GoBack());
        Assert.IsType<ConcreteState1>(stateMachine.State);
        Assert.Equal(4, ((ConcreteState1)stateMachine.State).Iteration);

        stateMachine.Handle(new GoBack());
        Assert.IsType<ConcreteState1>(stateMachine.State);
        Assert.Equal(4, ((ConcreteState1)stateMachine.State).Iteration);
    }

    private record ConcreteState1(int Iteration) : IState;
    private record ConcreteState2(int Iteration) : IState;
    private record ConcreteState3(int Iteration) : IState;
    private record Advance : ITrigger;
    private record GoBack : ITrigger;
}