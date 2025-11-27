using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{   
    public GameSaveData SaveData {get; private set;}

    private void Awake()
    {
        DontDestroyOnLoad(this);
        GameServices.Register<GameData>(this);
        SaveData = new GameSaveData();
    }
    public void SetPlayerData(PlayerSO player)
    {
        SaveData.playerSO = player;   
    }

    public void Save()
    {
        Player player = GameServices.Get<Player>();
        EnemySpawner spawner = GameServices.Get<EnemySpawner>(); 
        GameTimer timer = GameServices.Get<GameTimer>();

        if(player != null)
        {
            SaveData.playerData = new PlayerData(player);
        }
        if(spawner != null)
        {

            ////
            SaveData.gameTime = timer.GameTime;
            SaveData.currentWaveIndex = spawner.GetCurrentWaveIndex(); // 假设有这个方法
            ////
        }

        //SaveManager.SaveByJson("GAMEDATA", this);
        SaveManager.SaveByJson("GAMEDATA", SaveData);
        Debug.Log("游戏数据已保存！");
    }

    public void LoadSave(string fileName)
    {
         string path = Path.Combine(Application.persistentDataPath, fileName);
        if(File.Exists(path))
        {
            SaveData = SaveManager.LoadFromJson<GameSaveData>(fileName);
            Debug.Log($"{fileName} 加载成功");
        }
    }

}




[Serializable] // 确保这个类可以被JsonUtility序列化
public class GameSaveData
{
    public PlayerData playerData;
    public PlayerSO playerSO;
    public float gameTime = 0;
    public int currentWaveIndex = 0;
}

[Serializable] // 确保这个类可以被JsonUtility序列化
public class PlayerData
{
    public float MaxHealth;
    public float MoveSpeed;
    public float AttackDamage;
    public float AttackRange;
    public int KnockBackForce;
    public float AttackSpeed;
    public float CurrentHealth;
    public int CurrentLevel;
    public int CurrentExperience;
    public int ExperienceToNextLevel;

    // 提供一个空的构造函数供JsonUtility反序列化使用
    public PlayerData()
    {
    }

    // 提供一个用Player数据来填充自己的构造函数
    public PlayerData(Player player)
    {
        MaxHealth = player.MaxHealth;
        MoveSpeed = player.MoveSpeed;
        AttackDamage = player.AttackDamage;
        AttackRange = player.AttackRange;
        KnockBackForce = player.KnockBackForce;
        AttackSpeed = player.AttackSpeed;
        CurrentHealth = player.CurrentHealth;
        CurrentLevel = player.CurrentLevel;
        CurrentExperience = player.CurrentExperience;
        ExperienceToNextLevel = player.ExperienceToNextLevel;
    }
}