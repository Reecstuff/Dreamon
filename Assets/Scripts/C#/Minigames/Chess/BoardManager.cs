using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	public static BoardManager Instance { set; get; }
	private bool[,] allowedMoves { set; get; }

	public Chessman[,] Chessmans { set; get; }
	private Chessman selectedChessman;

	private int selectionX = -1;
	private int selectionY = -1;

	public List<GameObject> chessmanPrefabs;
	private List<GameObject> activeChessman = new List<GameObject>();

	public GameObject selectedHighlight;

	int currentChessmans = 3;

	public GameObject assignedTarget;
	public int winDialogue;
	public int loseDialogue;

	private void Start()
	{
		Instance = this;
		SpawnAllChessmans();

		selectedHighlight = Instantiate(selectedHighlight, transform);
	}

	private void Update()
	{
		//If the player lose the game
		if (currentChessmans < 0)
		{
			assignedTarget.GetComponent<MinigameManager>().StartNextDialog(false);
		}

		UpdateSelection();
		DrawChessboard();

		if (Input.GetMouseButtonDown(0))
		{
			if (selectionX >= 0 && selectionY >= 0)
			{
				if (selectedChessman == null)
				{
					//Select the chessman
					SelectChessman(selectionX, selectionY);
				}
				else
				{
					//If the player win the game
					if (selectionY == 7)
					{
						assignedTarget.GetComponent<MinigameManager>().StartNextDialog(true);
					}

					//Move the chessman
					MoveChessman(selectionX, selectionY);
				}
			}
		}
	}

	private void SelectChessman(int x, int z)
	{
		if (Chessmans[x,z] == null)
		{
			return;
		}

		allowedMoves = Chessmans[x, z].PossibleMove();
		selectedChessman = Chessmans[x, z];
		BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
	}

	private void MoveChessman(int x, int z)
	{
		if (allowedMoves[x,z])
		{
			Chessman c = Chessmans[x, z];

			if (c != null)
			{
				//capture a piece
				activeChessman.Remove(c.gameObject);
				Destroy(c.gameObject);
				Destroy(selectedChessman.gameObject);
				currentChessmans--;
			}

			Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
			selectedChessman.transform.position = transform.position + new Vector3(x + 0.5f, 0.5f, z + 0.5f);
			selectedChessman.SetPosition(x, z);
			Chessmans[x, z] = selectedChessman;
		} 

		BoardHighlights.Instance.Hidehighlights();
		selectedChessman = null;
	}

	private void UpdateSelection()
	{
		if (!Camera.main)
		{
			return;
		}

		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
		{
			selectionX = (int)hit.point.x;
			selectionY = (int)hit.point.z;
		}
		else
		{
			selectionX = -1;
			selectionY = -1;
		}
	}

	private void SpawnChessman(int index, int x, int z)
	{
		GameObject go = Instantiate(chessmanPrefabs[index], transform.position + new Vector3(x + 0.5f, 0.5f, z + 0.5f), Quaternion.identity) as GameObject;
		go.transform.SetParent(transform);
		Chessmans[x, z] = go.GetComponent<Chessman>();
		Chessmans[x, z].SetPosition(x, z);
		activeChessman.Add(go);
	}

	private void SpawnAllChessmans()
	{
		activeChessman = new List<GameObject>();
		Chessmans = new Chessman[8, 8];

		//Spawn the players pieces
		SpawnChessman(0, 0, 0);
		SpawnChessman(0, 7, 2);
		SpawnChessman(0, 7, 0);
		SpawnChessman(0, 0, 2);

		//Spawn the enemys pieces
		for (int i = 0; i < 8; i++)
		{
			SpawnChessman(1, i, 6);
		}
	}

	private void DrawChessboard()
	{
		//Draw the selection
		if (selectionX >= 0 && selectionY >= 0)
		{
			selectedHighlight.transform.position = Vector3.forward * (selectionY + 0.5f) + Vector3.right * (selectionX + 0.5f) + Vector3.up * 0.002f;
		}
	}
}
