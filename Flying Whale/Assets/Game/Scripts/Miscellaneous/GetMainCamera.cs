using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMainCamera : MonoBehaviour
{
    Camera m_camera;

    void Start()
    {
        if (m_camera is null)
            GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
