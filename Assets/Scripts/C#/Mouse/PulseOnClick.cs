using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Light))]
public class PulseOnClick : MonoBehaviour
{
    [SerializeField]
    float maxIntensity = 2;

    [SerializeField]
    float time = 0.5f;

    Light light;

    void Start()
    {
        light = GetComponent<Light>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            CancelInvoke(nameof(Reverse));
            light.DOIntensity(maxIntensity, time);
            Invoke(nameof(Reverse), time);
        }
    }

    void Reverse()
    {
        light.DOIntensity(1, time);
    }
}
