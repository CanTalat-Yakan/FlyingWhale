using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Vector3 m_Offset;

    void LateUpdate()
    {
        transform.position = GameManager.Instance.m_PlayerPosition + m_Offset;
    }
}
