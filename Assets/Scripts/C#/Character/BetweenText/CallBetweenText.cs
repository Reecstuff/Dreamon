using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CallBetweenText : MonoBehaviour
{
    [SerializeField]
    BetweenText betweenText;

    public void CallBetween(TimedTalk[] timedTalk)
    {
        betweenText.SetText(timedTalk);
    }

    public void EndCall()
    {
        betweenText.ClearText();
    }
}
