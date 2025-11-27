using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerSO",menuName = "SpellBrigade/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public string playerName;
    public Sprite playerSprite;
    public WeaponData playerWeapon;

    public float moveSpeed;
    public float playerHealth;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;
    public int knockbackForce;
}
