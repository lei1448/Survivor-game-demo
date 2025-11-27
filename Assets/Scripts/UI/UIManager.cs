using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    [SerializeField] private PlayerStatsView playerStatsView;

    private PlayerStatsController _playerStatsController;

    // 我们可以把Model暴露出去，方便游戏其他部分（比如战斗系统）访问
    public PlayerStatsModel PlayerStatsModel {get; private set;}

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        // 1. 创建 Model 实例
        PlayerStatsModel = new PlayerStatsModel(80, 100);

        // 2. 创建 Controller 实例，完成M和V的绑定
        _playerStatsController = new PlayerStatsController(PlayerStatsModel, playerStatsView);

        // 为了演示，2秒后模拟玩家受伤
        //Invoke(nameof(SimulateDamage), 2f);
    }

    void OnDestroy()
    {
        // 场景销毁时，清理事件订阅
        _playerStatsController?.CleanUp();
    }

    private void SimulateDamage()
    {
        Debug.Log("玩家受到30点伤害！");
        PlayerStatsModel.TakeDamage(30);
    }
}