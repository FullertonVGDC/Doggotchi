using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindMatches : MonoBehaviour {
	private Board board;
	public List<GameObject> currentMatches = new List<GameObject>();
	public List<int> totalMatches = new List<int>();
//	public const float baseMultiplier = 0.2f;

	/* @var board -- will find the only board object instance
	 * in the scene. */
	void Start () {
		board = FindObjectOfType<Board>();
	}
	
	public void FindAllMatches(){
		StartCoroutine(FindAllMatchesCo());
	}

	private void AddAndMatch(GameObject t){
	if(!currentMatches.Contains(t)){
		currentMatches.Add(t);
	}
		t.GetComponent<Tile>().isMatch = true;
	}

	private void GetNearbyPiees(GameObject t1, GameObject t2, GameObject t3){
		AddAndMatch(t1);
		AddAndMatch(t2);
		AddAndMatch(t3);
	}

	/* @function FindAllMatches */
	private IEnumerator FindAllMatchesCo(){
		yield return new WaitForSeconds(.2f);
		for(int i = 0; i < board.width; ++i){
			for(int j = 0; j < board.height; ++j){
				GameObject currentTile = board.allTools[i, j];
				if(currentTile != null){
					/* Check if the tiles horizontal to the current tile
					 * are a match with the tile */
					if(i > 0 && i < board.width - 1){
						/* @variable leftTile -- check tile directly to the left of current
						 * @variable rightTile -- check tile directly to the right of current */
						GameObject leftTile = board.allTools[i - 1, j];
						GameObject rightTile = board.allTools[i + 1, j];
						
						/* proceed with the match check ONLY IF the left and right tiles are not
						 * empty objects */
						if(leftTile != null && rightTile != null){
							/* Check the tags on right and left tiles to see if match with the 
							 * current tile. If matched, then set @variable isMatch to true */
							if(leftTile.tag == currentTile.tag && rightTile.tag == currentTile.tag){
								GetNearbyPiees(leftTile, currentTile, rightTile);
							}
						}
					}
					
					/* Check if the tiles vertical to the current tile
					 * are a match with the tile */
					if(j > 0 && j < board.height - 1){
						/* @variable downTile -- check tile directly below the current tile
						 * @variable upTile -- check tile directly above the current tile */
						GameObject downTile = board.allTools[i, j - 1];
						GameObject upTile = board.allTools[i, j + 1];
						
						/* Proceed with the match check ONLY IF the tiles below and above are
						 * not empty GameObjects */
						if(downTile != null && upTile != null){
							/* Check the tags of the tiles below and above to see if matching with
							 * the current tiles tag. TRUE then set @variable isMatch to true */
							if(downTile.tag == currentTile.tag && upTile.tag == currentTile.tag){
								GetNearbyPiees(upTile, currentTile, downTile);
							}
						}
					}
				}
			}
		}
	}

	public void MatchesPiecesOfType(string type){
		for(int i = 0; i < board.width; ++i){
			for(int j = 0; j < board.height; ++j){
				if(board.allTools[i, j] != null){
					if(board.allTools[i, j].tag == type){
						board.allTools[i, j].GetComponent<Tile>().isMatch = true;
					}
				}
			}
		}
	}
}
