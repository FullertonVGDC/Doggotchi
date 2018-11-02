using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	private Vector2 firstTouchPos_;
	private Vector2 finalTouchPos_;
	private Vector2 tmpPos_;
	private GameObject otherTile;
	private Board board;

	public bool isMatch = false;
	public float swipeAngle_ = 0;
	public float swipeResist = 1f;

	public int prevColumn;
	public int prevRow;
	public int column;
	public int row;
	public int targetX;
	public int targetY;

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<Board>();
		targetX = (int)transform.position.x;
		targetY = (int)transform.position.y;
		row = targetY;
		column = targetX;
		prevRow = row;
		prevColumn = column;
	}
	
	// Update is called once per frame
	void Update () {
		FindMatches();
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
		}
		else {
			// Set position
			tmpPos_ = new Vector2(transform.position.x, targetY);
			transform.position = tmpPos_;
		}
	}

	public IEnumerator CheckMoveCo(){
		yield return new WaitForSeconds(.5f);
		if(otherTile != null){
			if(!isMatch && !otherTile.GetComponent<Tile>().isMatch){
				otherTile.GetComponent<Tile>().row = row;
				otherTile.GetComponent<Tile>().column = column;
				row = prevRow;
				column = prevColumn;
			}
			else{
				board.DestroyMatches();
			}	
			otherTile = null;
		}
		
	}

	private void OnMouseDown(){
		firstTouchPos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	private void OnMouseUp(){
		finalTouchPos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		CalculateAngle();
	}

	void CalculateAngle(){
		if(Mathf.Abs(finalTouchPos_.y - firstTouchPos_.y) > swipeResist || Mathf.Abs(finalTouchPos_.x - firstTouchPos_.x) > swipeResist){
			swipeAngle_ = Mathf.Atan2(finalTouchPos_.y - firstTouchPos_.y, finalTouchPos_.x - firstTouchPos_.x) * 180 / Mathf.PI;
			MovePieces();
		}
	}

	void MovePieces(){
		// Swap right
		if(swipeAngle_ > -45 && swipeAngle_ <= 45 && column < board.width - 1){
			otherTile = board.allTools[column + 1, row];
			otherTile.GetComponent<Tile>().column -= 1;
			column += 1;
		}
		// Swap Up
		else if(swipeAngle_ > 45 && swipeAngle_ <= 135 && row < board.height - 1){
			otherTile = board.allTools[column, row + 1];
			otherTile.GetComponent<Tile>().row -= 1;
			row += 1;
		}
		// Swap Left
		else if((swipeAngle_ > 135 || swipeAngle_ <= -135) && column > 0){
			otherTile = board.allTools[column - 1, row];
			otherTile.GetComponent<Tile>().column += 1;
			column -= 1;
		}
		// Swap Down
		else if(swipeAngle_ < -45 && swipeAngle_ >= -135 && row > 0){
			otherTile = board.allTools[column, row - 1];
			otherTile.GetComponent<Tile>().row += 1;
			row -= 1;
		}

		StartCoroutine(CheckMoveCo());
	}

	void FindMatches(){
		if(column > 0 && column < board.width - 1){
			GameObject leftTile1 = board.allTools[column - 1, row];
			GameObject rightTile1 = board.allTools[column + 1, row];
			if(leftTile1 != null && rightTile1 != null){
				if(leftTile1.tag == this.gameObject.tag && rightTile1.tag == this.gameObject.tag){
					leftTile1.GetComponent<Tile>().isMatch = true;
					rightTile1.GetComponent<Tile>().isMatch = true;
					isMatch = true;
				}
			}
		}

		if(row > 0 && row < board.height - 1){
			GameObject upTile1 = board.allTools[column, row + 1];
			GameObject downTile1 = board.allTools[column, row - 1];
			if(upTile1 != null && downTile1 != null){
				if(upTile1.tag == this.gameObject.tag && downTile1.tag == this.gameObject.tag){
					upTile1.GetComponent<Tile>().isMatch = true;
					downTile1.GetComponent<Tile>().isMatch = true;
					isMatch = true;
				}
			}
		}
	}
}
