using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFirearmsWeaponData", menuName = "SpellBrigade/Firearms Weapon Data")]
public class FirearmsWeaponData : WeaponData
{
    public int projectileCount;
    public int projectileSpeed;
    public float lifetime;

    public GameObject projectilePrefab;
}
