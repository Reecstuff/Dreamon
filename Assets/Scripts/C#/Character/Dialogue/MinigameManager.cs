using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueTrigger))]
public class MinigameManager : MonoBehaviour
{
	public GameObject mainCamera;
	public Transform cameraPosition;
	public GameObject minigame;

	CameraController cameraController;


	public int winRounds;
	int loseRounds;
	public int[] nextWinDialog;
	public int[] nextLoseDialog;

	PlayerController player;
	DialogueTrigger dialogTrigger;

	private void Start()
	{
		cameraController = mainCamera.GetComponent<CameraController>();
		player = GameObject.FindObjectOfType<PlayerController>();
		dialogTrigger = GetComponent<DialogueTrigger>();
	}

	/// <summary>
	/// Activates the minigame and point the camera at the minigame
	/// </summary>
	public void StartNewMinigame()
	{
		cameraController.MoveToFixedPosition(cameraPosition.position, minigame.transform);
		player.motor.StopAgent();
		Invoke(nameof(SetMinigameActive), cameraController.drivingTime);
	}

	void SetMinigameActive()
	{
		minigame.SetActive(true);
	}

	/// <summary>
	/// Deactivate the mini-game and point the camera back at the player
	/// </summary>
	public void EndMinigame()
	{
		minigame.SetActive(false);

		//Focusing the demon
		player.SetFocus(this.GetComponent<Interactable>());
		player.motor.ResumeAgent();


		cameraController.MoveToFixedPosition(dialogTrigger.CameraPosition.position, dialogTrigger.interactionTransform);
	}

	public void StartNextDialog(bool isWin)
	{
		if (isWin == true)
		{
			for (int i = 0; i < nextWinDialog.Length; i++)
			{
				if (winRounds == i)
				{
					//Stop losing round
					EndMinigame();
					GetComponent<DialogueTrigger>().currentDialogue = nextWinDialog[i];
					GetComponent<DialogueTrigger>().TriggerDialogue();
				}
			}

			winRounds++;
		}
		else if (isWin == false)
		{
			for (int i = 0; i < nextLoseDialog.Length; i++)
			{
				if (loseRounds == i)
				{
					//Stop losing round
					EndMinigame();
					GetComponent<DialogueTrigger>().currentDialogue = nextLoseDialog[i];
					GetComponent<DialogueTrigger>().TriggerDialogue();
				}
			}

			loseRounds++;
		}
	}
}
