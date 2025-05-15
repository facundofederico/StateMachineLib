using StateMachineLib.Domain;

namespace StateMachineLib.Tests.UnitTests;

public class StatefulStateMachinePracticalExample
{
    [Fact]
    public void StatefulStateMachine_PracticalCase_GameEnemyAI()
    {
        var enemy = new Enemy();

        Assert.Equal(_idleSpeed, enemy.GetSpeed());

        do
        {
            enemy.HandleTrigger(new PlayerMadeNoise());
        } while (!enemy.HasDetectedPlayer());

        Assert.Equal(_patrolSpeed, enemy.GetSpeed());

        enemy.HandleTrigger(new PlayerInSight());
        Assert.Equal(_chaseSpeed, enemy.GetSpeed());
        
        enemy.HandleTrigger(new PlayerOutOfSight());
        Assert.Equal(_patrolSpeed, enemy.GetSpeed());
    }

    private class Enemy
    {
        private readonly StatefulStateMachine _stateMachine;

        public Enemy()
        {
            _stateMachine = new StatefulStateMachine(new IdleBehaviour());
            _stateMachine
                .Configure((IdleBehaviour s, PlayerMadeNoise t) => s.AttemptToHearPlayer() ? s : new PatrolBehaviour())
                .Configure((PatrolBehaviour s, PlayerInSight t) => new ChaseBehaviour())
                .Configure((ChaseBehaviour s, PlayerOutOfSight t) => new PatrolBehaviour());
        }

        public void HandleTrigger(ITrigger trigger)
        {
            _stateMachine.Handle(trigger);
        }

        public int GetSpeed()
        {
            var behaviour = (IEnemyBehaviour)_stateMachine.State;
            return behaviour.GetSpeed();
        }

        public bool HasDetectedPlayer() =>
            _stateMachine.State.GetType() == typeof(PatrolBehaviour) ||
            _stateMachine.State.GetType() == typeof(ChaseBehaviour);
    }

    private interface IEnemyBehaviour
    {
        public int GetSpeed();
    }

    private class IdleBehaviour() : IState, IEnemyBehaviour
    {
        private int _attemptsToHear = 0;
        public int GetSpeed() => _idleSpeed;
        public bool AttemptToHearPlayer()
        {
            if (new Random().Next(1, 4) > 2 - _attemptsToHear)
                return true;

            _attemptsToHear++;
            return false;
        }
    }

    private class PatrolBehaviour() : IState, IEnemyBehaviour
    {
        public int GetSpeed() => _patrolSpeed;
    }

    private class ChaseBehaviour() : IState, IEnemyBehaviour
    {
        public int GetSpeed() => _chaseSpeed;
    }

    private record PlayerMadeNoise : ITrigger;
    private record PlayerInSight : ITrigger;
    private record PlayerOutOfSight : ITrigger;
    const int _idleSpeed = 5;
    const int _patrolSpeed = 15;
    const int _chaseSpeed = 25;
}