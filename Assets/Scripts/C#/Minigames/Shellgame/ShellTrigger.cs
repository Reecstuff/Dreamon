using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellTrigger : Interactable
{
	public bool isWin;

	ShellgameManager shellgameManager;

    protected override void InitValues()
    {
        base.InitValues();
		shellgameManager = GetComponentInParent<ShellgameManager>();

	}

    public override void Interact()
	{
		if (shellgameManager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
		{
			SelectShell();
		}
	}

	public void SelectShell()
	{
		shellgameManager.animator.enabled = false;

		transform.DOLocalMoveY(1, shellgameManager.showResultTime);

		shellgameManager.ShowResult(isWin);
	}
}
