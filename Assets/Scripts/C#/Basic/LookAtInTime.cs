using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtInTime : MonoBehaviour
{
    [SerializeField]
    Transform lookAtStart;

    [SerializeField]
    float timeAtStart = 3;

    void Start()
    {
        Look(lookAtStart.position, timeAtStart);
    }

    public void Look(Vector3 position, float time)
    {
        transform.DOLookAt(position, time);
    }
}
