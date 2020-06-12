using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
	public GameObject mainCamera;
	public Vector3 cameraPosition;
	public GameObject minigame;

	CameraController cameraController;

	Vector3 playerOffset;

	int winRounds;
	int loseRounds;
	public int[] nextWinDialog;
	public int[] nextLoseDialog;

	private void Start()
	{
		cameraController = mainCamera.GetComponent<CameraController>();
		playerOffset = cameraController.offset;
	}

	/// <summary>
	/// Activates the minigame and point the camera at the minigame
	/// </summary>
	public void StartNewMinigame()
	{
		cameraController.SetOffset(cameraPosition, minigame.transform);

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
		GameObject.Find("Player").GetComponent<PlayerController>().SetFocus(this.GetComponent<Interactable>());

		cameraController.offset = playerOffset;
		cameraController.target = GameObject.Find("Player").transform;
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
