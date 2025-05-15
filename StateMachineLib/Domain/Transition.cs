namespace StateMachineLib.Domain;

public delegate IState Transition<S,T>(S state, T trigger) 
    where S : IState 
    where T : ITrigger;