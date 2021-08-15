using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHideCanvas : MonoBehaviour
{
    [SerializeField] Canvas[] m_canvas;

    void Update()
    {
        if (m_canvas.Length == 0) return;

        if(m_canvas[0].gameObject.activeInHierarchy && (GameManager.Instance.LOCKED || GameManager.Instance.HIDEALLCANVAS))
            foreach (var item in m_canvas)
                item.gameObject.SetActive(false);
        else if(!m_canvas[0].gameObject.activeInHierarchy && (!GameManager.Instance.LOCKED && !GameManager.Instance.HIDEALLCANVAS))
            foreach (var item in m_canvas)
                item.gameObject.SetActive(true);
    }
}
