using UnityEngine;

public class GameManager : MonoBehaviour
{
   

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        GameServices.Clear();
    }
}