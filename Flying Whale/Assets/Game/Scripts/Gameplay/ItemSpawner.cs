using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_spawnpoints;

    [SerializeField]
    private List<GameObject> m_itemPrefabs = new List<GameObject>();

    [SerializeField]
    private GameObject m_specialItemPrefab;

    [SerializeField]
    private int m_itemCount; //Same count for every item type for now 

    void Start()
    {
        m_spawnpoints = GameObject.FindGameObjectsWithTag("Spawnpoint");

        //check if enough spawnpoints for all items in total
        if (m_itemCount * m_itemPrefabs.Count < m_spawnpoints.Length)
        {
            SpawnItems();
        }
        else Debug.Log("Not enough spawnpoints available for number of items");
    }

    void SpawnItems()
    {
        //Loop for every item type
        for (int item = 0; item < m_itemPrefabs.Count; item++)
        {
            //Loop all items of one type
            for (int i = 0; i < m_itemCount; i++)
            {
                //spawnpoint can only be used once
                bool freeSpawnPoint = false;

                while (!freeSpawnPoint)
                {
                    int rnd = Random.Range(0, m_spawnpoints.Length);
                    if (m_spawnpoints[rnd] != null)
                    {
                        //Instantiate object of current item type at random generated spawnpoint
                        Instantiate(m_itemPrefabs[item], m_spawnpoints[rnd].transform.position, Quaternion.identity, gameObject.transform) ;
                        m_spawnpoints[rnd] = null;
                        freeSpawnPoint = true;
                    }
                }
            }
        }
        //Spawning special item
        Instantiate(m_specialItemPrefab, GameObject.FindGameObjectWithTag("SpecialItemSpawnpoint").transform.position, Quaternion.identity, gameObject.transform);
    }
}
