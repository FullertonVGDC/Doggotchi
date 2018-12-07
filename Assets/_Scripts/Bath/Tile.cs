using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	private Vector2 firstTouchPos_;
	private Vector2 finalTouchPos_;
	private Vector2 tmpPos_;
	public GameObject otherTile;
	private Board board;
	private FindMatches findMatches;
	//private EndGameManager endGameManager;

	public bool isMatch = false;
	public float swipeAngle_ = 0;
	public float swipeResist = 1f;

	public int prevColumn;
	public int prevRow;
	public int column;
	public int row;
	public int targetX;
	public int targetY;
	public bool isTypeBomb;
	public GameObject typeBomb;

	// Use this for initialization
	void Start () {
		//endGameManager = FindObjectOfType<EndGameManager>();
		board = FindObjectOfType<Board>();
		findMatches = FindObjectOfType<FindMatches>();
	}

	/* Debugger */
	void OnMouseOver(){
		if(Input.GetMouseButtonDown(1)){
			isTypeBomb = true;
			GameObject arrow = Instantiate(typeBomb, transform.position, Quaternion.identity);
			arrow.transform.parent = this.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(isMatch){
			SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
			mySprite.color = new Color(0f, 0f, 0f, .2f);
		}

		targetX = column;
		targetY = row;
		if(Mathf.Abs(targetX - transform.position.x) > .1){
			// Move to target
			tmpPos_ = new Vector2(targetX, transform.position.y);
			transform.position = Vector2.Lerp(transform.position, tmpPos_, .5f);

			if(board.allTools[column, row] != this.gameObject){
				board.allTools[column, row] = this.gameObject;
			}
			findMatches.FindAllMatches();
		}
		else {
			// Set position
			tmpPos_ = new Vector2(targetX, transform.position.y);
			transform.position = tmpPos_;
		}

		if(Mathf.Abs(targetY - transform.position.y) > .1){
			// Move to target
			tmpPos_ = new Vector2(transform.position.x, targetY);
			transform.position = Vector2.Lerp(transform.position, tmpPos_, .4f);
			
			if(board.allTools[column, row] != this.gameObject){
				board.allTools[column, row] = this.gameObject;
			}
			findMatches.FindAllMatches();

		}
		else {
			// Set position
			tmpPos_ = new Vector2(transform.position.x, targetY);
			transform.position = tmpPos_;
		}
	}

	public IEnumerator CheckMoveCo(){
		if(isTypeBomb){
			findMatches.MatchesPiecesOfType(otherTile.tag);
			isMatch = true;
		}
		else if(otherTile.GetComponent<Tile>().isTypeBomb){
			findMatches.MatchesPiecesOfType(this.gameObject.tag);
			otherTile.GetComponent<Tile>().isMatch = true;
		}

		yield return new WaitForSeconds(.5f);
		if(otherTile != null){
			if(!isMatch && !otherTile.GetComponent<Tile>().isMatch){
				otherTile.GetComponent<Tile>().row = row;
				otherTile.GetComponent<Tile>().column = column;
				row = prevRow;
				column = prevColumn;

				yield return new WaitForSeconds(.5f);
				board.currentTile = null;
				board.current = GameState.move;
			}
			else{
				board.DestroyMatches();
			}	
			//otherTile = null;
		}
	}

	/* @variable firstTouchPos_ -- the first touch position is calculated
	 * when the mouse is pushed down */
	private void OnMouseDown(){
		if(board.current == GameState.move){
			firstTouchPos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}

	/* @variable finalTouchPos_ -- the final position is calculated when
	 * the mouse is released */
	private void OnMouseUp(){
		if(board.current == GameState.move){
			finalTouchPos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			CalculateAngle();
		}
	}

	/* @funct CalculateAngle() -- calculate the angle of the mouse swipe
	 * @variable swipeAngle_ -- must be in degrees, Mathf will return radians */
	void CalculateAngle(){
		if(Mathf.Abs(finalTouchPos_.y - firstTouchPos_.y) > swipeResist || Mathf.Abs(finalTouchPos_.x - firstTouchPos_.x) > swipeResist){
			swipeAngle_ = Mathf.Atan2(finalTouchPos_.y - firstTouchPos_.y, finalTouchPos_.x - firstTouchPos_.x) * 180f / Mathf.PI;
			MovePieces();

			/* Check to see if the swap is a valid swap, else do not enter a waiting
			 * game state, all conditions must be met. If this condition is removed,
			 * the game will wait indefinetely if an invalid swap is made out of bounds
			 * of board. */
			if(column < board.width - 1 && column > 0 && row < board.height - 1 && row > 0){
				board.current = GameState.wait;
				board.currentTile = this;
			}
		}
		else {
			board.current = GameState.move;
		}
	}

	/* @funct MovePieces() -- when the swipeAngle_ is calculated
	 * this will determine which pieces will be moved with which
	 * pieces */
	void MovePieces(){
		// Swap right
		if(swipeAngle_ > -45 && swipeAngle_ <= 45 && column < board.width - 1){
			otherTile = board.allTools[column + 1, row];
			prevRow = row;
			prevColumn = column;
			otherTile.GetComponent<Tile>().column -= 1;
			column += 1;
		}
		// Swap Up
		else if(swipeAngle_ > 45 && swipeAngle_ <= 135 && row < board.height - 1){
			otherTile = board.allTools[column, row + 1];
			prevRow = row;
			prevColumn = column;
			otherTile.GetComponent<Tile>().row -= 1;
			row += 1;
		}
		// Swap Left
		else if((swipeAngle_ > 135 || swipeAngle_ <= -135) && column > 0){
			otherTile = board.allTools[column - 1, row];
			prevRow = row;
			prevColumn = column;
			otherTile.GetComponent<Tile>().column += 1;
			column -= 1;
		}
		// Swap Down
		else if(swipeAngle_ < -45 && swipeAngle_ >= -135 && row > 0){
			otherTile = board.allTools[column, row - 1];
			prevRow = row;
			prevColumn = column;
			otherTile.GetComponent<Tile>().row += 1;
			row -= 1;
		}

		StartCoroutine(CheckMoveCo());
	}

	public void MakeTypeBomb(){
		isTypeBomb = true;
		GameObject type = Instantiate(typeBomb, transform.position, Quaternion.identity);
		type.transform.parent = this.transform;
	}
}
