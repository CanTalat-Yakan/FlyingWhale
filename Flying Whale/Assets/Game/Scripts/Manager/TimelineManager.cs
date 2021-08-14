using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager Instance { get; private set; }

    [SerializeField] PlayableDirector m_director;
    [SerializeField] internal PlayableAsset m_Beginning;
    [SerializeField] internal PlayableAsset m_Ending;

    public bool m_IsPlaying { get => m_director.state == PlayState.Playing; }


    void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    public void Play(PlayableAsset _tl_asset)
    {
        if (!_tl_asset) return;

        StartCoroutine(ActivateTL(_tl_asset));

        m_director.Play();

        StartCoroutine(DeactivateTL(m_director.playableAsset.duration));
    }

    IEnumerator ActivateTL(PlayableAsset _tl_asset)
    {
        GameManager.Instance.m_CMVCamera.gameObject.SetActive(false);

        GameManager.Instance.DeactivateCharController();

        m_director.playableAsset = _tl_asset;

        m_director.initialTime = 0;

        yield return null;
    }
    IEnumerator DeactivateTL(double _offset)
    {
        float timeStamp = Time.time;
        yield return new WaitUntil(() => Time.time - timeStamp > _offset); 

        GameManager.Instance.m_CMVCamera.gameObject.SetActive(true); 

        GameManager.Instance.ActivateCharController();
    }
}