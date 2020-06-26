using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ParticleSystem))]
public class SetOnCursor : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    float yOffset = 0.2f;

    [SerializeField]
    float maxDistance = 20;

    [SerializeField]
    float minDistance = 6;

    [SerializeField]
    ParticleSystem cursorParticleSystem;

    [SerializeField]
    Light cursorLight;

    [SerializeField]
    LensFlare flare;

    [SerializeField]
    Color interactColor = Color.blue;

    RaycastHit hit;
    Vector3 nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        if (!cam)
            cam = Camera.main;

        Cursor.visible = false;
        flare = GetComponent<LensFlare>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale > 0 && !Cursor.visible)
        {
            if(cursorParticleSystem.isPaused)
            {
                InterruptParticleSystem(false);

            }

            nextPosition = RayCastPosition();
        }
        else
        {
            InterruptParticleSystem(true);
            return;
        }
        transform.position = nextPosition;
    }

    Vector3 RayCastPosition()
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, maxDistance, -1, QueryTriggerInteraction.Ignore))
        {
            CheckForInteractable();
            return new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);
        }
        else
        {
            return cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, maxDistance));
        }
    }

    void InterruptParticleSystem(bool stop)
    {
        if(stop)
        {
            cursorParticleSystem.Pause();
            cursorParticleSystem.Clear();
            cursorLight.enabled = false;
            flare.enabled = false;
        }
        else
        {
            cursorParticleSystem.Play();
            cursorLight.enabled = true;
            flare.enabled = true;
        }
    }

    void CheckForInteractable()
    {
        Interactable interactable = hit.transform.GetComponent<Interactable>();

        if (interactable)
        {
            if(!interactable.isClick)
            {
                if(cursorLight.color != interactColor)
                {
                    cursorLight.color = interactColor;
                    flare.color = interactColor;
                }
            }
        }
        else
        {
            if(cursorLight.color != Color.white)
            {
                cursorLight.color = Color.white;
                flare.color = Color.white;
            }
        }
    }
}
