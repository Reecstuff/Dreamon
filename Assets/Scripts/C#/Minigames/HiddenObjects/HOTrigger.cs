using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOTrigger : Interactable
{
	public override void Interact()
	{
		GetComponent<HOTrigger>().HODestroy();
	}

	//Destroys the object when you pick it up
	public void HODestroy()
	{
		Destroy(this.gameObject);
	}
}
