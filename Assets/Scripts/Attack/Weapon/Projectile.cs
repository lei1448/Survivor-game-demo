using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObjectEvent onProjectileDisappear;
    private float damage;
    private int knockBack;
    private float lifeTime;
    private Enemy targetEnemy;
    private float projectileSpeed;
    private Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        if(targetEnemy != null && other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage,knockBack);
            onProjectileDisappear?.Raise(this.gameObject);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if(targetEnemy != null)
        {
            transform.LookAt(targetEnemy.transform.position);
            rb.velocity = transform.forward * projectileSpeed;
        }
        if(lifeTime < 0)
        {
            onProjectileDisappear?.Raise(this.gameObject);
        }
    }

    public void SetProjectile(int knockback, float lifetime, float damage,Enemy enemy,float speed)
    {
        knockBack = knockback;
        lifeTime = lifetime;
        this.damage = damage;
        targetEnemy = enemy;
        projectileSpeed = speed;
    }
    public void SetProjectile(float lifetime, float damage, Vector3 vel)
    {
        lifeTime = lifetime;
        this.damage = damage;
        rb.velocity = vel;
    }

    public void SetTargetNull()
    {
        targetEnemy = null;
    }
}
