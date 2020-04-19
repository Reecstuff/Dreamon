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
        InitialiseValues();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        OverButton();
    }

    public void OnSelect(BaseEventData eventData)
    {
        OverButton();
    }


    void InitialiseValues()
    {
        source = GetComponent<AudioSource>();
        GetComponent<Button>().onClick.AddListener(() => ButtonClicked());

        source.playOnAwake = false;
    }


    void ButtonClicked()
    {
        source.clip = clickAudio;
        source.Play();
    }

    void OverButton()
    {
        source.clip = hoverAudio;
        source.Play();
    }


}
