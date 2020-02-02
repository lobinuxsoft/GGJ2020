using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioKey
{
    AcelerationStart,
    Crash,
    Door,
    Fire,
    FireStopped,
    Idle,
    Repair,
    Ambient,
    Spin,
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance && instance != this)
            Destroy(this);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        instance.PlayAudio(instance.AmbientAudio, AudioKey.Ambient);
    }

    public AudioSource shipAudio;
    public AudioSource playerAudio;
    public AudioSource SFXAudio;
    public AudioSource AmbientAudio;

    public AudioClip AcelerationStart;
    public AudioClip Crash;
    public AudioClip Door;
    public AudioClip Fire;
    public AudioClip FireStopped;
    public AudioClip Idle;
    public AudioClip Repair;
    public AudioClip Ambient;
    public AudioClip Spin;

    public void PlayAudio(AudioSource source, AudioKey key)
    {
        switch (key)
        {
            case AudioKey.AcelerationStart:
                source.PlayOneShot(AcelerationStart);
                break;
            case AudioKey.Crash:
                source.PlayOneShot(Crash);
                break;
            case AudioKey.Door:
                source.PlayOneShot(Door);
                break;
            case AudioKey.Fire:
                source.clip = Fire;
                source.Play();
                break;
            case AudioKey.FireStopped:
                source.PlayOneShot(FireStopped);
                break;
            case AudioKey.Idle:
                source.PlayOneShot(Idle);
                break;
            case AudioKey.Repair:
                source.PlayOneShot(Repair);
                break;
            case AudioKey.Ambient:
                source.clip = Ambient;
                source.Play();
                break;
            case AudioKey.Spin:
                source.PlayOneShot(Spin);
                break;
        }
    }
}
