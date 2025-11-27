using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageUpgrade", menuName = "SpellBrigade/Upgrades/Damage")]
public class DamageUpgrade : UpgradeData
{
    public int damageAmount;
    public override void ApplyUpgrade(GameObject player)
    {
        base.ApplyUpgrade(player);
        if(player.TryGetComponent<Player>(out Player component))
        {
            component.UpgradeDamage(damageAmount);
        }
    }
}