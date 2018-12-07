using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOverlay : MonoBehaviour {
	public Animator panelAnim;
	public Animator gameInfoAnim;

	public void Ok(){
		if(panelAnim != null && gameInfoAnim != null){
			panelAnim.SetBool("Out", true);
			gameInfoAnim.SetBool("Out", true);
			StartCoroutine(GameStartCo());
		}
	}

	public void GameOver(){
		panelAnim.SetBool("Out", false);
		panelAnim.SetBool("Game OVer", true);
	}

	IEnumerator GameStartCo(){
		yield return new WaitForSeconds(1f);
		Board board = FindObjectOfType<Board>();
		board.current = GameState.move;
	}
}
