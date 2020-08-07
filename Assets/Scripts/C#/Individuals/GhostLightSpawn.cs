using DG.Tweening;
using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.Lumin;
using System.Collections.Generic;

public class GhostLightSpawn : MonoBehaviour
{
    [SerializeField]
    List<GameObject> ghostLights;

    [SerializeField]
    float speed = 0.8f;

    Light[] lightComponents;
    Behaviour[] halos;

    Vector3 currentPoint;

    void Start()
    {
        InitValues();
    }

    void InitValues()
    {
        lightComponents = transform.GetComponentsInChildren<Light>();
        halos = new Behaviour[lightComponents.Length];
        for (int i = 0; i < lightComponents.Length; i++)
        {
            halos[i] = (Behaviour)lightComponents[i].GetComponent("Halo");
        }
        ResetLight(0);

        for (int i = 0; i < ghostLights.Count; i++)
        {
            ghostLights[i].SetActive(false);
        }

        currentPoint = ghostLights[0].transform.position;
    }

    private void OnEnable()
    {
        Light(1);
    }

    private void OnDisable()
    {
        DisableLight(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check Player
        if (other.GetComponent<PlayerController>())
        {
            currentPoint = other.transform.position;
            Light(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check Player
        if (other.GetComponent<PlayerController>())
        {
            currentPoint = other.transform.position;
            Debug.Log("Exit");
            Light(0);
        }
    }


    public void Light(int size)
    {
        if(gameObject.activeInHierarchy && ghostLights != null && lightComponents != null && ghostLights.Count > 0 && lightComponents.Length > 0)
        {
            StopAllCoroutines();
            ResetLight(size);
            StartCoroutine(WaitForLight(size));
        }
    }

    private void ResetLight(int size)
    {
        GameObject[] buffer = ghostLights.FindAll(g => g.activeInHierarchy == size < 1).ToArray();

        if (buffer.Length <= 0)
            return;

        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i].transform.localScale = Vector3.one * (size >= 1 ? 0 : 1);
            halos[ghostLights.IndexOf(buffer[i])].enabled = size < 1;
            lightComponents[ghostLights.IndexOf(buffer[i])].intensity = 1 * (size >= 1 ? 0 : 1);
        }
    }

    void DisableLight(int size)
    {

        if (gameObject.activeInHierarchy && ghostLights != null && lightComponents != null && ghostLights.Count > 0 && lightComponents.Length > 0)
        {
            for (int i = 0; i < ghostLights.Count; i++)
            {
                ghostLights[i].transform.localScale = Vector3.one * (size >= 1 ? 0 : 1);
                halos[i].enabled = size < 1;
                lightComponents[i].intensity = 1 * (size >= 1 ? 0 : 1);
                ghostLights[i].SetActive(size >= 1);
            }
        }
    }


    IEnumerator WaitForLight(int size)
    {

        GameObject currentLight = ClosestLight(size < 1);

        do
        {
            if(size >= 1)
            currentLight.SetActive(true);


            currentLight.transform.DOScale(size, speed);

            lightComponents[ghostLights.IndexOf(currentLight)].DOIntensity(size, speed);

            yield return new WaitForSeconds(speed);

            if (size < 1)
                currentLight.SetActive(false);

            halos[ghostLights.IndexOf(currentLight)].enabled = size >= 1;
            currentLight = ClosestLight(size < 1);


        } while (currentLight && currentLight.activeSelf == size < 1);
    }

    GameObject ClosestLight(bool isActive)
    {

        GameObject[] buffer = ghostLights.FindAll(g => g.activeInHierarchy == isActive).ToArray();

        if (buffer.Length <= 0)
            return null;

        GameObject closestLight = buffer[0];


        for (int i = 0; i < buffer.Length; i++)
        {
            if(
                Vector2.Distance(
                    new Vector2(currentPoint.x, currentPoint.z), 
                    new Vector2(buffer[i].transform.position.x, buffer[i].transform.position.z))
                < 
                Vector2.Distance(
                    new Vector2(currentPoint.x, currentPoint.z), 
                    new Vector2(closestLight.transform.position.x, closestLight.transform.position.z)))
            {
                closestLight = buffer[i];
            }
        }

        currentPoint = closestLight.transform.position;

        return closestLight;
    }
}
