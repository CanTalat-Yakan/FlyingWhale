using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float m_timeValue;

    public TextMeshProUGUI timerText;

    void Update()
    {
        if (m_timeValue > 0)
        {
            m_timeValue -= Time.deltaTime;
        }
        else
        {
            m_timeValue = 0;
            GameManager.Instance.GameOver();
        }

        DisplayTime(m_timeValue);
    }

    private void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }
}
