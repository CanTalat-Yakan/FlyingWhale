using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject m_SpawnedPrefab;
    public float m_SpawnCooldown = 1f;
    private float m_elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        m_elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_elapsedTime += Time.deltaTime;
        if(m_elapsedTime >=  m_SpawnCooldown)
		{
            m_elapsedTime = 0;
            Instantiate(m_SpawnedPrefab, transform);
		}
    }
}
