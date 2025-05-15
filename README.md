This library (in part a small experiment, in part something that I think could be useful) aims to provide a library that allows to define:
- States
- Triggers
- Transitions (State + Trigger -> State)
And provide a StateMachine class that allows to configure Transitions and process a State and a Trigger to get a new State (or the same one, in case no transition is valid).

I also plan to add a more advanced StateMachine that allows to register a history of transitions, provide a timeline, go back to the previous State, etc.