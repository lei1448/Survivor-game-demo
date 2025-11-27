using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private VoidEvent onLevelUpFinished;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button upgradeOptionButtonPrefab;
    [SerializeField] private Transform optionsContainer;

    [SerializeField] private List<UpgradeData> allUpgrades;//升级选项列表

    private Player player;

    private void Awake()
    {
        panel.SetActive(false);
    }

    private void Start()
    {
        player = GameServices.Get<Player>();
        
    }

    public void ShowUpgradeOptions()
    {
        Time.timeScale = 0f;
        panel.SetActive(true);

        foreach(Transform child in optionsContainer)
        {
            Destroy(child.gameObject);
        }

        List<UpgradeData> optionsToShow = GetRandomUpgrades(3);
        foreach(UpgradeData upgradeData in optionsToShow)
        {
            Button optionButton = Instantiate(upgradeOptionButtonPrefab, optionsContainer);
            optionButton.GetComponentInChildren<TMP_Text>().text = $"{upgradeData.Name}\n<size=20>{upgradeData.Description}</size>";
            optionButton.onClick.AddListener(() =>
            {
                upgradeData.ApplyUpgrade(player.gameObject);
                onLevelUpFinished?.Raise(new Void { });
                HidePanel();
            });
        }
    }

    private void HidePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    private List<UpgradeData> GetRandomUpgrades(int count)
    {
        // 克隆列表以防修改原始列表
        List<UpgradeData> shuffledUpgrades = new List<UpgradeData>(allUpgrades);

        // Fisher-Yates 洗牌算法
        for(int i = 0; i < shuffledUpgrades.Count; i++)
        {
            UpgradeData temp = shuffledUpgrades[i];
            int randomIndex = Random.Range(i, shuffledUpgrades.Count);
            shuffledUpgrades[i] = shuffledUpgrades[randomIndex];
            shuffledUpgrades[randomIndex] = temp;
        }

        return shuffledUpgrades.GetRange(0, Mathf.Min(count, shuffledUpgrades.Count));
    }
}