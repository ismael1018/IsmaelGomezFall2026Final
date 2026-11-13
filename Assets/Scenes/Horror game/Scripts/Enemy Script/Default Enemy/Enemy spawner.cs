using UnityEngine;

public class Enemyspawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float spawnTime = 3f;
    public int maxEnemies = 1;
    public Roomtrigger roomTrigger; 

    private int spawnedCount = 0;

    void Start()
    {
        InvokeRepeating("spawnEnemy", 1f, spawnTime);
    }

    void spawnEnemy()
    {
        if (spawnedCount >= maxEnemies)
        {
            CancelInvoke("spawnEnemy");
            return;
        }

        GameObject enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        roomTrigger.RegisterEnemy(enemy.GetComponent<EnemyAi>());
        spawnedCount++;
    }
}