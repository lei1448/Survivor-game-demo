using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{

    [SerializeField] private Float_FloatEvent OnPlayerHealthUpdate;//给血条
    [SerializeField] private Int_Event OnLevelUp;
    [SerializeField] private VoidEvent onLevelUpEvent;

    public PlayerSO PlayerSO {get;private set;}

    private bool isInvincible;
    private float currentHealth;
    private GameSaveData gameData;

    public int CurrentLevel { get; private set; } = 1;
    public int CurrentExperience { get; private set; } = 0;
    public int ExperienceToNextLevel { get; private set; } = 100;
    public float MaxHealth { get;private set; }
    public float MoveSpeed { get;private set; }
    public float AttackDamage { get;private set; }
    public float AttackRange { get;private set; }
    public int KnockBackForce { get;private set; }
    public float AttackSpeed { get;private set; }


    public Rigidbody Rb {get;private set;}
    public Animator Animator {get; private set;}
    public Vector2 MoveInput {get; private set;}

    // 状态机和状态实例
    public StateMachine StateMachine {get; private set;}
    public PlayerIdleState IdleState {get; private set;}
    public PlayerWalkState WalkState {get; private set;}
    public PlayerHitState HitState {get; private set;}
    public PlayerDiedState DiedState {get; private set;}
    public float CurrentHealth
    {
        get => currentHealth;
        private set => currentHealth = value;
    }

    private void Awake()
    {
        GameServices.Register<Player>(this);

        Rb = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        // 初始化状态机和所有状态
        StateMachine = new StateMachine();
        IdleState = new PlayerIdleState(this, StateMachine);
        WalkState = new PlayerWalkState(this, StateMachine);
        HitState = new PlayerHitState(this, StateMachine);
        DiedState =new PlayerDiedState(this,StateMachine);
    }


    private void Start()
    {
        gameData = GameServices.Get<GameData>().SaveData;
        PlayerSO = gameData.playerSO;

        if(gameData.playerData != null)
        {
            Debug.Log("发现存档，正在加载...");
            // 用加载的数据初始化自己
            InitializeFromSave(gameData.playerData);
        }
        else
        {
            Debug.Log("未发现存档，开始新游戏...");
            // 用ScriptableObject的数据初始化自己
            InitializeForNewGame();
        }

        OnPlayerHealthUpdate?.Raise(new Float_Float { val1 = CurrentHealth, val2 = MaxHealth });
        OnLevelUp?.Raise(CurrentLevel);
        StateMachine.Initialize(IdleState);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.Update(); 
    }

    private void OnDestroy()
    {
        GameServices.UnRegister(this);
    }

    // 用于新游戏的初始化
    public void InitializeForNewGame()
    {
        MaxHealth = PlayerSO.playerHealth;
        MoveSpeed = PlayerSO.moveSpeed;
        AttackDamage = PlayerSO.attackDamage;
        AttackRange = PlayerSO.attackRange;
        KnockBackForce = PlayerSO.knockbackForce;
        AttackSpeed = PlayerSO.attackSpeed;
        CurrentHealth = MaxHealth;
        CurrentLevel = 1;
        CurrentExperience = 0;
        ExperienceToNextLevel = 100; // 或者从SO读取
    }

    // 用于从存档恢复的初始化
    public void InitializeFromSave(PlayerData savedData)
    {
        MaxHealth = savedData.MaxHealth;
        MoveSpeed = savedData.MoveSpeed;
        AttackDamage = savedData.AttackDamage;
        AttackRange = savedData.AttackRange;
        KnockBackForce = savedData.KnockBackForce;
        AttackSpeed = savedData.AttackSpeed;
        CurrentHealth = savedData.CurrentHealth;
        CurrentLevel = savedData.CurrentLevel;
        ExperienceToNextLevel = savedData.ExperienceToNextLevel;
        CurrentExperience = savedData.CurrentExperience;
    }


    public void AddExperience(int amount)
    {
        CurrentExperience += amount;
        while(CurrentExperience >= ExperienceToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        CurrentExperience -= ExperienceToNextLevel;
        CurrentLevel++;

        // 升级所需的经验值可以设计一个增长曲线
        ExperienceToNextLevel = (int)(ExperienceToNextLevel * 1.5f);
        onLevelUpEvent?.Raise(new Void());
        OnLevelUp.Raise(CurrentLevel);
    }

    public void TakeDamage(float damageAmount)
    {
        if(!isInvincible)
        {
            StateMachine.ChangeState(HitState);
            CurrentHealth -= damageAmount;
            if(CurrentHealth < 0)
            {
                PlayerDied();
            }
            OnPlayerHealthUpdate?.Raise(new Float_Float { val1 = CurrentHealth, val2 = MaxHealth });
        }
    }

    private void PlayerDied()
    {
        CurrentHealth = 0;
        StateMachine.ChangeState(DiedState);
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

    public void SetPlayerVelocity(Vector3 velocity)
    {
        Rb.velocity = velocity;
    }

    public void UpgradeMoveSpeed(float percentage)
    {
        MoveSpeed *= (1 + percentage);
    }
    public void UpgradeDamage(int amount)
    {
        AttackDamage += amount;
    }
    public void UpgradeAttackSpeed(float percentage)
    {
        AttackSpeed *= (1 + percentage);
    }

    public void UpgradeAttackRange(float amount)
    {
        AttackRange += amount;
    }

    public void UpgradeHealth(float amount)
    {
        MaxHealth += amount;
        CurrentHealth += amount;
        OnPlayerHealthUpdate?.Raise(new Float_Float { val1 = CurrentHealth, val2 = MaxHealth });
    }

    public bool ToggleInvincible()
    {
        isInvincible = !isInvincible;
        return isInvincible;
    }
}
