using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOverDistance : MonoBehaviour
{
    [SerializeField] float m_scale;
    Vector3 m_initialScale;

    void Start()
    {
        m_initialScale = transform.localScale;
    }

    void Update()
    {
        if (GameManager.Instance)
            if (GameManager.Instance.m_MainCamera)
                transform.localScale = m_initialScale * Vector3.Distance(GameManager.Instance.m_MainCamera.transform.position, transform.position) * m_scale;
    }
}
