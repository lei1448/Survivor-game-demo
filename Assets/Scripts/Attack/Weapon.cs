using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData baseWeaponData;
    [SerializeField] protected LayerMask enemyLayerMask;

    protected Transform attackPoint;
    protected float currentAttackSpeed;
    protected int currentAttackDamage;
    protected float currentAttackRange;
    protected float currentAttackColdTime;
    protected int currentKnockForce;

    protected float realDamage;
    protected float realRange;
    protected float realSpeed;
    protected int realKnockForce;
    protected GameObject weaponPrefab;

    protected float attackTimer;

    protected Player player;

    protected virtual void Awake()
    {
        InitializeWeapon();
    }

    protected virtual void Start()
    {
        player = GameServices.Get<Player>();
        InitializeWeaponRealData();
    }
    protected virtual void InitializeWeapon()
    {
        currentAttackSpeed = baseWeaponData.attackSpeed;
        currentAttackDamage = baseWeaponData.attackDamage;
        currentAttackRange = baseWeaponData.attackRange;
        currentAttackColdTime = baseWeaponData.attackColdTime;
        currentKnockForce = baseWeaponData.knockbackForce;
        attackTimer = 0.2f;//开局0.2s后开始攻击

    }

    public virtual void InitializeWeaponRealData()//public为了事件系统访问
    {
        realDamage = currentAttackDamage + player.AttackDamage;
        realRange = currentAttackRange + player.AttackRange;
        realSpeed = currentAttackSpeed + player.AttackSpeed;
        realKnockForce = currentKnockForce * player.KnockBackForce;
    }



    protected virtual void Update()
    {
        Attack();
    }

    protected virtual void Attack()
    {
        
    }

    protected virtual void DamageEnemy()
    {
        
    }

    public void UpgradeDamage(int amount)
    {
        currentAttackDamage += amount;
    }

    public void UpgradeAttackSpeed(float percentage)
    {
        currentAttackSpeed *= (1 + percentage);
    }

    public void UpgradeAttackRange()
    {
        transform.localScale *= 1.1f;
    }

    protected virtual void OnDrawGizmos()
    {
        
    }
    public Transform GetPlayerTransform() => attackPoint;
    public WeaponData GetWeaponData() => baseWeaponData;
}
