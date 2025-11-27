using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTemplate : MonoBehaviour
{
    private PlayerSO playerSO;
    [SerializeField] private Image image;

    public void SetInfo(PlayerSO playerSO)
    {
        this.playerSO = playerSO;
        image.sprite = playerSO.playerSprite;
    }

    public void SetPlayerData()
    {
        GameServices.Get<GameData>().SetPlayerData(playerSO);
    }
}
