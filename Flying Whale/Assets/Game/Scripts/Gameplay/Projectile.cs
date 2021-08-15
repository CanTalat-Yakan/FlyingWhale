using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_rigidbody.AddForce(transform.forward * 20, ForceMode.Impulse);
        Destroy(this.gameObject, 2);
    }
}
