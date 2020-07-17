using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ChangeFootstepTrigger : MonoBehaviour
{
    [SerializeField]
    int newFootstepsIndex = -1;

    [SerializeField]
    int normalFootstepsIndex = -1;

    PlayerMotor currentPlayermotor;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (newFootstepsIndex >= 0 && normalFootstepsIndex >= 0)
            if (currentPlayermotor = other.GetComponent<PlayerMotor>())
            {
                if (currentPlayermotor.GetFootstepIndex() == newFootstepsIndex)
                    currentPlayermotor.ChangeFootstepsSound(normalFootstepsIndex);
                else
                    currentPlayermotor.ChangeFootstepsSound(newFootstepsIndex);
            }
    }
}
