using System;

public class PlayerStatsModel
{
    public int CurrentHealth {get; private set;}
    public int MaxHealth {get; private set;}

    // 当生命值发生变化时触发时间
    public event Action<int> OnHealthChanged;

    public PlayerStatsModel(int startingHealth, int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        if(CurrentHealth < 0)
            CurrentHealth = 0;

        // 广播事件，并附上新的生命值
        OnHealthChanged?.Invoke(CurrentHealth);
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        if(CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;

        // 广播事件
        OnHealthChanged?.Invoke(CurrentHealth);
    }
}