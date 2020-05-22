using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkManager : MonoBehaviour
{
	public int drinkBottles;
	public int maxBottles;

	public int drunkBottles;
	public int maxAlcBottles;

	public GameObject bottle;

	public float drinkTime = 3;

	private void Update()
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

		//Stop game
	}

	public void Lost()
	{
		//Lose the DontGetDrunk game
		Debug.Log("You Lose");

		//Stop game
	}
}
