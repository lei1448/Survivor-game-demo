using UnityEngine;

public abstract class State
{
    protected float stateTimer;
    public virtual void Enter()
    {
        stateTimer = 0;
    }

    // 状态更新时调用 (每帧)
    public virtual void Update()
    {
        stateTimer += Time.deltaTime;
    }

    // 状态退出时调用
    public virtual void Exit()
    {
    }
}