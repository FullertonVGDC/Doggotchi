using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatches : MonoBehaviour {
	private Board board;
	public List<GameObject> currentMatches = new List<GameObject>();

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<Board>();
	}

	public void FindAllMatches(){
		StartCoroutine(FindAllMatchesCo());
	}
	
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
								if(!currentMatches.Contains(leftTile)){
									currentMatches.Add(leftTile);
								}
								
								leftTile.GetComponent<Tile>().isMatch = true;

								if(!currentMatches.Contains(rightTile)){
									currentMatches.Add(rightTile);
								}
								rightTile.GetComponent<Tile>().isMatch = true;

								if(!currentMatches.Contains(currentTile)){
									currentMatches.Add(currentTile);
								}

								currentTile.GetComponent<Tile>().isMatch = true;
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
								if(!currentMatches.Contains(downTile)){
									currentMatches.Add(downTile);
								}

								downTile.GetComponent<Tile>().isMatch = true;

								if(!currentMatches.Contains(upTile)){
									currentMatches.Add(upTile);
								}

								upTile.GetComponent<Tile>().isMatch = true;

								if(!currentMatches.Contains(currentTile)){
									currentMatches.Add(currentTile);
								}
								currentTile.GetComponent<Tile>().isMatch = true;
							}
						}
					}
				}
			}
		}
	}
}
