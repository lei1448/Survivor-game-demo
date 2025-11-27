using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseUIManager : MonoBehaviour
{
    [Header("InitailizeInfo")]
    [SerializeField] private PlayerListSO playerList;
    [SerializeField] private Button characterTemplate;
    [SerializeField] private Transform characterTemplateParent;

    [Header("DetailPanel")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Image playerSprite;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI weaponText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI rangeText;
    // Start is called before the first frame update
    void Start()
    {
        InitializePlayerList();   
    }

    private void InitializePlayerList()
    {
        foreach(var playerSO in playerList.playerList)
        {
            Button newCharacterUI = Instantiate(characterTemplate, characterTemplateParent);
            newCharacterUI.GetComponent<CharacterTemplate>().SetInfo(playerSO);
            newCharacterUI.onClick.AddListener(() => ShowCharacterDetail(playerSO));
        }
    }

    public void ShowCharacterDetail(PlayerSO playerSO)
    {
        panel.SetActive(true);
        playerSprite.sprite = playerSO.playerSprite;
        playerName.text = playerSO.playerName;  
        weaponText.text = playerSO.playerWeapon.name;
        healthText.text = playerSO.playerHealth.ToString();
        strengthText.text = playerSO.attackDamage.ToString();
        speedText.text = playerSO.moveSpeed.ToString();
        rangeText.text = playerSO.attackRange.ToString();
    } 
}
