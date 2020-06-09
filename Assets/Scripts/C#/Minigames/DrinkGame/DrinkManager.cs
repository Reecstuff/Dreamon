﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrinkManager : MonoBehaviour
{
	public GameObject assignedTarget;

	public int drinkBottles;
	public int maxBottles;

	public int drunkBottles;
	public int maxAlcBottles;

	public float drinkTime = 3;
	public float maxBottleTimer = 3;
	public float bottleTime;


	public int winDialogue;
	public int loseDialogue;

	public Drink[] bottles;


	private void Start()
	{
		bottles = GetComponentsInChildren<Drink>();
		bottleTime = maxBottleTimer;
	}

	private void Update()
	{
		CheckGame();
		TimingBottle();
	}

	void TimingBottle()
	{
		bottleTime -= Time.deltaTime;

		if (bottleTime <= 0)
		{
			bottleTime = maxBottleTimer;
			RandomAlc();
		}
	}

	void CheckGame()
	{
		drinkTime -= Time.deltaTime;

		if (drinkTime >= 0)
		{
			if (drinkBottles == maxBottles)
			{
				Win();
			}
			if (drunkBottles == maxAlcBottles)
			{
				//Drink to much alcohole
				Lost();
			}
		}
		else
		{
			Lost();
		}
	}

	public void Win()
	{
		//Win the DontGetDrunk game
		Debug.Log("You Win");
		//Dialogue.Opinion opinion = dialogue.opinion[selectedOpinion];

		//Stop game
		assignedTarget.GetComponent<MinigameManager>().EndMinigame();
		assignedTarget.GetComponent<DialogueTrigger>().TriggerDialogue();
		assignedTarget.GetComponent<DialogueTrigger>().currentDialogue = winDialogue;
	}

	public void Lost()
	{
		//Lose the DontGetDrunk game
		Debug.Log("You Lose");
		//Dialogue.Opinion opinion = dialogue.opinion[selectedOpinion];

		//Stop game
		assignedTarget.GetComponent<MinigameManager>().EndMinigame();
		assignedTarget.GetComponent<DialogueTrigger>().TriggerDialogue();
		assignedTarget.GetComponent<DialogueTrigger>().currentDialogue = loseDialogue;
	}

	void RandomAlc()
	{
		// Reset All Bottles
		for (int i = 0; i < bottles.Length; i++)
		{
			// Reset to Alc
			bottles[i].SetBottle();
		}

		// Set Non Alc Bottles
		// Can be double index
		for (int i = 0; i < Random.Range(1, bottles.Length); i++)
		{
			bottles[Random.Range(0, bottles.Length)].SetBottle(false);
		}
	}
}
