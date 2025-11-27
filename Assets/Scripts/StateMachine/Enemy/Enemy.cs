using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] protected Transform_FloatEvent onDamagedEvent;//用于受伤时弹出伤害ui
    [SerializeField] protected Transform_FloatEvent onEnemyDiedEvent;//用于生成掉落物
    [SerializeField] protected GameObjectEvent onEnemyDiedEvent_GO;//事件系统调用对象池的returnToPool方法和spawner的handle方法
    [SerializeField] protected VoidEvent onEnemyDiedEvent_Void;//用于设置子弹目标

    [SerializeField] protected EnemyData enemyData;

    //get from enemydata
    public float MoveSpeed { get; private set; }
    public float AttackDamage { get; private set; } 
    public float DetectedRadius { get; private set; } //巡逻状态需要
    public bool CanBeKnocked { get; private set; } = true;
    public int BackForce { get; private set; }  //受到的击退力,受击状态需要
    public EnemyData EnemyData { get; protected set; } 
    protected LayerMask playerLayerMask;
    private float currentHealth;

    //needed parameter
    protected Player player;

    //get per frame
    protected Vector3 movDir;

    public Rigidbody Rb {get; private set;}
    public Animator Anim {get; private set;}
    public StateMachine StateMachine {get; private set;}


    public EnemyPartrolState PartrolState { get; private set; }
    public EnemyAttackState EnemyAttackState { get; protected set; }
    public EnemyReadyForAttackState EnemyReadyForAttackState { get; protected set; }
    public EnemyHitState EnemyHitState { get; private set; }

    private void Awake()
    {
        EnemyData = enemyData;       
    }

    protected virtual void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        player = GameServices.Get<Player>();

        StateMachine = new StateMachine();
        PartrolState = new EnemyPartrolState(this,StateMachine);
        EnemyHitState = new EnemyHitState(this,StateMachine);
        EnemyReadyForAttackState = new EnemyReadyForAttackState(this,StateMachine);
        EnemyAttackState = new EnemyAttackState(this, StateMachine);

        StateMachine.Initialize(PartrolState);

        AttackDamage = EnemyData.damage;
        MoveSpeed = EnemyData.moveSpeed;
        DetectedRadius = EnemyData.detectedRadius;
        playerLayerMask = 1 << 6;
    }
     
    protected virtual void OnEnable()
    {
        currentHealth = EnemyData.maxHealth;
    }

    protected virtual void Update()
    {
        StateMachine.CurrentState.Update();
        if(player != null) movDir = new Vector3(player.transform.position.x, transform.position.y ,player.transform.position.z) - transform.position;
    }

    public virtual void Initialize(EnemyData data, float difficultyMultiplier)
    {
        this.EnemyData = data;

        currentHealth = EnemyData.maxHealth * difficultyMultiplier;
        MoveSpeed = EnemyData.moveSpeed * difficultyMultiplier;
        AttackDamage = EnemyData.damage * difficultyMultiplier;

        if(StateMachine != null)
        {
            StateMachine.Initialize(PartrolState);
        }
    }

    public virtual void TakeDamage(float damage,int knockbackForce)
    {
        currentHealth -= damage;
        BackForce = knockbackForce;
        if(CanBeKnocked) StateMachine.ChangeState(EnemyHitState);
        onDamagedEvent?.Raise(new Transform_Float {transform = this.transform , value = damage});

        if(currentHealth <= 0) Die();
    }

    protected virtual void Die()
    {
        StateMachine.ChangeState(PartrolState);
        onEnemyDiedEvent?.Raise(new Transform_Float { transform = this.transform, value = EnemyData.experienceValue });

        onEnemyDiedEvent_GO?.Raise(this.gameObject);

        onEnemyDiedEvent_Void?.Raise(new Void { });
    }

    protected virtual void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(AttackDamage);
        }
    }


    public virtual void SetVelocity(Vector3 velocity) => Rb.velocity = new Vector3(velocity.x, Rb.velocity.y, velocity.z);
    public virtual Vector3 GetMovDir() => movDir.normalized;
    public virtual bool ToggleCanBeKnocked() => CanBeKnocked = !CanBeKnocked;

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 4);
    }
}
