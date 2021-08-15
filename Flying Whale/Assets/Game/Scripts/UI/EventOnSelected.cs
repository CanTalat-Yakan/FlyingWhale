using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventOnSelected : MonoBehaviour
{
    EventSystem m_eventSystem;
    GameObject m_tmp;


    void Start() { m_eventSystem = GetComponent<EventSystem>(); }

    private void Update()
    {
        if (m_eventSystem.currentSelectedGameObject != m_tmp)
            Event();

        m_tmp = m_eventSystem.currentSelectedGameObject;
    }

    void Event()
    {
        AudioManager.Instance.Play(AudioManager.PlayRandomFromList(ref AudioManager.Instance.m_AudioInfo.Button.clips));
    }
}
