using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public float GameTime { get; private set; } = 0;
    public TimeSpan timeSpan;

    private GameTimer()
    {
    }

    private void Awake()
    {
        GameServices.Register<GameTimer>(this);
    }

    private void Start()
    {
        GameSaveData gameData = GameServices.Get<GameData>().SaveData;
        GameTime = gameData.gameTime;
    }
    private void OnDestroy()
    {
        GameServices.UnRegister<GameTimer>(this);
    }
    private void Update()
    {
        timeSpan = TimeSpan.FromSeconds(GameTime);  
        GameTime += Time.deltaTime;
        timeText.text = timeSpan.ToString(@"hh\:mm\:ss");
    }
}
