using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    Vector3 m_initPos;
    
    void Start()
    {
        m_initPos = transform.position;
    }
    void Update()
    {
        transform.position = new Vector3(
            m_initPos.x, 
            m_initPos.y + (Mathf.Sin(Time.time) * 0.5f + 1), 
            m_initPos.z);        
    }
}
