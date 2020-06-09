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

		//Changes the color of the bottle if it contains alcohol
		if (isAlc == true)
		{
			GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0.92f, 0.016f, 1));
		}
		else
		{
			GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1, 1));
		}
	}

	//Drink the bottle by deactivating the MeshRenderer
	public void DrinkBottle()
	{
		DrinkManager drinkManager = gameObject.GetComponentInParent<DrinkManager>();

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

	//Randomly determines whether a bottle contains alcohol
	private void RandomAlc()
	{
		float rNumber = Random.Range(0.0f, 1.0f);

		if (rNumber < 0.75f)
		{
			isAlc = true;
		}
		else
		{
			isAlc = false;
		}
	}
}
