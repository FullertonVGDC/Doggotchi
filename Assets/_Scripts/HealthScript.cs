using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Image Bar;
    public float max_health = 100f;
    public float cur_health = 0f;

    // Use this for initialization
    void Start()
    {

        cur_health = max_health;
        InvokeRepeating("decreaseHealth", 0f, 2f);

    }

    void decreaseHealth()
    {
        cur_health -= 10f;
        float calc_health = cur_health / max_health;
        SetHealth(calc_health);
    }

    void SetHealth(float myhealth)
    {
        Bar.fillAmount = myhealth;
    }
}
	
