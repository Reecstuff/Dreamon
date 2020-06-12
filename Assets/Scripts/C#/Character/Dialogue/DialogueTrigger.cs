﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DialogueTrigger : Interactable
{
	public Dialogue[] dialogue;

	public int currentDialogue;

	public MinigameManager minigameManager;

	public bool isClick = false;

	// Delete this, it is already in Dementum
	[SerializeField]
	GameObject[] objectsToDeactivate;

	//Starts interacting with the player
	public override void Interact()
	{
		TriggerDialogue();
	}

	//Starts the dialog
	public void TriggerDialogue()
	{
		if (isClick == false)
		{
			FindObjectOfType<DialogueManager>().StartDialogue(this);
			isClick = true;
		}
	}

	public virtual void TheEnd()
	{
		float animationTime = 6;

		Sequence s = DOTween.Sequence();
		// Fly Away

		s.Append(transform.DOScale(Vector3.zero, animationTime));
		s.Join(transform.DOShakeRotation(animationTime));
		s.Play();

		if (objectsToDeactivate.Length > 0)
		{
			for (int i = 0; i < objectsToDeactivate.Length; i++)
			{
				objectsToDeactivate[i].SetActive(false);
			}
		}

		Invoke(nameof(SetInactive), animationTime + 0.5f);
	}

	protected virtual void SetInactive()
	{
		gameObject.SetActive(false);
	}
}
