using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform playerTrans;
    [SerializeField] private ObjectPool pool;
    [SerializeField] private int minEnemyCounter = 5;
    [SerializeField] private float spawnTimeGap = 3f;
    [SerializeField] private float spawnRadius = 15;
    [SerializeField] private int maxEnemyCount = 30;
    private float spawnTimer;

    private void Update()
    {
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        spawnTimer += Time.deltaTime;
        if((pool.GetActiveCounter() <= minEnemyCounter || spawnTimeGap < spawnTimer) && pool.GetActiveCounter() < maxEnemyCount)
        {
            spawnTimer = spawnTimeGap;
            Vector3 randomDir = Random.insideUnitSphere * spawnRadius;
            randomDir += playerTrans.position;
            if(NavMesh.SamplePosition(randomDir, out NavMeshHit navHit, spawnRadius, NavMesh.AllAreas))
            {
                //找到了一个有效的点
                Vector3 spawnPosition = navHit.position;
                GameObject enemy = pool.GetFromPool();
                enemy.transform.position = spawnPosition;
                Enemy enemyComponent = enemy.GetComponent<Enemy>();
                enemyComponent.PoolToReturnTo = this.pool;
                enemyComponent.SetHealthMax();
                //Debug.Log($"在NavMesh上的点 {spawnPosition} 生成了一个敌人。");
            }
            else
            {
                // 如果在指定范围内找不到有效的点，可以进行重试或等待下一次
                Debug.LogWarning("在指定区域内找不到有效的NavMesh生成点！");
            }
            //Random random = Random.Range()
            //enemy.transform.position = new Vector3()
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerTrans.position, spawnRadius);
    }
}
