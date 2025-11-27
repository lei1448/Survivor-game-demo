using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField] private String_StringEvent OnPlayerDied;
    private Player player;
    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public void PlayerDied()
    {
        OnPlayerDied?.Raise(new String_String { str1 = player.PlayerSO.playerWeapon.weaponName.ToString(), str2 = GameServices.Get<GameTimer>().timeSpan.ToString(@"hh\:mm\:ss") });
        player.ToggleInvincible();
    }
}
