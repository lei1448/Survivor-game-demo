using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float health = 100;
    public ObjectPool PoolToReturnTo {get; set;}//对象池引用

    public float attackDistance;
    private float maxHealth = 100;
    public float moveSpeed;
    private Rigidbody rb;
    private Vector3 movDir;

    public StateMachine StateMachine {get; private set;}

    public EnemyPartrolState PartrolState { get; private set; }
    public EnemyAttackState EnemyAttackState { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StateMachine = new StateMachine();
        PartrolState = new EnemyPartrolState(this,StateMachine);
        EnemyAttackState = new EnemyAttackState(this,StateMachine);
        StateMachine.Initialize(PartrolState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentState.Update();
        movDir = new Vector3(Player.Instance.transform.position.x, transform.position.y ,Player.Instance.transform.position.z) - transform.position;
    }


    public void TackDamage(int damage)
    {
        health -= damage;

        // 使用新的生成器来创建伤害数字
        if(DamagePopupGenerator.Instance != null)
        {
            DamagePopupGenerator.Instance.Create(transform.position + Vector3.up * 1.5f, damage);
        }

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PoolToReturnTo.ReturnToPool(gameObject);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    public Vector3 GetMovDir()
    {
        return movDir.normalized;
    }

    public void SetHealthMax() => health = maxHealth;
    public LayerMask GetPlayerLayerMask() => playerMask;
    public int GetAttackDamage() => attackDamage;
    public float GetAttackRange() => attackRange;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
