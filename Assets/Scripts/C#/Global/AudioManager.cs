using DG.Tweening;
using System.Collections;
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

    /// <summary>
    /// Set the current Backgroundmusic
    /// </summary>
    /// <param name="clip">New Backgroundmusic</param>
    public void SetBackGroundMusic(AudioClip clip, int timeSamples = 0)
    {
        if(!backgroundMusic.clip || !backgroundMusic.clip.Equals(clip))
        {
            if (timeSamples > 0)
                backgroundMusic.timeSamples = timeSamples;

            backgroundMusic.clip = clip;
            backgroundMusic.Play();
        }
    }

    public void PitchManual(float pitch)
    {
        backgroundMusic.pitch = pitch;
    }

    public bool CompareClip(AudioClip clip)
    {
        return backgroundMusic.clip.Equals(clip);
    }
}
