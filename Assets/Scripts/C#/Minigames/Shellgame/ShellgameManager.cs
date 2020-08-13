using System.Collections;
using TMPro;
using UnityEngine;

public class ShellgameManager : MiniGame
{
	public GameObject assignedTarget;
	public Animator animator;
	public GameObject[] shells = new GameObject[3];
	int rounds;
	int rNumber;

	GameObject point;
	public GameObject pointPrefab;

	[SerializeField]
	TextMeshProUGUI roundText;

	public override void StartMiniGame()
	{
		gameObject.SetActive(true);
		ShellAnimation();
		SetWin();
		base.StartMiniGame();
	}

    protected override IEnumerator MiniGameUpdate()
    {
		do
		{
			if (point)
				point.transform.position = new Vector3(shells[rNumber - 1].transform.position.x, transform.position.y, shells[rNumber - 1].transform.position.z);

			return base.MiniGameUpdate();
		} while (gameObject.activeInHierarchy);
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
		rounds++;

		if (rounds == 1)
		{
			animator.SetTrigger("Trigger1");
		}
		else if (rounds == 2)
		{
			animator.SetTrigger("Trigger2");
		}
		else if (rounds == 3)
		{
			animator.SetTrigger("Trigger3");
		}
		ShowRounds();
	}

	public void Win()
	{
		Destroy(point);

		//Stop game
		EndMiniGame();
		assignedTarget.GetComponent<MinigameManager>().StartNextDialog(true);
	}

	public void Lost()
	{
		Destroy(point);

		//Stop game
		EndMiniGame();
		assignedTarget.GetComponent<MinigameManager>().StartNextDialog(false);
	}

    protected override void EndMiniGame()
    {
        base.EndMiniGame();
		roundText.SetText(string.Empty);
    }

    void ShowRounds()
	{
		if (rounds <= 3)
			roundText.SetText(string.Concat("Round\n", rounds));
		else
			roundText.SetText(string.Empty);
	}
}
