using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthUpgrade", menuName = "SpellBrigade/Upgrades/IncreaseHealth")]
public class HealthUpgrade : UpgradeData
{
    public int health;
    public override void ApplyUpgrade(GameObject player)
    {
        base.ApplyUpgrade(player);
        if(player.TryGetComponent<Player>(out Player component))
        {
            component.UpgradeHealth(health);
        }
    }
}
