using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellgameManager : MonoBehaviour
{
	public Animation[] animations = new Animation[3];
	public GameObject[] shells = new GameObject[3];

	private void Start()
	{
		ShellAnimation();
	}

	private void ShellAnimation()
	{
		//Start random animation
		int rNumber = Random.Range(1, 3);

		if (rNumber == 1)
		{
			animations[0].Play();

			shells[0].GetComponent<ShellTrigger>().isWin = true;
		}
		else if (rNumber == 2)
		{
			animations[1].Play();

			shells[1].GetComponent<ShellTrigger>().isWin = true;
		}
		else if(rNumber == 3)
		{
			animations[2].Play();

			shells[2].GetComponent<ShellTrigger>().isWin = true;
		}
	}
}
