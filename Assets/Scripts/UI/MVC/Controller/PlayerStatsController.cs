public class PlayerStatsController
{
    private PlayerStatsModel _model;
    private PlayerStatsView _view;

    // 通过构造函数接收Model和View的实例，并完成绑定
    public PlayerStatsController(PlayerStatsModel model, PlayerStatsView view)
    {
        _model = model;
        _view = view;

        // 1. V -> C -> M: 监听View的按钮点击，然后调用Model的方法
        _view.HealButton.onClick.AddListener(OnHealButtonClicked);

        // 2. M -> V: 订阅Model的数据变化事件，当事件发生时，直接更新View
        _model.OnHealthChanged += OnHealthDataChanged;

        // 同时初始化UI
        _view.UpdateHealthDisplay(_model.CurrentHealth, _model.MaxHealth);
    }

    // 当治疗按钮被点击时
    private void OnHealButtonClicked()
    {
        // 调用Model的业务逻辑
        _model.Heal(10);
    }

    // 当Model的生命值数据变化时
    private void OnHealthDataChanged(int newHealth)
    {
        // 通知View更新显示
        _view.UpdateHealthDisplay(newHealth, _model.MaxHealth);
    }

    //防止事件的内存泄漏
    public void CleanUp()
    {
        _view.HealButton.onClick.RemoveListener(OnHealButtonClicked);
        _model.OnHealthChanged -= OnHealthDataChanged;
    }
}