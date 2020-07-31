using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkTrigger : Interactable
{ 

	public override void Interact()
	{
	 	GetComponent<Drink>().DrinkBottle();
	}
}
