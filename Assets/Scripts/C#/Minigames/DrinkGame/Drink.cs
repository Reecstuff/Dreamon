using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
	public bool isAlc;

	DrinkManager drinkManager;

	private void Start()
	{
		drinkManager = GetComponentInParent<DrinkManager>();
	}

	//Drink the bottle by deactivating the MeshRenderer
	public void DrinkBottle()
	{
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

	public void SetBottle(bool isAlcoholic = true)
	{
		isAlc = isAlcoholic;

		if(isAlcoholic)
		{

			GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0.92f, 0.016f, 1));
			GetComponent<MeshRenderer>().enabled = true;
		}
		else
		{
			GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1, 1));
		}
	}
}
