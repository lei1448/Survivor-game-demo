using UnityEngine;

/// <summary>
/// “智能包裹频道”的抽象基类（设计规范）。
/// 它规定了所有带数据的事件频道都必须能做什么。
/// </summary>
/// <typeparam name="T">包裹里数据的类型</typeparam>
public abstract class BaseGameEvent<T> : ScriptableObject
{
    public abstract void Raise(T data);
    public abstract void RegisterListener(IGameEventListener<T> listener);
    public abstract void UnregisterListener(IGameEventListener<T> listener);
}