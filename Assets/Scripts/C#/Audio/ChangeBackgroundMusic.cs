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

    void Start()
    {
        if (newMusic)
            AudioManager.Instance?.SetBackGroundMusic(newMusic);
    }
}
