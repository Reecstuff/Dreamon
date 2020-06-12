using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCredits : Interactable
{
	public Animator transition;

	public override void Interact()
	{
		StartCoroutine(LoadLevel());
	}
	
	IEnumerator LoadLevel()
	{
		//Play animation
		transition.SetTrigger("Start");

		//Wait
		yield return new WaitForSeconds(4);

		//Load scene
		SceneManager.LoadScene("Credits");
	}
}
