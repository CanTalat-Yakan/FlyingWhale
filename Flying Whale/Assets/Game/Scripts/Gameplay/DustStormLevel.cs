using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DustStormLevel : MonoBehaviour
{
    [SerializeField] AnimationCurve m_curve;
    [SerializeField] ParticleSystem m_dust;
    [SerializeField] ParticleSystem m_trail;
    EmissionModule m_module;
    float m_tmp;

    void Start() { m_tmp = -1; }
    void Update()
    {
        if (m_tmp != GameManager.Instance.m_WindStrength)
        {
            if (GameManager.Instance.m_WindStrength == 0)
            {
                m_dust.Clear();
                m_trail.Clear();
            }

            m_dust.Play();
            m_module = m_dust.emission;
            m_module.rateOverTime = m_curve.Evaluate(GameManager.Instance.m_WindStrength) * 110;

            m_trail.Play();
            m_module = m_trail.emission;
            m_module.rateOverTime = m_curve.Evaluate(GameManager.Instance.m_WindStrength) * 80;
        }

        m_tmp = GameManager.Instance.m_WindStrength;
    }
}
