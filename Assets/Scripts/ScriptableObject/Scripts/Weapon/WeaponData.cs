using UnityEngine;


public enum WeaponType
{
    Firearms,
    Melee
}


[CreateAssetMenu(fileName = "NewWeaponData", menuName = "SpellBrigade/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public WeaponType type; 
    public string weaponName;
    public Sprite weaponIcon;

    public float attackSpeed;
    public float attackRange;
    public int attackDamage;
    public float attackColdTime;
    public int knockbackForce;

    public GameObject weaponPrefab;
}