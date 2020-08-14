using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

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

	[SerializeField]
	AnimationClip startAnimation;

	public float showResultTime = 1f;

	public override void StartMiniGame()
	{
		if (!point)
			point = Instantiate(pointPrefab, transform);
		else
			point.SetActive(true);

		gameObject.SetActive(true);
		SetWin();
		StartAnimation();
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

		shells[rNumber - 1].GetComponent<ShellTrigger>().isWin = true;
		point.transform.position = shells[rNumber-1].transform.position;
	}

	void StartAnimation()
    {
		animator.Play(startAnimation.name);
		Invoke(nameof(ShellAnimation), startAnimation.length);
    }

	private void ShellAnimation()
	{
		rounds++;

		point.SetActive(false);

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


	public void ShowResult(bool isWin)
    {
		point.SetActive(true);
		point.transform.position = shells[rNumber - 1].transform.position;

		// Fix for position stuck
		animator.enabled = false;

		if(!isWin)
        {
			shells[rNumber - 1].transform.DOLocalMoveY(1, showResultTime);
        }


		if(isWin)
        {
			Invoke(nameof(Win), showResultTime * 2);
        }
		else
        {
			Invoke(nameof(Lost), showResultTime * 2);
        }
    }

	public void Win()
	{
		//Stop game
		EndMiniGame();
		assignedTarget.GetComponent<MinigameManager>().StartNextDialog(true);
	}

	public void Lost()
	{
		//Stop game
		EndMiniGame();
		assignedTarget.GetComponent<MinigameManager>().StartNextDialog(false);
	}

    protected override void EndMiniGame()
    {
        base.EndMiniGame();
		roundText.SetText(string.Empty);
		point.SetActive(true);
		animator.enabled = true;
	}

	void ShowRounds()
	{
		if (rounds <= 3)
			roundText.SetText(string.Concat("Round\n", rounds));
		else
			roundText.SetText(string.Empty);
	}
}
