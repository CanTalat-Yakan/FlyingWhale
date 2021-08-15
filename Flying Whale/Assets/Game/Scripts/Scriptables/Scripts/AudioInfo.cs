using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "Scriptables/Audio Info", fileName = "Audio Info", order = 1)]
public class AudioInfo : ScriptableObject
{
    [Header("Main")]
    public AudioClip Music;
    public AudioClip Ambient;
    public AudioClip Environment;
    public AudioClip BattleMusic;

    [Header("Menu")]
    public AudioCollection Button;
    public AudioClip Panel;

    [Header("Player")]
    public AudioCollection[] Footsteps;
    public AudioCollection[] Landing;
    public AudioCollection Jump;
    public AudioCollection Damage;
    public AudioCollection Groan;
    public AudioCollection Swoosh;
    public AudioCollection Sword;

    [Header("Battle")]
    public AudioCollection Cannon;
}

[Serializable] public struct AudioCollection { public AudioClip[] clips; }