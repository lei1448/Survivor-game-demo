using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("发射设置")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private ObjectPool projectilePool;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            //Shoot();
        }
    }

    public void Shoot()
    {
        if(projectilePool == null || firePoint == null)
        {
            Debug.LogError("发射器或对象池未设置！");
            return;
        }

        // 1. 从池中获取一个子弹
        GameObject projectileObj = projectilePool.GetFromPool();

        // 2. 设置子弹的位置和旋转
        projectileObj.transform.position = firePoint.position;
        projectileObj.transform.rotation = firePoint.rotation;

        // 3. 告知子弹它应该返回哪个池
        Projectile projectileComponent = projectileObj.GetComponent<Projectile>();
        if(projectileComponent != null)
        {
            projectileComponent.PoolToReturnTo = this.projectilePool;
        }
    }
}