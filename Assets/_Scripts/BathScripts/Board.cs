using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
	wait,
	move
}

public class Board : MonoBehaviour {
	public GameState current = GameState.move;
	public int width;
	public int height;
	public int offSet;
	public GameObject tilePrefab;
	//private BackgroundTile[,] allTiles;
	public GameObject[] tiles;
	public GameObject[,] allTools;

	private FindMatches findMatches;

	// Use this for initialization
	void Start () {
		findMatches = FindObjectOfType<FindMatches>();
		//allTiles = new BackgroundTile[width, height];
		allTools = new GameObject[width, height];
		Setup();
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

	private void DestroyMatchesAt(int column, int row){
		if(allTools[column, row].GetComponent<Tile>().isMatch){
			findMatches.currentMatches.Remove(allTools[column, row]);
			Destroy(allTools[column, row]);
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
			yield return new WaitForSeconds(.5f);
			DestroyMatches();
		}

		yield return new WaitForSeconds(.5f);
		current = GameState.move;
	}
}
