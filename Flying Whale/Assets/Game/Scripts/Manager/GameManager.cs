using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] internal bool LOCKED = false;
    [SerializeField] internal LayerMask m_IgnoreLayer;
    [SerializeField] internal Camera m_MainCamera;
    internal GameObject m_Player;
    internal int[] m_itemCounter = new int[4];
    [SerializeField] bool m_specialItemPicked;
    [SerializeField] bool m_whaleTimeFinished;

    void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (m_MainCamera is null)
            m_MainCamera = Camera.main;

        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        SceneHandler.AddScene("TestingScene1");
        yield return new WaitUntil(() => m_specialItemPicked);
        SceneHandler.UnloadScene("TestingScene1");
        m_specialItemPicked = false;

        SceneHandler.AddScene("TestingScene2");
        yield return new WaitUntil(() => m_whaleTimeFinished);
        SceneHandler.UnloadScene("TestingScene2");
        m_whaleTimeFinished = false;

        SceneHandler.AddScene("TestingScene1");
        yield return new WaitUntil(() => m_specialItemPicked);
        SceneHandler.UnloadScene("TestingScene1");
        m_specialItemPicked = false;

        SceneHandler.AddScene("TestingScene2");
        yield return new WaitUntil(() => m_whaleTimeFinished);
        SceneHandler.UnloadScene("TestingScene2");
        m_whaleTimeFinished = false;


        yield return null;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            OptionsOverlay();

        Time.timeScale = LOCKED ? 0 : 1;
    }

    void OptionsOverlay()
    {
        LOCKED = !LOCKED;

        //Pause
        if (LOCKED)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (!SceneHandler.IsSceneLoaded("Options"))
                SceneHandler.AddScene("Options");
        }
        //Continue
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (SceneHandler.IsSceneLoaded("Options"))
                SceneHandler.UnloadScene("Options");
        }
    }

    internal void PickedItem(int _i) { m_itemCounter[_i]++; }

    internal void PickedItemSpecial() { m_specialItemPicked = true; }
    internal void WhaleTimeFinished() { m_whaleTimeFinished = true; }

    internal RaycastHit HitRayCast(float _maxDistance, Ray? _ray = null)
    {
        RaycastHit hit;

        Physics.Raycast(
            _ray is null
                ? m_MainCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f))
                : _ray.Value,
            out hit,
            _maxDistance,
            ~m_IgnoreLayer);

        return hit;
    }
    internal bool BoolRayCast(float _maxDistance, Ray? _ray = null)
    {
        return Physics.Raycast(
            _ray is null
                ? m_MainCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f))
                : _ray.Value,
            _maxDistance,
            ~m_IgnoreLayer);
    }
    internal static float Map(float _oldValue, float _oldMin, float _oldMax, float _newMin, float _newMax)
    {
        float oldRange = _oldMax - _oldMin;
        float newRange = _newMax - _newMin;
        float newValue = ((_oldValue - _oldMin) * newRange / oldRange) + _newMin;

        return Mathf.Clamp(newValue, _newMin, _newMax);
    }
}
public static class ExtensionMethods
{
    public static float Remap(this float _value, float _oldMin, float _oldMax, float _newMin, float _newMax)
    {
        return (_value - _oldMin) / (_oldMax - _oldMin) * (_newMax - _newMin) + _newMin;
    }
}