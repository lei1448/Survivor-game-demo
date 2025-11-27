using System.Collections.Generic;
using UnityEngine;

// 这个结构体用于定义单个敌人的信息和它的生成权重
[System.Serializable]
public struct EnemySpawnInfo
{
    public EnemyData enemyData; // 引用敌人的基础数据
    [Tooltip("生成权重，权重越高的敌人越容易出现")]
    public int weight;
}

[CreateAssetMenu(fileName = "NewEnemyWaveData", menuName = "SpellBrigade/EnemyWaveData")]
public class EnemyWaveData : ScriptableObject
{
    [Tooltip("该波次在游戏开始后多少秒激活")]
    public float timeToActivate;

    [Tooltip("该波次包含的敌人种类和权重")]
    public List<EnemySpawnInfo> enemiesInWave;
}