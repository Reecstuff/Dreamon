using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class PathBlocker : MonoBehaviour
{
    [SerializeField]
    Transform pathBlockerEndPosition;

    AudioSource audioSource;
    DialogueTrigger dialogueTrigger;


    private void Start()
    {
        LoadState();
    }


    public void ClearPathBlocker()
    {
        PlayAudio();
        SaveState();
    }

    public void EndState()
    {
        if (dialogueTrigger = GetComponent<DialogueTrigger>())
        {
            dialogueTrigger.enabled = false;
            dialogueTrigger.TheEnd(false);
        }
        transform.position = pathBlockerEndPosition.position;
        transform.rotation = pathBlockerEndPosition.rotation;
    }

    protected virtual void SaveState()
    {
        if (SaveManager.instance)
        {
            if (!SaveManager.instance.HasInteracted(gameObject.name))
            {
                SaveManager.instance.Save(gameObject.name);
            }
        }
    }


    /// <summary>
    /// Load the state of this Trigger from savegame
    /// </summary>
    protected virtual void LoadState()
    {
        if(SaveManager.instance)
        if (SaveManager.instance.HasInteracted(gameObject.name))
        {
            ClearPathBlocker();
        }
    }


    void PlayAudio()
    {
        if(!audioSource)
            audioSource = GetComponent<AudioSource>();
        
        if (audioSource.clip)
            audioSource.Play();
    }
}
