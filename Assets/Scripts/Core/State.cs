// 所有状态的通用基类
public abstract class State
{
    // 进入状态时调用
    public virtual void Enter()
    {
    }

    // 状态更新时调用 (每帧)
    public virtual void Update()
    {
    }

    // 状态退出时调用
    public virtual void Exit()
    {
    }
}