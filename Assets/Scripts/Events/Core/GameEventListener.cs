using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 抽象的“智能邮箱”组件。这是您可以挂载到游戏对象上的实际组件的“模板”。
/// 它负责连接“事件频道(ScriptableObject)”和“Unity的响应事件(UnityEvent)”。
/// </summary>
/// <typeparam name="T">包裹类型</typeparam>
/// <typeparam name="TEvent">事件频道类型</typeparam>
/// <typeparam name="TUnityEvent">Unity响应事件类型</typeparam>
public abstract class GameEventListener<T, TEvent, TUnityEvent> : MonoBehaviour, IGameEventListener<T>
    where TEvent : BaseGameEvent<T> // 泛型约束：确保 TEvent 是我们的事件频道类型
    where TUnityEvent : UnityEvent<T> // 泛型约束：确保 TUnityEvent 是能接收T类型参数的UnityEvent
{
    [Tooltip("要监听的事件频道")]
    [SerializeField] private TEvent gameEvent;

    [Tooltip("当事件发生时，要执行的响应")]
    [SerializeField] private TUnityEvent onEventRaised;

    // 在对象启用时，向频道“订阅”或“注册”
    private void OnEnable()
    {
        if(gameEvent != null)
        {
            gameEvent.RegisterListener(this);
        }
    }

    // 在对象禁用时，从频道“取消订阅”
    private void OnDisable()
    {
        if(gameEvent != null)
        {
            gameEvent.UnregisterListener(this);
        }
    }

    // 当被频道通知时，执行在 Inspector 中配置好的响应方法
    public void OnEventRaised(T data)
    {
        onEventRaised.Invoke(data);
    }
}