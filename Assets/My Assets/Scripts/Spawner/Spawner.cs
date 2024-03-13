using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour
{
    [field: SerializeField] protected T[] prefabs { get; set; }
    [field: SerializeField] protected Transform[] spawnPoints { get; set; }
    protected int spawnCount { get; set; }
}
