using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canons : MonoBehaviour
{
    [SerializeField]
    private float m_cooldownTime;

    [SerializeField]
    private GameObject m_projectilePrefab;

    private float m_startTime;

    private void Start()
    {
        m_startTime = m_cooldownTime;
    }

    void Update()
    {
        m_cooldownTime -= Time.deltaTime;
        if(m_cooldownTime <= 0)
        {
            GameObject projectile = Instantiate(m_projectilePrefab, transform.position, transform.localRotation);

            m_cooldownTime = m_startTime;
        }
    }
}
