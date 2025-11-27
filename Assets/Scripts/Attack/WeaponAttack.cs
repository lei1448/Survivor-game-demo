using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public Action<float,Transform> OnRangeAttack;
    public Action OnRangeAttackFinished;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackColdTime = 0.1f;
    private float attackTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime * attackSpeed;
        if(attackTimer < 0)
        {
            AttackAllEnemy();
        }
        else if(attackTimer < attackColdTime * 0.8)
        {
            OnRangeAttackFinished?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }

    private void AttackAllEnemy()
    {
        Collider[] enemies;
        enemies = Physics.OverlapSphere(attackPoint.position, attackRange);
        foreach(Collider enemyCol in enemies)
        {
            if(enemyCol.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.TackDamage(attackDamage);
            }
        }
        OnRangeAttack?.Invoke(attackRange,attackPoint);
        attackTimer = attackColdTime;
    }

    public Transform GetAttackPoint() => attackPoint;

    // 新增：用于升级攻击力
    public void UpgradeDamage(int amount)
    {
        attackDamage += amount;
    }

    // 新增：用于升级攻击速度
    public void UpgradeAttackSpeed(float percentage)
    {
        attackSpeed *= (1 + percentage);
    }
}
