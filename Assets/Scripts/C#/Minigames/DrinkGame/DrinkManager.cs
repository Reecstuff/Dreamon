using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DrinkManager : MonoBehaviour
{
	public Text timerText;
	public Text drinkBottleText;
	public Text drunkBottleText;

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

	private void Update()
	{
		timerText.text = Mathf.Round(drinkTime).ToString();
		drinkBottleText.text = "Needed bottles: " + maxBottles.ToString() + "  Drunk bottles: " + drinkBottles.ToString();
		drunkBottleText.text = "Prohibited alcohol : " + maxAlcBottles.ToString() + "  Drunk alcohol: " + drunkBottles.ToString();

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
