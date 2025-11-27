using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewProjectile",menuName ="SpellBrigade/Projectile")]
public class ProjectileData : ScriptableObject
{
    public float lifetime;
    public float projectileDamage;
    public int projectileKnockback;
    public GameObject projectilePrefab;
}
