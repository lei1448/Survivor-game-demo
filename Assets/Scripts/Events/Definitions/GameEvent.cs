using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "NewGameEvent", menuName = "SpellBrigade/Events/Game Event")]
/// <summary>
/// “智能包裹频道”的通用实现。
/// 这个类不能直接在Unity中创建，但被具体的事件频道继承。
/// </summary>
/// <typeparam name="T">包裹里数据的类型</typeparam>
public abstract class GameEvent<T> : BaseGameEvent<T>
{
    // 存储所有监听这个频道的“邮箱”
    private readonly List<IGameEventListener<T>> listeners = new List<IGameEventListener<T>>();

    /// <summary>
    /// 广播事件，通知所有监听者
    /// </summary>
    public override void Raise(T data)
    {
        // 从后往前遍历。这样即使监听者在响应事件时将自己注销，也不会影响循环。
        for(int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(data);
        }
    }

    public override void RegisterListener(IGameEventListener<T> listener)
    {
        if(!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public override void UnregisterListener(IGameEventListener<T> listener)
    {
        if(listeners.Contains(listener))
            listeners.Remove(listener);
    }
}