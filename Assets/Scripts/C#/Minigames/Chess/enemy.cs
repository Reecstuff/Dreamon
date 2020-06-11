using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Chessman
{
	private void Start()
	{
		isEnemy = true;
	}

	public override bool[,] PossibleMove()
	{
		bool[,] r = new bool[8, 8];

		//UpLeft
		PieceMove(CurrentX - 1, CurrentY + 2, ref r);

		//UpRight
		PieceMove(CurrentX + 1, CurrentY + 2, ref r);

		//RightUp
		PieceMove(CurrentX + 2, CurrentY + 1, ref r);

		//RightDown
		PieceMove(CurrentX + 2, CurrentY - 1, ref r);

		//DownLeft
		PieceMove(CurrentX - 1, CurrentY - 2, ref r);

		//DownRight
		PieceMove(CurrentX + 1, CurrentY - 2, ref r);

		//LeftUp
		PieceMove(CurrentX - 2, CurrentY + 1, ref r);

		//LeftDown
		PieceMove(CurrentX - 2, CurrentY - 1, ref r);

		return r;
	}

	public void PieceMove(int x, int y, ref bool[,] r)
	{
		Chessman c;
		if (x >= 0 && x < 8 && y >= 0 && y < 8)
		{
			c = BoardManager.Instance.Chessmans[x, y];
			if (c == null)
			{
				r[x, y] = true;
			}
		}
	}
}