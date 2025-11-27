using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "SpellBrigade/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    public int maxHealth = 10;
    public float moveSpeed = 2f;
    public int damage = 5;
    public float detectedRadius;
    public GameObject enemyPrefab;

    [Header("Drops")]
    public GameObject experienceGemPrefab;
    public int experienceValue = 10;
}