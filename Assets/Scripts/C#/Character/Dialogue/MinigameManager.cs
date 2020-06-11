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
		minigame.SetActive(true);

		cameraController.offset = cameraPosition;
		cameraController.target = minigame.transform;
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

	public void StartNewDialog(int nextDialog)
	{
		//Stop game
		EndMinigame();
		GetComponent<DialogueTrigger>().currentDialogue = nextDialog;
		GetComponent<DialogueTrigger>().TriggerDialogue();
	}
}
