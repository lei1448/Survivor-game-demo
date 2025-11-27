using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedUpgrade", menuName = "SpellBrigade/Upgrades/MoveSpeed")]
public class SpeedUpgrade : UpgradeData
{
    public float speedPercentage;

    public override void ApplyUpgrade(GameObject player)
    {
        base.ApplyUpgrade(player);
        Player playerComponent = player.GetComponent<Player>();
        if(playerComponent != null)
        {
            playerComponent.UpgradeMoveSpeed(speedPercentage);
        }
    }
}