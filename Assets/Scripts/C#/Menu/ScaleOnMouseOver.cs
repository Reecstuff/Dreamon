using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScaleOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    [SerializeField]
    float scaleSpeed = 0.18f;

    [SerializeField]
    Vector3 scaleVector = new Vector3(1.07f, 1.07f, 1);

    [SerializeField]
    float backScaleFactor = 100;

    RectTransform recT;

    bool scaled = false;
    Coroutine coroutine;

    void Start()
    {
        recT = GetComponent<RectTransform>();
    }

    /// <summary>
    /// For better Visualisation scaling UI Element on Mouse over
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        scaled = false;
        ScaleButton();

    }

    /// <summary>
    /// Rescaling UI Element on Mouse exit
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        scaled = true;
        ScaleButton();

    }

    void ScaleButton()
    {

        if(scaled)
        {
            //if (coroutine != null)
            //    StopCoroutine(coroutine);

            recT.DOKill();
            recT.localScale = Vector3.one;

            //coroutine = StartCoroutine(LerpVector(Vector3.one, scaleSpeed / backScaleFactor));
        }
        else
        {
            //if (coroutine != null)
            //    StopCoroutine(coroutine);

            recT.DOKill();
            recT.DOScale(scaleVector, scaleSpeed);

            //coroutine = StartCoroutine(LerpVector(scaleVector, scaleSpeed));
        }
    }

    /// <summary>
    /// Lerp the localScale to targetScale
    /// </summary>
    IEnumerator LerpVector(Vector3 targetScale, float time)
    {
        float lerpvalue = 0;
        Vector3 lerpScale = recT.localScale;

        while (lerpvalue < 1)
        {
            lerpvalue += Time.deltaTime / time;
            recT.localScale = Vector3.Lerp(lerpScale, targetScale, lerpvalue);
            yield return new WaitForSecondsRealtime(0.008f);
        }
    }
}
