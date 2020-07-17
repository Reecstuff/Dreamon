using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOManager : MiniGame
{
	public GameObject[] hiddenObjects;
	public int foundObjects;

	public GameObject assignedTarget;

	public float searchTime = 60;

    public override void StartMiniGame()
    {
        base.StartMiniGame();
    }

    private void Update()
	{
		searchTime -= Time.deltaTime;

		if (searchTime < 0)
		{
			assignedTarget.GetComponent<MinigameManager>().StartNextDialog(false);
		}
		else if (hiddenObjects.Length == foundObjects)
		{
			assignedTarget.GetComponent<MinigameManager>().StartNextDialog(true);
		}
	}
}
