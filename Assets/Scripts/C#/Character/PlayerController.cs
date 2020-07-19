using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Control player by input
/// </summary>
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;
    public LayerMask movementMask;
    
    public Transform facePoint;

    [HideInInspector]
    public PlayerMotor motor;

    public CallBetweenText callBetween;

    Camera cam;



    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();

        // Subscribe to load and autoSave event
        if (SaveManager.instance)
        {
            SaveManager.instance.OnLoadSave += LoadPlayer;
            SaveManager.instance.currentAutoSave.OnAutoSave += SavePlayer;
        }
    }


    void Update()
    {
        CheckForInteractable();
        MoveCharacter();
    }

    /// <summary>
    /// Load player in scene
    /// </summary>
    public void LoadPlayer()
    {
        // Fixing Positionbug
        GetComponent<NavMeshAgent>().enabled = false;

        transform.position = SaveManager.instance.GetPlayerPosition();
        motor.SetAnimationState(SaveManager.instance.GetAnimationState());
        motor.ChangeFootstepsSound(SaveManager.instance.GetFootstepIndex());
        GetComponent<NavMeshAgent>().enabled = true;
    }

    /// <summary>
    /// Save all data from player
    /// </summary>
    public void SavePlayer()
    {
        SaveManager.instance.Save(transform.position, motor.GetAnimationState(), motor.GetFootstepIndex());
    }

    /// <summary>
    /// Check for Interactable on Leftclick
    /// </summary>
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

    /// <summary>
    /// Move Character in direction of Mousecursor
    /// </summary>
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

    /// <summary>
    /// Set interactable Focus of Character
    /// </summary>
    /// <param name="newFocus"></param>
    public void SetFocus(Interactable newFocus)
    {
        if (newFocus.hasInteracted)
            return;

        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            focus = newFocus;

            // Move to Interactable
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

    private void OnDisable()
    {
        // Unsubscribe events
        if (SaveManager.instance)
        {
            SaveManager.instance.OnLoadSave -= LoadPlayer;
            SaveManager.instance.currentAutoSave.OnAutoSave -= SavePlayer;
        }
    }
}
