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
    ParticleSystem particleSystem;

    [SerializeField]
    Light light;

    [SerializeField]
    LensFlare flare;


    RaycastHit hit;
    RaycastHit secondHit;
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
            if(particleSystem.isPaused)
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
            particleSystem.Pause();
            particleSystem.Clear();
            light.enabled = false;
            flare.enabled = false;
        }
        else
        {
            particleSystem.Play();
            light.enabled = true;
            flare.enabled = true;
        }
    }
}
