This library aims to provide a simple mechanism to change the state of an entity according to state transitions, and keeping each state in a record to allow users to go back to previous states reliably.

Terminology:
- StateableEntity: abstract class that represents an entity whose attribute values can be assigned to a certain state and therefore we can say, if we compare it to itself in another point in time while having those attributes with the same value, we can say the entity shares state.
- StateMachine: tool that takes in an entity, and a list of previous states of that entity, and allows the user to either mutate that state from a trigger and a set of rules, or to restore a previous state of that entity.