using UnityEngine;

public class StateMachine
{
    public EntityState currentState {  get; private set; }

    public void Initialize(EntityState startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    public void ChangeState(EntityState newsState) 
    {
        currentState.Exit();
        currentState = newsState;
        currentState.Enter();
    }

    public void UpdateActiveState()
    {
        currentState.Update();
    }
}
