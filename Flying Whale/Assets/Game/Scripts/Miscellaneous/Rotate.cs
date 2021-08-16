using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float m_speed = 1;
    
    void Update()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0, m_speed * Time.deltaTime, 0);
    }
}
