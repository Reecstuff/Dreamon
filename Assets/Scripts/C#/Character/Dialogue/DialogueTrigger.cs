using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DialogueTrigger : Interactable
{
	public Dialogue[] dialogue;

	public int currentDialogue;

	public MinigameManager minigameManager;

	public Transform camPosition;

	protected DialogueManager dialogueManager;

	public AudioSource source;

	public bool? isLost;

	[SerializeField]
	protected int saveIndex;


	protected override void InitValues()
	{
		base.InitValues();
		dialogueManager = FindObjectOfType<DialogueManager>();
		if (SaveManager.instance)
			SaveManager.instance.OnLoadSave += LoadState;
	}


	//Starts interacting with the player
	public override void Interact()
	{
		PlaySound();

		TriggerDialogue();
	}

	//Starts the dialog
	public void TriggerDialogue()
	{
		if (hasInteracted == false && !dialogueManager.currentDialogObject || !dialogueManager.selectMinigame)
		{
			dialogueManager.StartDialogue(this);
			hasInteracted = true;
		}
	}

	public virtual void TheEnd(bool isLose)
	{
		SaveState(!isLose);

		// Do Something
	}

	protected virtual void PlaySound()
	{
		if (source)
			if (source.clip)
				source.Play();
	}

	protected virtual void SetInactive()
	{
		gameObject.SetActive(false);
	}

	protected virtual void SaveState(bool result)
	{
		if(SaveManager.instance)
		{
			if(!SaveManager.instance.HasInteracted(saveIndex))
			{
				SaveManager.instance.Save(saveIndex, result);
			}
		}
	}


	/// <summary>
	/// Load the state of this Trigger from savegame
	/// </summary>
	protected virtual void LoadState()
	{
		hasInteracted = SaveManager.instance.HasInteracted(saveIndex);
		isLost = !SaveManager.instance.HasResult(saveIndex);
		if (hasInteracted)
		{
			CancelInvoke();
			TheEnd((bool)isLost);
		}
	}

	private void OnDisable()
	{
		if (SaveManager.instance)
			SaveManager.instance.OnLoadSave -= LoadState;
	}
}
