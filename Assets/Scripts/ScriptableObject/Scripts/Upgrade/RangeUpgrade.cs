using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeUpgrade", menuName = "SpellBrigade/Upgrades/IncreaseRange")]

public class RangeUpgrade : UpgradeData
{
    public float rangeAmount;
    public VoidEvent onRangeUpgrade;
    public override void ApplyUpgrade(GameObject player)
    {
        base.ApplyUpgrade(player);
        player.GetComponent<Player>().UpgradeAttackRange(rangeAmount);
        onRangeUpgrade?.Raise(new Void { });
    }
}
