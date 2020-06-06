using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorImage : OverButton
{
    [SerializeField]
    Image image;

    Button button;

    protected override void Initialise()
    {
        base.Initialise();
        button = GetComponent<Button>();
    }

    protected override void OnButton()
    {
        base.OnButton();
        image.color = button.colors.highlightedColor;
    }

    protected override void ButtonClicked()
    {
        base.ButtonClicked();
        image.color = button.colors.pressedColor;
    }

    protected override void ButtonExit()
    {
        base.ButtonExit();
        image.color = button.colors.normalColor;
    }

}
