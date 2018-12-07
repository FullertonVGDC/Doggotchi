using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogcontroller : MonoBehaviour {

    public GameObject sit;
    public GameObject bark;

	// Use this for initialization
	void Start () {
        sit.SetActive(true);
        bark.SetActive(false);

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton(0)) {
            sit.SetActive(false);
            bark.SetActive(true);
        }
        else
        {
            sit.SetActive(true);
            bark.SetActive(false);
        }
    }
}
