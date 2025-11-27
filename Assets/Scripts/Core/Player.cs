using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    //[SerializeField] private float invincibilityTime;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float playerHealth;
    public Rigidbody rb {get;private set;}
    public Animator Animator {get; private set;}
    public Vector2 MoveInput {get; private set;} 

    // 状态机和状态实例
    public StateMachine StateMachine {get; private set;}
    public PlayerIdleState IdleState {get; private set;}
    public PlayerWalkState WalkState {get; private set;}
    //public PlayerShootState ShootState {get; private set;}
    public PlayerHitState HitState {get; private set;}

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        rb = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();

        // 初始化状态机和所有状态
        StateMachine = new StateMachine();
        IdleState = new PlayerIdleState(this, StateMachine);
        WalkState = new PlayerWalkState(this, StateMachine);
        //ShootState = new PlayerShootState(this, StateMachine);
        HitState = new PlayerHitState(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();

        if(Input.GetKeyDown(KeyCode.H))//测试
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        // 1.调用UI模块的Model更新数据
        UIManager.Instance.PlayerStatsModel.TakeDamage(damageAmount);
        Debug.Log("beijizhong");
        // 2.切换到受击状态，产生视觉反馈
        StateMachine.ChangeState(HitState);
        playerHealth -= damageAmount;
        Debug.Log($"{playerHealth}");
    }

    //Player接收并执行命令的唯一入口
    public void ExecuteCommand(ICommand command)
    {
        command.Execute(this);
    }

    //由 MoveCommand 调用，用于更新移动数据
    public void SetMoveDirection(Vector2 direction)
    {
        MoveInput = direction;
    }

    public void PerformShoot()
    {
        // 只有在可以攻击的状态下才切换到射击状态
        // 这里为了简化，我们总是允许切换
        //StateMachine.ChangeState(ShootState);
    }

    public void SetPlayerVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    public float GetPlayerMoveSpeed() => moveSpeed;
}