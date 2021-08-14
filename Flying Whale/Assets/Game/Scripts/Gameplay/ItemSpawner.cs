using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_spawnpoints = new List<GameObject>();

    [SerializeField]
    private GameObject m_itemPrefab;

    [SerializeField]
    private int m_itemCount;

    void Start()
    {
        if(m_itemCount <= m_spawnpoints.Count)
        {
            for(int i = 0; i < m_itemCount; i++)
            {
                bool freeSpawnPoint = false;

                while(!freeSpawnPoint)
                {
                    int rnd = Random.Range(0, m_spawnpoints.Count);
                    if(m_spawnpoints[rnd] != null)
                    {
                        Instantiate(m_itemPrefab, m_spawnpoints[rnd].transform.position, Quaternion.identity);
                        m_spawnpoints[rnd] = null;
                        freeSpawnPoint = true;
                    }
                }
            }
        }
    }
}
