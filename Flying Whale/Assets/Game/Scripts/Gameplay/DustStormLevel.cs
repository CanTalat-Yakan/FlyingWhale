using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DustStormLevel : MonoBehaviour
{
    [SerializeField] ParticleSystem m_ps;
    [SerializeField] ParticleSystem m_sub;
    float m_tmp;

    void Start() { m_tmp = -1; }
    void Update()
    {
        if (m_tmp != GameManager.Instance.m_WindStrength)
        {
            EmissionModule module = m_ps.emission;
            module.rateOverTime = GameManager.Map(GameManager.Instance.m_WindStrength, 0, 1, 0, 1000);

            module = m_sub.emission;
            module.rateOverTime = GameManager.Map(GameManager.Instance.m_WindStrength, 0, 1, 0, 1000);
        }

        m_tmp = GameManager.Instance.m_WindStrength;
    }
}
