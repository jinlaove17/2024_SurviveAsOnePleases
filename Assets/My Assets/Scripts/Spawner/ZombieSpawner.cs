using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : Spawner<Zombie>
{
    private void Update()
    {
        if (spawnCount <= 10)
        {
            for (int i = 0; i < 20; ++i)
            {
                CreateZombie();
            }
        }
    }

    private void CreateZombie()
    {
        // ������ ������ ������ �������� ����
        Zombie zombiePrefab = prefabs[Random.Range(0, prefabs.Length)];

        // ������ ��ġ�� �������� ����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 randomPos = Random.insideUnitSphere * 5.0f + spawnPoint.position;
        NavMeshHit navMeshHit;

        randomPos.y = 0.0f;

        while (!NavMesh.SamplePosition(randomPos, out navMeshHit, 3.0f, NavMesh.AllAreas)) { }
        
        Zombie zombie = PoolManager.instance.GetObject<Zombie>(zombiePrefab.name, navMeshHit.position, spawnPoint.rotation);

        zombie.onDeath += () => --spawnCount;
        ++spawnCount;
    }
}
