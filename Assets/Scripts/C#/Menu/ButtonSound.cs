using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Play Sound on Button Mouse over and Click
/// </summary>
[RequireComponent(typeof(AudioSource), typeof(Button))]
public class ButtonSound : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    AudioSource source;

    [SerializeField]
    AudioClip clickAudio, hoverAudio;

    void Start()
    {
        Initialise();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OverButton();
    }

    public void OnSelect(BaseEventData eventData)
    {
        OverButton();
    }


    void Initialise()
    {
        source = GetComponent<AudioSource>();
        GetComponent<Button>().onClick.AddListener(() => ButtonClicked());

        source.playOnAwake = false;
    }


    void ButtonClicked()
    {
        AudioManager.Instance?.TakeAudioToNextScene(clickAudio);
    }

    void OverButton()
    {
        source.clip = hoverAudio;
        source.Play();
    }
}
