using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MenuButton(int type)
    {
        switch (type)
        {
            case 0:
                Application.Quit();
                break;
            case 1:
                SceneManager.LoadScene("MainScene");
                break;
            case 2:
                SceneManager.LoadScene("HomeScene");
                break;
            case 3:
                SceneManager.LoadScene("BathMinigame");
                break;
            case 4:
                SceneManager.LoadScene("walk");
                break;
            case 5:
                SceneManager.LoadScene("PlayMinigame");
                break;
            case 6:
                SceneManager.LoadScene("FoodMinigame");
                break;
            case 7:
                SceneManager.LoadScene("credits");
                break;
            case 8:
                SceneManager.LoadScene("howtoplay");
                break;
            default:
                break;
        }

    }
}
