using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPool : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int poolSize;
    internal Stack<GameObject> pool;

    void Awake()
    {
        pool = new Stack<GameObject>();
        CreateInstance();
    }

    void CreateInstance()
    {
        for (int i = 0; i < poolSize; i++)
            pool.Push(GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, transform));

        Debug.Log($"Instanced  {poolSize} GameObjects");
    }

    public void Add(GameObject obj)
    {
        pool.Push(obj);
    }

    public GameObject Get()
    {
        if (pool.Count == 0)
            CreateInstance();

        return pool.Pop();
    }
}
