using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    public E_SceneName sceneName;
    public void Switch()
    {
        GameServices.Get<SceneLoadManager>().ChangeScene(sceneName);
        Debug.Log($"teleportto{sceneName}");
    }
}
