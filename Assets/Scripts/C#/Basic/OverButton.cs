using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OverButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerExitHandler
{
    protected void Start()
    {
        Initialise();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnButton();
    }

    public void OnSelect(BaseEventData eventData)
    {
        OnButton();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonExit();
    }


    protected virtual void Initialise()
    {
        GetComponent<Button>().onClick.AddListener(() => ButtonClicked());
    }


    protected virtual void ButtonClicked()
    {

    }

    protected virtual void OnButton()
    {

    }

    protected virtual void ButtonExit()
    {

    }

   
}