using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellgameManager : MiniGame
{
	public GameObject assignedTarget;
	public Animator animator;
	public GameObject[] shells = new GameObject[3];
	int round;
	int rNumber;

	GameObject point;
	public GameObject pointPrefab;

	public override void StartMiniGame()
	{
		base.StartMiniGame();
		ShellAnimation();
		SetWin();
	}

	private void Update()
	{
		point.transform.position = new Vector3(shells[rNumber-1].transform.position.x, transform.position.y, shells[rNumber-1].transform.position.z);
	}

	private void SetWin()
	{
		rNumber = Random.Range(1,4);

		if (rNumber == 1)
		{
			shells[0].GetComponent<ShellTrigger>().isWin = true;
			point = Instantiate(pointPrefab, shells[1].transform.position, transform.rotation);
		}
		else if (rNumber == 2)
		{
			shells[1].GetComponent<ShellTrigger>().isWin = true;
			point = Instantiate(pointPrefab, shells[1].transform.position, transform.rotation);
		}
		else if (rNumber == 3)
		{
			shells[2].GetComponent<ShellTrigger>().isWin = true;
			point = Instantiate(pointPrefab, shells[2].transform.position, transform.rotation);
		}
	}

	private void ShellAnimation()
	{
		round++;

		if (round == 1)
		{
			animator.SetTrigger("Trigger1");
		}
		else if (round == 2)
		{
			animator.SetTrigger("Trigger2");
		}
		else if (round == 3)
		{
			animator.SetTrigger("Trigger3");
		}
	}

	public void Win()
	{
		Destroy(point);

		//Stop game
		assignedTarget.GetComponent<MinigameManager>().StartNextDialog(true);
	}

	public void Lost()
	{
		Destroy(point);

		//Stop game
		assignedTarget.GetComponent<MinigameManager>().StartNextDialog(false);
	}
}
