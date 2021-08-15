using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EPhase { ISLAND, WHALE }
public class TimerCountHandler : MonoBehaviour
{
    [SerializeField] EPhase phase;
    [SerializeField] Image img;
    [SerializeField] Canvas canvas;
    int[] TimeDurationWhalePhase = new int[] { 0, 30, 50, 60, 80 };
    int[] TimeDurationIslandPhase = new int[] { 0, 80, 35, 40, 45 };
    float m_timer;

    void Start()
    {
        if (phase == EPhase.WHALE) StartCoroutine(TimeCounterWhalePhase());
        if (phase == EPhase.ISLAND) StartCoroutine(TimeCounterIslandPhase());
    }

    IEnumerator TimeCounterWhalePhase()
    {
        canvas.targetDisplay = 2;

        yield return new WaitForSeconds(10);

        canvas.targetDisplay = 0;

        float timeGoal = TimeDurationWhalePhase[GameManager.Instance.m_CurrentLevel];
        m_timer = 0;

        while (m_timer < timeGoal)
        {
            yield return null;

            m_timer += Time.deltaTime;

            img.fillAmount = GameManager.Map(m_timer, 0, timeGoal, 0, 1);
        }

        GameManager.Instance.WhaleTimeFinished();

        yield return null;
    }

    IEnumerator TimeCounterIslandPhase()
    {
        float timeGoal = TimeDurationIslandPhase[GameManager.Instance.m_CurrentLevel];
        m_timer = 0;

        while (m_timer < timeGoal)
        {
            yield return null;

            m_timer += Time.deltaTime;

            GameManager.Instance.m_WindStrength = GameManager.Map(m_timer, 0, timeGoal, 0, 1);
        }

        GameManager.Instance.GameOver();

        yield return null;
    }
}
