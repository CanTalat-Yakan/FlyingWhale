using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    internal int m_CurrentLevel { get; private set; }

    [SerializeField] internal bool HIDEALLCANVAS = false;
    [SerializeField] internal bool LOCKED = false;
    [SerializeField] internal LayerMask m_IgnoreLayer;
    [SerializeField] internal Camera m_MainCamera;
    [SerializeField] internal CinemachineVirtualCamera m_CMVCamera;
    [SerializeField] internal TweenCurves m_Curves;
    [SerializeField] internal Vector3 m_PlayerPosition { get => m_controller.transform.position; }

    [SerializeField] StarterAssets.ThirdPersonController m_controller;
    [SerializeField] Animator m_animator;
    [SerializeField] RuntimeAnimatorController m_combatAniController;
    [SerializeField] RuntimeAnimatorController m_explorerAniController;
    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_pivot;
    [SerializeField] GameObject m_sword;
    [SerializeField] Material m_day;
    [SerializeField] Material m_night;
    [SerializeField] AudioSource m_wind;
    [SerializeField] FollowPlayer m_windPS;
    [SerializeField] Canvas m_ressourceCanvas;
    [Range(0, 1)]
    [SerializeField] internal float m_WindStrength;
    [Space]
    internal int[] m_ItemCounter = new int[4];
    [SerializeField] bool m_specialItemPicked;
    [SerializeField] bool m_whaleTimeFinished;
    [SerializeField] internal bool m_Fighting;
    [SerializeField] internal bool m_Attacking;
    IEnumerator m_currentCoroutine;


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
        m_CurrentLevel = 1;

        StartCoroutine(m_currentCoroutine = StartIsland());

        yield return new WaitUntil(() => m_CurrentLevel == 2);

        m_CurrentLevel = 1;
        StartCoroutine(m_currentCoroutine = StartIsland());

        yield return new WaitUntil(() => m_CurrentLevel == 2);

        m_CurrentLevel = 1;
        StartCoroutine(m_currentCoroutine = StartIsland());

        yield return null;
    }

    IEnumerator StartIsland()
    {
        yield return new WaitWhile(() => AudioManager.Instance is null);

        AudioManager.Instance.StartMainMusic();
        AudioManager.Instance.StopBattleMusic();


        SceneHandler.AddScene("Island" + m_CurrentLevel);

        yield return new WaitUntil(() => SceneHandler.IsSceneLoaded("Island" + m_CurrentLevel));

        m_animator.runtimeAnimatorController = m_explorerAniController;

        m_WindStrength = 0;

        ResetPlayer();

        if (m_CurrentLevel % 1 == 0)
            SetDay();
        else
            SetNight();

        yield return new WaitWhile(() => TimelineManager.Instance.m_IsPlaying);
        TimelineManager.Instance.Play(TimelineManager.Instance.m_Beginning);

        yield return new WaitUntil(() => m_specialItemPicked);

        TimelineManager.Instance.Play(TimelineManager.Instance.m_Ending);
        yield return new WaitWhile(() => TimelineManager.Instance.m_IsPlaying);

        SceneHandler.UnloadScene("Island" + m_CurrentLevel);

        m_specialItemPicked = false;
        ResetPlayer();

        StartCoroutine(m_currentCoroutine = StartWhalePhase());


        yield return null;
    }

    IEnumerator StartWhalePhase()
    {
        yield return new WaitWhile(() => AudioManager.Instance is null);

        AudioManager.Instance.StopMainMusic();
        AudioManager.Instance.PlayBattleMusic();

        SceneHandler.AddScene("Whale");

        yield return new WaitUntil(() => SceneHandler.IsSceneLoaded("Whale"));

        ResetPlayer();

        if (m_CurrentLevel % 1 == 0)
            SetNight();
        else
            SetDay();

        m_windPS.m_Offset = Vector3.down * 7;
        m_WindStrength = 0.15f;

        yield return new WaitForSeconds(10);

        m_ressourceCanvas.targetDisplay = 2;

        m_animator.runtimeAnimatorController = m_combatAniController;

        m_Fighting = true;

        yield return new WaitUntil(() => m_animator.runtimeAnimatorController == m_combatAniController);

        GameObject sword = Instantiate(m_sword, Vector3.zero, Quaternion.identity, m_pivot.transform);
        sword.transform.localPosition = m_sword.transform.position;
        sword.transform.localRotation = m_sword.transform.rotation;


        yield return new WaitUntil(() => m_whaleTimeFinished);

        SceneHandler.UnloadScene("Whale");

        m_windPS.m_Offset = Vector3.zero;

        m_ressourceCanvas.targetDisplay = 0;

        Destroy(sword);

        m_whaleTimeFinished = false;
        ResetPlayer();

        m_CurrentLevel++;


        yield return null;
    }
    IEnumerator Respawn()
    {

        SceneHandler.UnloadScene("Island" + m_CurrentLevel);

        yield return new WaitWhile(() => SceneHandler.IsSceneLoaded("Island" + m_CurrentLevel));

        SceneHandler.AddScene("Island" + m_CurrentLevel);

        yield return new WaitUntil(() => SceneHandler.IsSceneLoaded("Island" + m_CurrentLevel));

        m_animator.runtimeAnimatorController = m_explorerAniController;

        m_WindStrength = 0;

        ResetPlayer();

        if (m_CurrentLevel % 1 == 0)
            SetDay();
        else
            SetNight();

        m_specialItemPicked = false;

        yield return new WaitUntil(() => m_specialItemPicked);

        TimelineManager.Instance.Play(TimelineManager.Instance.m_Ending);
        yield return new WaitWhile(() => TimelineManager.Instance.m_IsPlaying);

        SceneHandler.UnloadScene("Island" + m_CurrentLevel);

        m_specialItemPicked = false;
        ResetPlayer();

        StartCoroutine(m_currentCoroutine = StartWhalePhase());


        yield return null;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            OptionsOverlay();

        Time.timeScale = LOCKED ? 0 : 1;

        m_wind.volume = m_Curves.Log.Evaluate(m_WindStrength);
    }

    public void GameOver() { StopCoroutine(m_currentCoroutine); StartCoroutine(Respawn()); }

    #region Predefined 
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

            AudioManager.Instance.Play(AudioManager.Instance.m_AudioInfo.PanelOpen);
            AudioManager.Instance.StopMainMusic();
            AudioManager.Instance.StopBattleMusic();
        }
        //Continue
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (SceneHandler.IsSceneLoaded("Options"))
                SceneHandler.UnloadScene("Options");

            AudioManager.Instance.Play(AudioManager.Instance.m_AudioInfo.PanelClose);

            if (SceneHandler.IsSceneLoaded("Whale"))
                AudioManager.Instance.StartBattleMusic();
            else
                AudioManager.Instance.StartMainMusic();
        }
    }

    void SetNight()
    {
        RenderSettings.skybox = m_night;
        GameObject.FindGameObjectWithTag("DirectionalLight").GetComponent<Light>().intensity = 0.2f;
    }
    void SetDay()
    {
        RenderSettings.skybox = m_day;
        GameObject.FindGameObjectWithTag("DirectionalLight").GetComponent<Light>().intensity = 1;
    }

    internal int CheckGroundType()
    {
        //if (BoolRayCast(1, new Ray(m_controller.transform.position, Vector3.down))) return 0;

        //string tag = HitRayCast(1, new Ray(m_controller.transform.position, Vector3.down)).collider.tag;
        //if (tag == "Dirty") return 0;
        //if (tag == "Wood") return 1;

        return 0;
    }

    internal void SetDustStormWithTimer(float _timerDuration, float _currentTimer)
    {
        m_WindStrength.Remap(
            0, _currentTimer,
            0, _timerDuration);
    }

    internal void PickedItem(int _i) { m_ItemCounter[_i]++; }

    internal void PickedItemSpecial() { m_specialItemPicked = true; }
    internal void WhaleTimeFinished() { m_whaleTimeFinished = true; m_Fighting = false; }

    internal void ResetPlayer()
    {
        m_player.transform.position = Vector3.zero;
        m_player.transform.rotation = Quaternion.identity;
    }
    internal void ActivateCharController() { m_controller.enabled = true; }
    internal void DeactivateCharController() { m_controller.enabled = false; }

    internal void ActivateSwordCollider() { m_sword.tag = "Untagged"; }
    internal void DeactivateSwordCollider() { m_sword.tag = "Sword"; }

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

#endregion