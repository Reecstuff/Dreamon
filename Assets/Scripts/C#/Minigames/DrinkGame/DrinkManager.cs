﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrinkManager : MonoBehaviour
{
	[SerializeField]
	Canvas MiniGameCanvas;
	
	public TextMeshProUGUI timerText;
	public TextMeshProUGUI drinkBottleText;
	public TextMeshProUGUI drunkBottleText;

	public int drinkBottles;
	public int maxBottles;

	public int drunkBottles;
	public int maxAlcBottles;

	public float drinkTime = 3;
	private float startDrinkTime;

	public float maxBottleTimer = 3;
	public float bottleTime;

	public Drink[] bottles;

	public GameObject assignedTarget;

	private void Start()
	{
		startDrinkTime = drinkTime;
		bottles = GetComponentsInChildren<Drink>();
		bottleTime = maxBottleTimer;
		RandomAlc();
	}

	private void OnEnable()
	{
		MiniGameCanvas.gameObject.SetActive(true);
	}

	private void OnDisable()
	{
		MiniGameCanvas.gameObject.SetActive(false);
	}

	private void Update()
	{
		if(MiniGameCanvas.gameObject.activeInHierarchy)
		{
			// TODO: Slider im Krug
			timerText.text = string.Join(":", (((drinkTime - drinkTime % 60) / 60)).ToString("00"), Mathf.Round(drinkTime % 60).ToString("00"));
			drinkBottleText.text = string.Concat("Water: ", drinkBottles.ToString(), " / ",  maxBottles.ToString());
			drunkBottleText.text = string.Concat("<color=yellow>Alcohol: ", drunkBottles.ToString(), " / ", maxAlcBottles.ToString());
		}
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
		drinkBottles = 0;
		drunkBottles = 0;
		bottleTime = 0;
		drinkTime = startDrinkTime;

		//Stop game
		assignedTarget.GetComponent<MinigameManager>().StartNextDialog(true);
	}

	public void Lost()
	{
		drinkBottles = 0;
		drunkBottles = 0;
		bottleTime = 0;
		drinkTime = startDrinkTime;

		//Stop game
		assignedTarget.GetComponent<MinigameManager>().StartNextDialog(false);
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
