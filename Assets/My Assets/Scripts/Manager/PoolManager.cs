using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [Serializable]
    public class PoolObject
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public Transform parent;
    }

    [field: SerializeField] private PoolObject[] poolObjects { get; set; }
    private Dictionary<string, List<GameObject>> poolDictionary { get; set; }

    private void Awake()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (PoolObject poolObject in poolObjects)
        {
            poolDictionary.Add(poolObject.tag, new List<GameObject>());

            for (int i = 0; i < poolObject.size; ++i)
            {
                GameObject newObject = CreateNewObject(poolObject.tag, poolObject.prefab, poolObject.parent);

                poolDictionary[poolObject.tag].Add(newObject);
            }
        }
    }

    public GameObject GetObject(string tag, Vector3 position)
    {
        return GetObjectFromPool(tag, position, Quaternion.identity);
    }

    public GameObject GetObject(string tag, Vector3 position, Quaternion rotation)
    {
        return GetObjectFromPool(tag, position, rotation);
    }

    public T GetObject<T>(string tag, Vector3 position)
    {
        GameObject obj = GetObjectFromPool(tag, position, Quaternion.identity);

        if (obj.TryGetComponent(out T component))
        {
            return component;
        }

        throw new Exception("컴포넌트가 존재하지 않습니다.");
    }

    public T GetObject<T>(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject obj = GetObjectFromPool(tag, position, rotation);

        if (obj.TryGetComponent(out T component))
        {
            return component;
        }

        throw new Exception("컴포넌트가 존재하지 않습니다.");
    }

    private GameObject GetObjectFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            throw new Exception("poolDictionary에 해당 Key가 없습니다.");
        }

        List<GameObject> pool = poolDictionary[tag];
        GameObject objFromPool = pool.Find(obj => obj.activeSelf == false);

        if (objFromPool == null)
        {
            PoolObject poolObject = Array.Find(poolObjects, (PoolObject poolObj) => poolObj.tag == tag);
            GameObject newObject = CreateNewObject(poolObject.tag, poolObject.prefab, poolObject.parent);

            pool.Add(newObject);
            objFromPool = newObject;
        }

        objFromPool.transform.position = position;
        objFromPool.transform.rotation = rotation;
        objFromPool.SetActive(true);

        return objFromPool;
    }

    private GameObject CreateNewObject(string tag, GameObject prefab, Transform parent)
    {
        GameObject newObject = Instantiate(prefab, ((parent == null) ? transform : parent));

        newObject.name = tag;
        newObject.SetActive(false);

        return newObject;
    }
}
