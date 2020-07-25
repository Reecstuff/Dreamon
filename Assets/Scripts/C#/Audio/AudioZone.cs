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


    /// <summary>
    /// Set Background & FXclip to this Clip if not already set
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (AudioManager.Instance)
        {
            if (BackGroundMusicClip && !AudioManager.Instance.CompareClip(BackGroundMusicClip))
            {
                AudioManager.Instance.SetSourceClip(BackGroundMusicClip, 0, currentBackTimeSamples);
            }
            if (FXSoundClip && !AudioManager.Instance.CompareClip(FXSoundClip))
            {
                AudioManager.Instance.SetSourceClip(FXSoundClip, 1, currentFXTimeSamples);
            }
        }
    }

    /// <summary>
    /// Set clips to no clip on exit zone
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (AudioManager.Instance)
        {
            currentBackTimeSamples = AudioManager.Instance.GetSamples(0);
            currentFXTimeSamples = AudioManager.Instance.GetSamples(1);

            AudioManager.Instance.SetSourceClip(null, 0, currentBackTimeSamples);
            AudioManager.Instance.SetSourceClip(null, 1, currentFXTimeSamples);
        }
    }

    /// <summary>
    /// On Disable set Ambience off
    /// </summary>
    private void OnDisable()
    {
        if(AudioManager.Instance)
        {
            AudioManager.Instance.SetSourceClip(null, 1);
        }
    }
}