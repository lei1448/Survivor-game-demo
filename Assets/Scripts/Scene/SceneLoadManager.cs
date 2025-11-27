using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : MonoBehaviour
{
    void Awake()
    {
        GameServices.Register<SceneLoadManager>(this);
        DontDestroyOnLoad(this.gameObject);
    }
    public CanvasGroup blackCurtainCanvasGroup;
    public float fadeDuration;
    public void ChangeScene(E_SceneName sceneName)
    {
        StartCoroutine(LoadSceneAsynchronously(sceneName));
    }
    private IEnumerator LoadSceneAsynchronously(E_SceneName sceneName)
    {
        blackCurtainCanvasGroup.blocksRaycasts = true;
        Debug.Log("starttele");
        yield return StartCoroutine(Fade(1));
        Debug.Log("fade1");
        yield return SceneManager.LoadSceneAsync(sceneName.ToString());
        Debug.Log("load finished");
        StartCoroutine(Fade(0));
        blackCurtainCanvasGroup.blocksRaycasts = false;
    }

    private IEnumerator Fade(float targetAlpha)
    {  
        float elapsedTime = 0f;
        float t;
        float startAlpha = blackCurtainCanvasGroup.alpha;
        while(elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;   
            t = Mathf.Clamp01(elapsedTime / fadeDuration);
            blackCurtainCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t * t * t);
            yield return null;
        }
        blackCurtainCanvasGroup.alpha = targetAlpha;
    }
}
