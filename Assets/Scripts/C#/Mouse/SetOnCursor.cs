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
    ParticleSystem particleSystem;

    [SerializeField]
    Light light;


    RaycastHit hit;
    Vector3 nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (!cam)
            cam = Camera.main;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.timeScale > 0 && !Cursor.visible)
        {
            if(particleSystem.isPaused)
            {
                particleSystem.Play();
                light.enabled = true;
            }

            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition),  out hit, maxDistance))
            {

                nextPosition = new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);
            }
            else
            {
                nextPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, maxDistance));
            }
        }
        else
        {
            particleSystem.Pause();
            particleSystem.Clear();
            light.enabled = false;
            return;
        }
        transform.position = nextPosition;
    }
}
