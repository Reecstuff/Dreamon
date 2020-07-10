using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CallBetweenText : MonoBehaviour
{
    [SerializeField]
    BetweenText betweenText;

    [SerializeField]
    TimedTalk[] introTalk;

    void Start()
    {
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1);

        betweenText.SetText(introTalk);
    }

    public void CallBetween(TimedTalk[] timedTalk)
    {
        betweenText.SetText(timedTalk);
    }
}
