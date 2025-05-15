namespace StateMachineLib.Domain;

public interface IState;
public interface ITrigger;
public delegate IState Transition<S,T>(S state, T trigger) 
    where S : IState 
    where T : ITrigger;