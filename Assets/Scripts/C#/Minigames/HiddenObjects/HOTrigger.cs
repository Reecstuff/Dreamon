using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOTrigger : Interactable
{
	public override void Interact()
	{
		GetComponent<HOTrigger>().HODestroy();
	}

	public void HODestroy()
	{
		Destroy(this.gameObject);
	}
}
