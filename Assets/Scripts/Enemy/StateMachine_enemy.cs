
public interface IEnemyState
{
    void Enter();
    void Update();
    void Exit();
}
public class StateMachine_enemy
{
    private IEnemyState currentState;

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null && currentState.GetType() == newState.GetType()) return;

        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }
}
