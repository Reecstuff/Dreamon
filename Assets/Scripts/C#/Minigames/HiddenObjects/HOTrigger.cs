using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOTrigger : Interactable
{
	public Camera mainCamera;

	public override void Interact()
	{
		GetComponent<HOTrigger>().HODestroy();
	}

	//Destroys the object when you pick it up
	public void HODestroy()
	{
		GetComponentInParent<HOManager>().foundObjects++;

		//transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - 2, mainCamera.transform.position.z + 2);

		Destroy(this.gameObject);
	}
}
