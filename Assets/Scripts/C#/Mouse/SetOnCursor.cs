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
    bool isMenu = false;

    RaycastHit hit;
    Vector3 nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (!cam)
            cam = Camera.main;

        Cursor.visible = false;
        var main = GetComponent<ParticleSystem>().main;
        main.useUnscaledTime = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.timeScale > 0 && !isMenu)
        {
            if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition),  out hit, maxDistance))
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
            nextPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, maxDistance));
        }
        transform.position = nextPosition;
    }
}
