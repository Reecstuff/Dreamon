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
    AudioClip clip;

    int currentTimeSamples;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(AudioManager.Instance && clip)
        {
            if(!AudioManager.Instance.CompareClip(clip))
            {
                AudioManager.Instance.SetBackGroundMusic(clip, currentTimeSamples);
            }
        }
    }
}