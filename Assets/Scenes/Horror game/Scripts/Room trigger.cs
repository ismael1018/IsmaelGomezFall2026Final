using UnityEngine;
using System.Collections.Generic;

public class Roomtrigger : MonoBehaviour
{
    private List<EnemyAi> enemiesInRoom = new List<EnemyAi>();

    public void RegisterEnemy(EnemyAi enemy)
    {
        enemiesInRoom.Add(enemy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (EnemyAi enemy in enemiesInRoom)
            {
                if (enemy != null)
                    enemy.isChasing = true;
            }
        }
    }
}