using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmsWeapon : AutoWeapon
{
    [Header("µ¯Ò©³Ø")]
    private ObjectPoolManager pool;
    private GameObject poolParent;


    private Vector3 offset;

    protected FirearmsWeaponData weaponData;

    protected GameObject projectilePrefab;

    protected float realProjtctileSpeed;
    protected override void Start()
    {
        base.Start();

        pool = GameServices.Get<ObjectPoolManager>();
        poolParent = new GameObject($"{this.name} projectile pool");
        poolParent.transform.parent = pool.gameObject.transform;
        offset = transform.position - player.transform.position;

        weaponData = (FirearmsWeaponData)baseWeaponData;

        realProjtctileSpeed = weaponData.projectileSpeed * realSpeed;
        projectilePrefab = weaponData.projectilePrefab;
    }

    private void AroundPlayer()
    {
        Quaternion.Euler(0, 50 * Time.deltaTime, 0);
        transform.position = player.transform.position + offset;
    }

    protected override void Attack()
    {
        AroundPlayer();
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0)
        { 
            FindNearestEnemy();
            if(targetEnemy != null) Fire();
            attackTimer = currentAttackColdTime / realSpeed;
        }
    }

    private void Fire()
    {
        for(int i = 0; i < weaponData.projectileCount; i++)
        {
            GameObject projectileGO = pool.GetFromPool(projectilePrefab, transform.position,Quaternion.identity,poolParent.transform);
            projectileGO.transform.position = transform.position;   
            projectileGO.GetComponent<Projectile>().SetProjectile(realKnockForce,weaponData.lifetime,realDamage,targetEnemy,weaponData.projectileSpeed);
        }
    }
}
