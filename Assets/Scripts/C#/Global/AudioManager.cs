using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    AudioSource backgroundMusic, nextSceneSource;

    private void Awake()
    {
        MakeSingelton();
    }

    private void Start()
    {
        Initialise();
    }


    /// <summary>
    /// This Object should only be once in the Game
    /// </summary>
    void MakeSingelton()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Initialise()
    {
        backgroundMusic = GetComponents<AudioSource>()[0];
        nextSceneSource = GetComponents<AudioSource>()[1];

        backgroundMusic.loop = true;
        backgroundMusic.playOnAwake = true;
    }


    public void TakeAudioToNextScene(AudioClip c)
    {
        nextSceneSource.clip = c;
        nextSceneSource.Play();
    }
}
