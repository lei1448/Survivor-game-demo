using System.IO;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject continueButton;
    public GameObject maskPanel;
    private void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "GAMEDATA");

        if(File.Exists(path))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void DeleteSave()
    {
        SaveManager.DeleteSaveFile("GAMEDATA");
    }

    public void LoadSave()
    {
        GameServices.Get<GameData>().LoadSave("GAMEDATA");
    }

    public void ShowMaskPanel()
    {
        maskPanel.SetActive(true);
    }
    public void HideMaskPanel()
    {
        maskPanel.SetActive(false);
    }

    public void OpenPanel(GameObject panelToShow)
    {
        panelToShow.SetActive(true);
        maskPanel.SetActive(true);
    }
    public void ClosePanel(GameObject panelToHide)
    {
        panelToHide.SetActive(false);
        maskPanel.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

