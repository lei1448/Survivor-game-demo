using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackSpeedUpgrade", menuName = "SpellBrigade/Upgrades/AttackSpeed")]
public class AttackSpeedUpgrade : UpgradeData
{
    public float attackspeedPercentage;
    public override void ApplyUpgrade(GameObject player)
    {
        base.ApplyUpgrade(player);
        if(player.TryGetComponent<Player>(out Player component))
        {
            component.UpgradeAttackSpeed(attackspeedPercentage);
        }
    }
}

