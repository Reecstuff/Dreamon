using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

/// <summary>
/// Needs to be Switched
/// </summary>
public class Dementum : DialogueTrigger
{
    [SerializeField]
    GameObject[] objectsToDeactivate;

    [SerializeField]
    GameObject lostCandleLight;

    [SerializeField]
    float deathAnimationTime = 6;

    [SerializeField]
    PolterGeist Taverpoltergeist;

    [SerializeField]
    PathBlocker blocker;

    [SerializeField]
    Dialogue[] lostDialogue;

    [SerializeField]
    Transform lostCameraTransform;

    public override void TheEnd(bool isLose)
    {
        base.TheEnd(isLose);

        // Both Endings:
        // Tür geht auf
        blocker?.ClearPathBlocker();
        // Poltergeist off
        if (Taverpoltergeist)
            Taverpoltergeist.enabled = false;
        // Ambience Music off
        if(AudioManager.Instance)
            AudioManager.Instance.SetSourceClip(null, 1, AudioManager.Instance.GetSamples(1));
        // Dementum dissapears
        DementumDisapears();

        // Lost the Game:
        if(isLose)
        {
            // Remove all Chairs, etc.
            // + Remove light sources
            DissapearObjects();
            // Except one Light
            // Activate this Light beside Elios
            lostCandleLight.SetActive(true);

            // Backgroundmusic off
            if (AudioManager.Instance)
                AudioManager.Instance.SetSourceClip(null, 0, AudioManager.Instance.GetSamples(0));

            // Lost Dialogue
            LostDialogue();
            
        }
        else
        {
            // Won the Game:
            // Light near Dementum disappears
            // This Light is the first in the List
            objectsToDeactivate.First().SetActive(false);
        }

        // Set Gameobject inactive
        Invoke(nameof(SetInactive), deathAnimationTime + 0.5f);

    }

    void DementumDisapears()
    {
        // Shrink and Shake
        Sequence s = DOTween.Sequence();

        s.Append(transform.DOScale(Vector3.zero, deathAnimationTime));
        s.Join(transform.DOShakeRotation(deathAnimationTime));
        s.Play();
    }

    void DissapearObjects()
    {
        if (objectsToDeactivate.Length > 0)
        {
            for (int i = 0; i < objectsToDeactivate.Length; i++)
            {
                objectsToDeactivate[i].SetActive(false);
            }
        }
    }

    void LostDialogue()
    {
        if (lostDialogue.Length > 0)
        {
            dialogue = lostDialogue;

            // Reset Values
            dialogueManager.cameraController.CancelResetCameraToPlayer();
            hasInteracted = false;
            currentDialogue = 0;
            camPosition = lostCameraTransform;

            // Start new Dialogue
            TriggerDialogue();
        }
    }
}
