using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "SpellBrigade/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string Name;
    [TextArea(3, 5)]
    public string Description;

    public virtual void ApplyUpgrade(GameObject player)
    {
        Debug.Log($"Ó¦ÓÃÉý¼¶: {Name}");
    }
}