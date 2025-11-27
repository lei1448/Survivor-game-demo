using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using XLua;

[LuaCallCSharp]
public class AAMgr : SingletonMono<AAMgr>
{
    // 缓存资源句柄，用于管理和释放
    private readonly Dictionary<string, AsyncOperationHandle> _assetHandles = new Dictionary<string, AsyncOperationHandle>();
    // 缓存实例化对象，用于通过实例反向查找地址以释放资源
    private readonly Dictionary<GameObject, string> _instantiatedObjects = new Dictionary<GameObject, string>();

    #region 同步加载
    // Addressables 的主要设计思想是异步。同步加载会阻塞主线程，可能导致卡顿。

    /// <summary>
    /// 同步加载资源（泛型版本）
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="address">资源的Addressable地址(名字)</param>
    /// <returns>加载到的资源</returns>
    public T LoadAsset<T>(string address) where T : Object
    {
        if (_assetHandles.TryGetValue(address, out var handle))
        {
            return handle.Result as T;
        }

        // 执行同步加载
        AsyncOperationHandle<T> newHandle = Addressables.LoadAssetAsync<T>(address);
        T result = newHandle.WaitForCompletion(); // 阻塞主线程
        if (newHandle.Status == AsyncOperationStatus.Succeeded)
        {
            _assetHandles[address] = newHandle;
        }
        else
        {
            Debug.LogError($"[AAMgr] Sync load asset failed: {address}");
            return null;
        }

        Debug.Log($"[AAMgr] Sync load asset succeed: {address}");
        return result;
    }
    /// <summary>
    /// 同步加载资源 (Lua专用, 返回Object类型)
    /// </summary>
    public Object LoadAsset(string address)
    {
        return LoadAsset<Object>(address);
    }
    /// <summary>
    /// 同步加载并实例化GameObject
    /// </summary>
    /// <param name="address">GameObject的Addressable地址</param>
    /// <param name="parent">父物体</param>
    /// <returns>实例化后的GameObject</returns>
    public GameObject LoadAndInstantiate(string address, Transform parent = null)
    {
        GameObject prefab = LoadAsset<GameObject>(address);
        if (prefab == null) return null;

        GameObject instance = Instantiate(prefab, parent);
        _instantiatedObjects[instance] = address;
        return instance;
    }

    #endregion

    #region 异步加载

    /// <summary>
    /// 异步加载资源 (泛型)
    /// </summary>
    public void LoadAssetAsync<T>(string address, UnityAction<T> callback) where T : Object
    {
        if (_assetHandles.TryGetValue(address, out var handle))
        {
            // 如果句柄已加载完成
            if (handle.IsDone)
            {
                callback?.Invoke(handle.Result as T);
            }
            // 如果句柄正在加载中，则在其完成时回调
            else
            {
                handle.Completed += op => callback?.Invoke(op.Result as T);
            }
            return;
        }

        StartCoroutine(ReallyLoadAssetAsync(address, callback));
    }

    private IEnumerator ReallyLoadAssetAsync<T>(string address, UnityAction<T> callback) where T : Object
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);
        _assetHandles[address] = handle; // 立即缓存句柄

        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            callback?.Invoke(handle.Result);
        }
        else
        {
            Debug.LogError($"[AAMgr] Load asset failed: {address}");
            _assetHandles.Remove(address); // 加载失败则移除
            callback?.Invoke(null);
        }
    }

    /// <summary>
    /// 异步加载资源 (Lua专用, 返回Object类型)
    /// </summary>
    public void LoadAssetAsync(string address, UnityAction<Object> callback)
    {
        LoadAssetAsync<Object>(address, callback);
    }

    /// <summary>
    /// 异步实例化GameObject
    /// </summary>
    public void InstantiateAsync(string address, UnityAction<GameObject> callback, Transform parent = null)
    {
        LoadAssetAsync<GameObject>(address, (prefab) =>
        {
            if (prefab != null)
            {
                GameObject instance = Instantiate(prefab, parent);
                _instantiatedObjects[instance] = address; // 记录实例
                callback?.Invoke(instance);
            }
            else
            {
                Debug.LogError($"[AAMgr] Instantiate failed, prefab not found: {address}");
                callback?.Invoke(null);
            }
        });
    }

    #endregion

    #region 资源释放

    /// <summary>
    /// 释放一个资源 (减少其引用计数)
    /// </summary>
    public void ReleaseAsset(string address)
    {
        if (_assetHandles.TryGetValue(address, out var handle))
        {
            Addressables.Release(handle);
            _assetHandles.Remove(address);
        }
    }

    /// <summary>
    /// 销毁一个通过本管理器创建的GameObject实例，并释放其资源
    /// </summary>
    public void ReleaseInstance(GameObject instance)
    {
        if (instance != null && _instantiatedObjects.TryGetValue(instance, out string address))
        {
            ReleaseAsset(address);
            _instantiatedObjects.Remove(instance);
        }
        Destroy(instance);
    }
    
    /// <summary>
    /// 清理所有通过本管理器加载的资源和实例
    /// </summary>
    public void ClearAll()
    {
        // 销毁所有实例化的对象
        foreach (var instance in _instantiatedObjects.Keys)
        {
            Destroy(instance);
        }

        // 释放所有加载的资源句柄
        foreach (var handle in _assetHandles.Values)
        {
            Addressables.Release(handle);
        }

        _assetHandles.Clear();
        _instantiatedObjects.Clear();
    }
    
    protected void OnDestroy()
    {
        ClearAll();
    }

    #endregion
}