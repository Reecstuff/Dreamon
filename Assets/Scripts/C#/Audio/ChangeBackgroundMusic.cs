using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Set new Backgroundmusic on Sceneload
/// </summary>
public class ChangeBackgroundMusic : MonoBehaviour
{
    [SerializeField]
    AudioClip newMusic;

    [SerializeField]
    AudioClip FXSoundClip;

    void Start()
    {
        if (newMusic)
            AudioManager.Instance?.SetSourceClip(newMusic);

        if(FXSoundClip)
        if (!AudioManager.Instance.CompareClip(FXSoundClip))
        {
            AudioManager.Instance.SetSourceClip(FXSoundClip, 1);
        }
    }
}
