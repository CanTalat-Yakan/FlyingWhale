using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "Scriptables/Audio Info", fileName = "Audio Info", order = 1)]
public class AudioInfo : ScriptableObject
{
    [Header("Main")]
    public AudioClip[] Music;

    [Header("Menu")]
    [Tooltip("0 = Move, 1 = Select")]
    public AudioClip[] Button = new AudioClip[2];
}

[Serializable] public struct AudioCollection { public AudioClip[] clips; }