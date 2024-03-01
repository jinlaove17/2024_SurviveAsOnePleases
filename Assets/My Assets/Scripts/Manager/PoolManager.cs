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
                GameObject newObject = CreateNewObject(poolObject.tag, poolObject.prefab);

                poolDictionary[poolObject.tag].Add(newObject);
            }
        }
    }

    private void Start()
    {
        for (int i = 0; i < 5; ++i)
        {
            float x = UnityEngine.Random.Range(-6.0f, 6.0f);
            float z = UnityEngine.Random.Range(-6.0f, 6.0f);

            GetObject("Normal Zombie", new Vector3(x, 0.0f, z));
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

        throw new Exception("������Ʈ�� �������� �ʽ��ϴ�.");
    }

    public T GetObject<T>(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject obj = GetObjectFromPool(tag, position, rotation);

        if (obj.TryGetComponent(out T component))
        {
            return component;
        }

        throw new Exception("������Ʈ�� �������� �ʽ��ϴ�.");
    }

    private GameObject GetObjectFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            throw new Exception("poolDictionary�� �ش� Key�� �����ϴ�.");
        }

        List<GameObject> pool = poolDictionary[tag];
        GameObject objFromPool = pool.Find(obj => obj.activeSelf == false);

        if (objFromPool == null)
        {
            PoolObject poolObject = Array.Find(poolObjects, (PoolObject poolObj) => poolObj.tag == tag);
            GameObject newObject = CreateNewObject(poolObject.tag, poolObject.prefab);

            pool.Add(newObject);
            objFromPool = newObject;
        }

        objFromPool.transform.position = position;
        objFromPool.transform.rotation = rotation;
        objFromPool.SetActive(true);

        return objFromPool;
    }

    private GameObject CreateNewObject(string tag, GameObject prefab)
    {
        GameObject newObject = Instantiate(prefab, transform);

        newObject.name = tag;
        newObject.SetActive(false);

        return newObject;
    }
}
