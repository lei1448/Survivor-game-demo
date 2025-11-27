using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerListSO",menuName = "SpellBrigade/PlayerListSO")]
public class PlayerListSO : ScriptableObject
{
    public List<PlayerSO> playerList = new();
}
