using UnityEngine;

[CreateAssetMenu(fileName = "PlayerChoice", menuName = "Game/PlayerChoice")]
public class PlayerChoice : ScriptableObject
{
    [Header("玩家选择的初始武器")]
    public WeaponData startingWeapon;
}