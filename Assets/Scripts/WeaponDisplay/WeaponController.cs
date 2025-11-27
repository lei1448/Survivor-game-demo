using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Player player;
    private List<WeaponData> weapons;
    private float radius;//环绕半径
    private Vector3 meleeCenter;//近战武器环绕中心
    private Vector3 firearmCenter;//远程武器环绕中心
    private float meleeAngle;//武器所占角度
    private float fireAngle;
    private Vector3 updatePos;//更新坐标

    private int meleeCount = 0;
    private int fireCount = 0;


    private PlayerSO playerSO;

    void Start()
    {
        playerSO = GameServices.Get<GameData>().SaveData.playerSO;
        player = GameServices.Get<Player>();
        weapons = new List<WeaponData>()
        {
           playerSO.playerWeapon,
        };

        radius = 1;
        firearmCenter = player.transform.position + Vector3.up;
        meleeCenter = player.transform.position;

        UpdateWeaponPosition();
    }

    public void RemoveAllWeapoon()
    {
        weapons.Clear();
    }

    public void AddWeaponOwned()
    {
        weapons.Add(playerSO.playerWeapon);
    }

    public void AddWeapon(WeaponData weapon)
    {
        weapons.Add(weapon);
    }

    public void RemoveWeapon(WeaponData weapon)
    {
        weapons.Remove(weapon);
    }

    public void UpdateWeaponPosition()
    {
        GetAngle();
        for(int i = 0; i < weapons.Count; i++)
        {
            switch(weapons[i].type)
            {
                case WeaponType.Firearms:
                    updatePos.x = Mathf.Cos(fireAngle * (i - meleeCount)) * radius;
                    updatePos.z = Mathf.Sin(fireAngle * (i - meleeCount)) * radius;
                    Instantiate(weapons[i].weaponPrefab, updatePos + firearmCenter,Quaternion.identity);            
                    fireCount++;
                    break;
                case WeaponType.Melee:
                    updatePos.x = Mathf.Cos(meleeAngle * (i - fireCount)) * radius;
                    updatePos.z = Mathf.Sin(meleeAngle * (i - fireCount)) * radius;  
                    Instantiate(weapons[i].weaponPrefab, updatePos + meleeCenter, Quaternion.identity);
                    meleeCount++;
                    break;
            }
        }
    }

    private void GetAngle()
    {
        foreach(var weapon in weapons)
        {
            if(weapon.type == WeaponType.Firearms)
            {
                fireCount++;
            }
            else if(weapon.type == WeaponType.Melee)
            {
                meleeCount++;
            }
        }
        meleeAngle = 2 * Mathf.PI / meleeCount;
        fireAngle = 2 * Mathf.PI / fireCount;

        meleeCount = 0;
        fireCount = 0;
    }
}
