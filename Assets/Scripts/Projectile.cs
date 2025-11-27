using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 3f;

    // 非常重要：每个被池管理的对象都需要知道把它“生”出来的池子是哪个
    // 以便在“死亡”时可以“回家”
    public ObjectPool PoolToReturnTo {get; set;}
    private float _lifetimeTimer;

    // OnEnable 在每次对象被从池中取出并激活时都会调用
    private void OnEnable()
    {
        // 重置生命周期计时器
        _lifetimeTimer = lifetime;
    }

    void Update()
    {
        // 向前移动
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // 生命周期倒计时
        _lifetimeTimer -= Time.deltaTime;
        if(_lifetimeTimer <= 0f)
        {
            // 生命周期结束，返回池中
            ReturnToPool();
        }
    }

    // 可以在这里添加碰撞检测逻辑
    private void OnTriggerEnter(Collider other)
    {
        // 比如碰到敌人后，也返回池中
        // ... 造成伤害 ...
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if(PoolToReturnTo != null)
        {
            PoolToReturnTo.ReturnToPool(this.gameObject);
        }
        else
        {
            // 如果因为某些原因没有设置池，作为备用方案直接销毁
            Debug.LogWarning("PoolToReturnTo 未设置，对象将被直接销毁！");
            Destroy(gameObject);
        }
    }
}