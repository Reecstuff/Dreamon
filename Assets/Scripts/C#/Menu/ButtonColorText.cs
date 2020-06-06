using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonColorText : OverButton
{
    [SerializeField]
    TextMeshProUGUI text;

    Button button;

    protected override void Initialise()
    {
        base.Initialise();
        button = GetComponent<Button>();
    }

    protected override void OnButton()
    {
        base.OnButton();
        text.color = button.colors.highlightedColor;
    }

    protected override void ButtonClicked()
    {
        base.ButtonClicked();
        text.color = button.colors.pressedColor;
    }

    protected override void ButtonExit()
    {
        base.ButtonExit();
        text.color = button.colors.normalColor;
    }

}