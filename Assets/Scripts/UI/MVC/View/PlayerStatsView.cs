using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsView : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Button healButton;

    // 将按钮的访问权限开放给Controller
    public Button HealButton => healButton;

    // 更新UI显示的公共方法，它只关心传入的数据，不关心业务逻辑
    public void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        if(healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if(healthText != null)
        {
            healthText.text = $"{currentHealth} / {maxHealth}";
        }
    }
}