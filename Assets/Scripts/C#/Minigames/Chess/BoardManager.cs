using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BoardManager : MiniGame
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

	int rounds;
	int winRounds;
	int loseRounds;

	[SerializeField]
	AudioClip[] movePiece;

	[SerializeField]
	AudioClip[] hitPiece;

	AudioSource source;

	private void Start()
	{
		source = GetComponent<AudioSource>();
		Instance = this;
		SpawnAllChessmans(rounds);
		Cursor.visible = true;
		selectedHighlight = Instantiate(selectedHighlight, transform);
	}

    public override void StartMiniGame()
    {
        base.StartMiniGame();
    }

    private void Update()
	{
		//If the player lose the game
		if (currentChessmans == 0)
		{
			loseRounds++;

			if (rounds == 3)
			{
				if (winRounds < loseRounds)
				{
					assignedTarget.GetComponent<MinigameManager>().StartNextDialog(false);
				}
				else if (winRounds > loseRounds)
				{
					assignedTarget.GetComponent<MinigameManager>().StartNextDialog(true);
				}
			}


			SpawnAllChessmans(rounds);
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
					if (selectionY == 7 && selectedHighlight.transform.localPosition.z >= 7)
					{
						winRounds++;
						if (rounds == 3)
						{
							if (winRounds < loseRounds)
							{
								assignedTarget.GetComponent<MinigameManager>().StartNextDialog(false);
							}
							else if (winRounds > loseRounds)
							{
								assignedTarget.GetComponent<MinigameManager>().StartNextDialog(true);
							}
						}


						SpawnAllChessmans(rounds);
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

				source.clip = hitPiece[Random.Range(0, hitPiece.Length)];
			}
			else
			{
				source.clip = movePiece[Random.Range(0, movePiece.Length)];
			}

			Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
			selectedChessman.transform.localPosition = new Vector3(x + 0.5f, selectedChessman.transform.localScale.y / 2, z + 0.5f);
			selectedChessman.SetPosition(x, z);
			Chessmans[x, z] = selectedChessman;

			source.Play();
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
			
			selectionX = (int)transform.InverseTransformPoint(hit.point).x;
			selectionY = (int)transform.InverseTransformPoint(hit.point).z;

		}
		else
		{
			selectionX = -1;
			selectionY = -1;
		}
	}

	private void SpawnChessman(int index, int x, int z)
	{

		GameObject go = Instantiate(chessmanPrefabs[index]) as GameObject;
		go.transform.SetParent(transform);
		go.transform.rotation = transform.rotation;


		go.transform.localPosition = new Vector3(x + 0.5f, go.transform.localScale.y / 2, z + 0.5f);

		Chessmans[x, z] = go.GetComponent<Chessman>();
		Chessmans[x, z].SetPosition(x, z);
		activeChessman.Add(go);
	}

	private void SpawnAllChessmans(int round)
	{
		Chessmans = new Chessman[8, 8];

		if (round > 0)
		{
			for (int i = 0; i < activeChessman.Count; i++)
			{
				Destroy(activeChessman[i]);
			}

			activeChessman.Clear();
		}

		if (round == 0)
		{
			//Spawn the players pieces
			SpawnChessman(0, 7, 1);
			SpawnChessman(0, 6, 0);

			//Spawn the enemys pieces
			for (int i = 0; i < 8; i++)
			{
				SpawnChessman(1, i, 6);
			}
		}
		else if (round == 1)
		{
			//Spawn the players pieces
			SpawnChessman(0, 0, 2);
			SpawnChessman(0, 7, 1);
			SpawnChessman(0, 6, 0);
			SpawnChessman(0, 2, 0);

			//Spawn the enemys pieces
			SpawnChessman(1, 3, 2);
			SpawnChessman(1, 4, 2);
			SpawnChessman(1, 5, 3);
			SpawnChessman(1, 5, 4);
			SpawnChessman(1, 2, 3);
			SpawnChessman(1, 2, 4);

			for (int i = 0; i < 8; i++)
			{
				SpawnChessman(1, i, 6);
			}
		}
		else if (round == 2)
		{
			//Spawn the players pieces
			SpawnChessman(0, 4, 0);
			SpawnChessman(0, 2, 2);

			SpawnChessman(1, 2, 5);
			SpawnChessman(1, 2, 4);
			SpawnChessman(1, 5, 5);
			SpawnChessman(1, 5, 4);
			SpawnChessman(1, 0, 2);
			SpawnChessman(1, 7, 2);

			//Spawn the enemys pieces
			for (int i = 0; i < 8; i++)
			{
				SpawnChessman(1, i, 6);
			}
		}

		ResetChess();
	}

	private void ResetChess()
	{
		rounds++;
		currentChessmans = activeChessman.Count(c => c.GetComponent<Pieces>() == true);
		selectionX = 0;
		selectionY = 0;
	}

	private void DrawChessboard()
	{
		//Draw the selection
		if (selectionX >= 0 && selectionY >= 0)
		{
			selectedHighlight.transform.localPosition = Vector3.forward * (selectionY + 0.5f )  + Vector3.right * (selectionX + 0.5f )  + Vector3.up * 0.002f;
		}
	}
}
