using System;
using UnityEngine;


/// <summary>
/// Creates a Zone where the Player can 
/// hear 2D Backgroundmusic only in this Zone
/// </summary>
[RequireComponent(typeof(SphereCollider))]
public class AudioZone : MonoBehaviour
{
    [SerializeField]
    AudioClip BackGroundMusicClip;

    [SerializeField]
    AudioClip FXSoundClip;

    int currentBackTimeSamples;
    int currentFXTimeSamples;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (AudioManager.Instance)
        {
            if (BackGroundMusicClip && FXSoundClip)
            {
                if (!AudioManager.Instance.CompareClip(BackGroundMusicClip))
                {
                    AudioManager.Instance.SetSourceClip(BackGroundMusicClip, currentBackTimeSamples);
                }
                if (!AudioManager.Instance.CompareClip(FXSoundClip))
                {
                    AudioManager.Instance.SetSourceClip(FXSoundClip, 1, currentFXTimeSamples);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (AudioManager.Instance)
        {
            currentBackTimeSamples = AudioManager.Instance.GetSamples(0);
            currentFXTimeSamples = AudioManager.Instance.GetSamples(1);
        }
    }
}