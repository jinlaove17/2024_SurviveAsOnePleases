using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    [field: SerializeField] private Zombie[] zombiePrefabs { get; set; }
    [field: SerializeField] private Transform[] spawnPoints { get; set; }
    private int aliveCount { get; set; }

    private void Update()
    {
        if (aliveCount <= 5)
        {
            for (int i = 0; i < 10; ++i)
            {
                CreateZombie();
            }
        }
    }

    private void CreateZombie()
    {
        // ������ ������ ������ �������� ����
        Zombie zombiePrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];

        // ������ ��ġ�� �������� ����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 randomPos = Random.insideUnitSphere * 3.0f + spawnPoint.position;
        NavMeshHit navMeshHit;

        while (!NavMesh.SamplePosition(randomPos, out navMeshHit, 3.0f, NavMesh.AllAreas)) { }
        
        Zombie zombie = PoolManager.instance.GetObject<Zombie>(zombiePrefab.name, navMeshHit.position);

        zombie.onDeath += () => --aliveCount;
        ++aliveCount;
    }
}
