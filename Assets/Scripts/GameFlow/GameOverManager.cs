using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI weapon;
    public TextMeshProUGUI gameTime;
    public Button backMenu_Btn;

    public void GameOver(String_String weapon_Time)
    {   
        gameOverPanel.SetActive(true);
        weapon.text = weapon_Time.str1.ToString();
        gameTime.text = weapon_Time.str2;
        Time.timeScale = 0;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SaveManager.DeleteSaveFile("GAMEDATA");
    }
}
