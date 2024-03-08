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
        if (aliveCount <= 10)
        {
            for (int i = 0; i < 20; ++i)
            {
                CreateZombie();
            }
        }
    }

    private void CreateZombie()
    {
        // 생성할 좀비의 종류를 랜덤으로 결정
        Zombie zombiePrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];

        // 생성할 위치를 랜덤으로 결정
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 randomPos = Random.insideUnitSphere * 5.0f + spawnPoint.position;
        NavMeshHit navMeshHit;

        randomPos.y = 0.0f;

        while (!NavMesh.SamplePosition(randomPos, out navMeshHit, 3.0f, NavMesh.AllAreas)) { }
        
        Zombie zombie = PoolManager.instance.GetObject<Zombie>(zombiePrefab.name, navMeshHit.position, spawnPoint.rotation);

        zombie.onDeath += () => --aliveCount;
        ++aliveCount;
    }
}
