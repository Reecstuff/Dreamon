using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
	public bool isAlc;

	private float bottleTimer;
	public float maxBottleTimer = 3;

	private void Update()
	{
		if (GetComponent<MeshRenderer>().enabled == false)
		{
			bottleTimer -= Time.deltaTime;
		}
		if (bottleTimer <= 0)
		{
			bottleTimer = maxBottleTimer;

			RandomAlc();

			GetComponent<MeshRenderer>().enabled = true;
		}
	}

	public void DrinkBottle()
	{
		DrinkManager drinkManager = GameObject.Find("DrinkGame").GetComponent<DrinkManager>();

		if (isAlc == true)
		{
			drinkManager.drunkBottles++;
		}
		else
		{
			drinkManager.drinkBottles++;
		}

		GetComponent<MeshRenderer>().enabled = false;
	}

	private bool RandomAlc()
	{
		float rNumber = Random.Range(0.0f, 1.0f);

		if (rNumber < 0.75f)
		{
			return isAlc = true;
		}
		else
		{
			return isAlc = false;
		}
	}
}
