using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOManager : MonoBehaviour
{
	public GameObject[] hiddenObjects;
	public int foundObjects;

	public GameObject assignedTarget;

	public float searchTime = 60;

	public int winDialog;
	public int loseDialog;

	private void Update()
	{
		searchTime -= Time.deltaTime;

		if (searchTime <= 0)
		{
			EndGame(loseDialog);
		}
		else if (hiddenObjects.Length == foundObjects)
		{
			EndGame(winDialog);
		}
	}

	void EndGame(int dialogNumber)
	{
		//Stop game
		assignedTarget.GetComponent<MinigameManager>().EndMinigame();
		assignedTarget.GetComponent<DialogueTrigger>().currentDialogue = dialogNumber;
		assignedTarget.GetComponent<DialogueTrigger>().TriggerDialogue();
	}
}
