using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "SpellBrigade/Magic Enemy Data")]
public class MagicEnemyData : EnemyData
{
    [Header("Projectile")]
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float projectileLifeTime;
}