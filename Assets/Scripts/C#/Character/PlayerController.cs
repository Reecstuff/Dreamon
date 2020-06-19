using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;
    public LayerMask movementMask;
    
    public Transform facePoint;

    [HideInInspector]
    public PlayerMotor motor;

    Camera cam;



    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInteractable();
        MoveCharacter();
    }

    void CheckForInteractable()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, -1, QueryTriggerInteraction.Ignore))
            {
                //Check if we hit an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                //If we did set it as our focus
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    void MoveCharacter()
    {
        if (Input.GetMouseButton(0) && focus == null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask, QueryTriggerInteraction.Ignore))
            {
                //Move our player to what we hit
                motor.MoveToPoint(hit.point);
            }
        }
    }

    public void SetFocus(Interactable newFocus)
    {
        if (newFocus.GetComponent<DialogueTrigger>())
        {
            if (newFocus.GetComponent<DialogueTrigger>().isClick)
                return;
        }

        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    public void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }

        focus = null;
        motor.StopFollowingTarget();
    }
}
