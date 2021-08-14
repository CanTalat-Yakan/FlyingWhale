using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DustStormLevel : MonoBehaviour
{
    [SerializeField] ParticleSystem m_dust;
    [SerializeField] ParticleSystem m_trail;
    EmissionModule m_module;
    float m_tmp;

    void Start() { m_tmp = -1; }
    void Update()
    {
        if (m_tmp != GameManager.Instance.m_WindStrength)
        {
            m_dust.Play();
            m_module = m_dust.emission;
            m_module.rateOverTime = GameManager.Map(GameManager.Instance.m_WindStrength, 0, 1, 0, 150);

            m_trail.Play();
            m_module = m_trail.emission;
            m_module.rateOverTime = GameManager.Map(GameManager.Instance.m_WindStrength, 0, 1, 0, 80);
        }

        m_tmp = GameManager.Instance.m_WindStrength;
    }
}
