﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource), typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    AudioMixer mainMixer;

    public string[] volumeStrings;

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

    /// <summary>
    /// Play Clip on Audiosource that isn't going to destroy on Scenechange
    /// </summary>
    /// <param name="c">AudioClip that is going to be played</param>
    public void TakeAudioToNextScene(AudioClip c)
    {
        nextSceneSource.clip = c;
        nextSceneSource.Play();
    }

    /// <summary>
    /// Get the current volumesetting
    /// </summary>
    /// <param name="index">Index of Volume in volumeStrings</param>
    public float GetCurrentVolume(int index)
    {
        float currentVolume = 0;

        mainMixer.GetFloat(volumeStrings[index], out currentVolume);

        return currentVolume;
    }

    /// <summary>
    /// Set the current volumesetting
    /// </summary>
    /// <param name="index">Index of Volume in volumeStrings</param>
    /// <param name="volume">Volume its going to be</param>
    public void SetCurrentVolume(int index, float volume)
    {
        // Mute Volume, I could still hear it
        if (volume <= -40)
            volume = -80;

        mainMixer.SetFloat(volumeStrings[index], volume);
    }
}
