public class StateMachine// 状态机核心，负责状态的持有和切换
{
    public State CurrentState
    {
        get; private set;
    }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        CurrentState?.Exit(); // 确保当前状态不为空
        CurrentState = newState;
        newState.Enter();
    }
}