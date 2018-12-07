using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
	wait,
	move,
	win,
	lose,
	pause
}

public class Board : MonoBehaviour {
	public GameState current = GameState.move;
	public int width;
	public int height;
	public int offSet;
	public GameObject tilePrefab;
	public GameObject[] tiles;
	public GameObject[,] allTools;
	public Tile currentTile;
	public int baseValue = 20;

	private FindMatches findMatches;
	private bool noMoreMatches;
	private ScoreManager scoreManager;
	private int streakValue = 1;
	private GoalManager goalManager;
	private SoundManager soundManager;
	private DogPlayer dog;


	// Use this for initialization
	void Start () {
		dog = FindObjectOfType<DogPlayer>();
		soundManager = FindObjectOfType<SoundManager>();
		goalManager = FindObjectOfType<GoalManager>();
		scoreManager = FindObjectOfType<ScoreManager>();
		findMatches = FindObjectOfType<FindMatches>();
		allTools = new GameObject[width, height];
		Setup();

		current = GameState.pause;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void Setup(){
		for(int i = 0; i < width; ++i){
			for(int j = 0; j < height; ++j){
				Vector2 tmpPos = new Vector2(i, j + offSet);
				GameObject backgroundTile = Instantiate(tilePrefab, tmpPos, Quaternion.identity) as GameObject;
				backgroundTile.transform.parent = this.transform;
				backgroundTile.name = "( " + i + ", " + j + " )";

				int whichTile = Random.Range(0, tiles.Length);
				int maxIt = 0;
				while(MatchesAt(i, j, tiles[whichTile]) && maxIt < 100){
					whichTile = Random.Range(0, tiles.Length);
					++maxIt;
				}
				maxIt = 0;

				GameObject tool = Instantiate(tiles[whichTile], tmpPos, Quaternion.identity);
				tool.GetComponent<Tile>().row = j;
				tool.GetComponent<Tile>().column = i;
				tool.transform.parent = this.transform;
				tool.name = "( " + i + ", " + j + " )";
				allTools[i, j] = tool;
			}
		}
	}

	private bool MatchesAt(int column, int row, GameObject piece){
		if(column > 1 && row > 1){
			if(allTools[column - 1, row].tag == piece.tag && allTools[column - 2, row].tag == piece.tag){
				return true;
			}
			if(allTools[column, row - 1].tag == piece.tag && allTools[column, row - 2].tag == piece.tag){
				return true;
			}
		}
		else if(column <= 1 || row <= 1){
			if(row > 1){
				if(allTools[column, row - 1].tag == piece.tag && allTools[column, row - 2].tag == piece.tag){
					return true;
				}
			}
			if(column > 1){
				if(allTools[column - 1, row].tag == piece.tag && allTools[column - 2, row].tag == piece.tag){
					return true;
				}
			}
		}

		return false;
	}


	private void CheckMakeBombs(){
		if(findMatches.currentMatches.Count >= 4 || findMatches.currentMatches.Count <= 8){
			// make a type bomb
				if(currentTile != null){
					if(currentTile.isMatch){
						if(!currentTile.isTypeBomb){
							currentTile.isMatch = false;
							currentTile.MakeTypeBomb();
							if(soundManager != null){
								soundManager.Bark();
							}
						}

					}
					else{
						if(currentTile.otherTile != null){
							Tile other = currentTile.otherTile.GetComponent<Tile>();
							if(other.isMatch){
								if(!other.isTypeBomb){
									other.isMatch = false;
									other.MakeTypeBomb();
								}
							}
						}
					}
				}
		}
	}

	private void DestroyMatchesAt(int column, int row){
		if(allTools[column, row].GetComponent<Tile>().isMatch){
			if(findMatches.currentMatches.Count >= 4){
				CheckMakeBombs();
			}

			if(goalManager != null){
				goalManager.CompareGoal(allTools[column, row].tag.ToString());
				goalManager.UpdateGoals();
			}
			findMatches.currentMatches.Remove(allTools[column, row]);

			if(soundManager != null){
				soundManager.PlayNoise();
			}

			Destroy(allTools[column, row]);
			scoreManager.IncreaseScore(baseValue * streakValue);

			if(dog != null){
				dog.UpdateHygiene(streakValue);
			}

			allTools[column, row] = null;
		}
	}

	public void DestroyMatches(){
		for(int i = 0; i < width; ++i){
			for(int j = 0; j < height; ++j){
				if(allTools[i, j] != null){
					DestroyMatchesAt(i, j);
				}
			}
		}

		StartCoroutine(DecreaseRowCo());
	}

	private IEnumerator DecreaseRowCo(){
		int nullCount = 0;
		for(int i = 0; i < width; ++i){
			for(int j = 0; j < height; ++j){
				if(allTools[i, j] == null){
					++nullCount;
				}
				else if(nullCount > 0){
					allTools[i, j].GetComponent<Tile>().row -= nullCount;
					allTools[i, j] = null;
				}
			}
			nullCount = 0;
		}

		yield return new WaitForSeconds(.4f);
		StartCoroutine(FillBoardCo());
	}

	private void RefillBoard(){
		for(int i = 0; i < width; ++i){
			for(int j = 0; j < height; ++j){
				if(allTools[i, j] == null){
					Vector2 tempPos = new Vector2(i, j + offSet);
					int use = Random.Range(0, tiles.Length);
					GameObject piece = Instantiate(tiles[use], tempPos, Quaternion.identity);

					piece.transform.parent = this.transform;
					piece.name = "( " + i + ", " + j + " )";
					
					allTools[i, j] = piece;
					piece.GetComponent<Tile>().row = j;
					piece.GetComponent<Tile>().column = i;
				}
			}
		}
	}

	private bool MatchesOnBoard(){
		for(int i = 0; i < width; ++i){
			for(int j = 0; j < height; ++j){
				if(allTools[i, j] != null){
					if(allTools[i, j].GetComponent<Tile>().isMatch){
						return true;
					}
				}
			}
		}
		return false;
	}

	private IEnumerator FillBoardCo(){
		RefillBoard();
		yield return new WaitForSeconds(.5f);

		while(MatchesOnBoard()){
			streakValue += 1;
			yield return new WaitForSeconds(.5f);
			DestroyMatches();
		}

		findMatches.currentMatches.Clear();
		currentTile = null;
		yield return new WaitForSeconds(.5f);

		if(DeadLocked()){
			ShuffleBoard();
		}

		current = GameState.move;
		streakValue = 1;
	}

	private void SwitchPieces(int column, int row, Vector2 direction){ // Reeses peices? Reeces?
		GameObject temp = allTools[column + (int)direction.x, row + (int)direction.y] as GameObject;
		allTools[column + (int)direction.x, row + (int)direction.y] = allTools[column, row];
		allTools[column, row] = temp;
	}

	private bool CheckForMatches(){
		for(int i = 0; i < width; ++i){
			for(int j = 0; j < height; ++j){
				if(allTools[i, j] != null){
						if(i < width - 2){
						if(allTools[i + 1, j] != null && allTools[i + 2, j] != null){
							if(allTools[i + 1, j].tag == allTools[i, j].tag && allTools[i + 2, j].tag == allTools[i, j].tag){
								return true;
							}
						}
					}
					if(j < height - 2){
						if(allTools[i, j + 1] != null && allTools[i, j + 2] != null){
							if(allTools[i, j + 1].tag == allTools[i, j].tag && allTools[i, j + 2].tag == allTools[i, j].tag){
								return true;
							}
						}
					}
				}
			}
		}

		return false;
	}

	private bool SwitchAndCheck(int column, int row, Vector2 direction){
		SwitchPieces(column, row, direction);

		if(CheckForMatches()){
			SwitchPieces(column, row, direction);
			return true;
		}

		SwitchPieces(column, row, direction);
		return false;
	}

	private bool DeadLocked(){
		for(int i = 0; i < width; ++i){
			for(int j = 0; j < height; ++j){
				if(allTools[i, j] != null){
					if(i < width - 2){
						if(SwitchAndCheck(i, j, Vector2.right)){
							return false;
						}
					}

					if(j < height - 2){
						if(SwitchAndCheck(i, j, Vector2.up)){
							return false;
						}
					}
				}
			}
		}

		return true;
	}

	private void ShuffleBoard(){
		List<GameObject> reshuffle = new List<GameObject>();
		
		for(int i = 0; i < width; ++i){
			for(int j = 0; j < height; ++j){
				if(allTools[i, j] != null){
					reshuffle.Add(allTools[i, j]);
				}
			}
		}

		for(int i = 0; i < width; ++i){
			for(int j = 0; j < height; ++j){
				int use = Random.Range(0, reshuffle.Count);
				

				int maxIt = 0;
				while(MatchesAt(i, j, reshuffle[use]) && maxIt < 100){
					use = Random.Range(0, reshuffle.Count);
					++maxIt;
				}
				Tile pieces = reshuffle[use].GetComponent<Tile>();
				maxIt = 0;

				pieces.column = i;
				pieces.row = j;

				allTools[i, j] = reshuffle[use];
				reshuffle.Remove(reshuffle[use]);
			}
		}
		
		if(DeadLocked()){
			ShuffleBoard();
		}
	}

	
}
