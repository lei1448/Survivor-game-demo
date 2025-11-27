using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameServices
{
    // 使用字典来存储服务，键是服务的类型，值是服务的实例
    private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

    // 注册服务
    public static void Register<T>(T service)
    {
        Type type = typeof(T);
        if(services.ContainsKey(type))
        {
            Debug.LogWarning($"服务 '{type.Name}' 已经被注册，旧的实例将被覆盖。");
            services[type] = service;
        }
        else
        {
            services.Add(type, service);
            Debug.Log($"服务 '{type.Name}' 注册成功。");
        }
    }

    //注销服务
    public static void UnRegister<T>(T service)
    {
        Type type = typeof(T);
        if(services.ContainsKey(type))
        {
            services.Remove(type);
            Debug.Log($"服务'{type.Name}'注销成功");
        }
        else
        {
            Debug.LogWarning($"服务'{type.Name}'不存在");
        }
    }

    // 获取服务
    public static T Get<T>()
    {
        Type type = typeof(T);
        if(services.TryGetValue(type, out object service))
        {
            return (T)service;
        }
        else
        {
            // 如果找不到服务，抛出异常可以帮助我们快速定位问题
            throw new InvalidOperationException($"未找到已注册的服务: '{type.Name}'。请确保它在 GameManager 中被正确注册。");
        }
    }

    // 清理所有服务，用于游戏重新开始等场景
    public static void Clear()
    {
        services.Clear();
        Debug.Log("所有服务已被清除。");
    }
}