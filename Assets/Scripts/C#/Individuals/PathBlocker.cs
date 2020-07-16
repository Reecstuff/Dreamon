using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class PathBlocker : MonoBehaviour
{

    AudioSource audioSource;
    DialogueTrigger dialogueTrigger;

    public void ClearPathBlocker()
    {
        PlayAudio();
        if (dialogueTrigger = GetComponent<DialogueTrigger>())
            dialogueTrigger.enabled = false;

        // Missing Animation
        transform.DOMoveY(transform.localPosition.y + 10, 0.1f);
    }


    void PlayAudio()
    {
        if(!audioSource)
            audioSource = GetComponent<AudioSource>();
        
        if (audioSource.clip)
            audioSource.Play();
    }
}
