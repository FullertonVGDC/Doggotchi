using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsScript : MonoBehaviour
{
   
    public Image HealthBar;
	public Image HappyBar;
	public Image HygieneBar;
	public Image HungerBar;

    public Image StarCounter;
    public Sprite ThreeStar;
    public Sprite TwoStar;
    public Sprite OneStar;
    public Sprite NoStar;


    public float max_health = 100f;
    public float cur_health = 0f;

	public float max_happy = 100f;
	public float cur_happy = 0f;

	public float max_hygiene = 100f;
	public float cur_hygiene = 0f;

	public float max_hunger = 100f;
	public float cur_hunger = 0f;

	public float average = 0f;
	public float heartcount = 0f;

    // Use this for initialization
    void Start()
    {
		cur_health = 20f;
		cur_happy = 20f;
		cur_hunger = 20f;
		cur_hygiene = 20f;


		SetHealth(cur_health / 100f);
		SetHappy(cur_happy / 100f);
		Sethygiene(cur_hygiene / 100f);
		SetHunger(cur_hunger /100f);



	
    }

	/*void decreaseHealth()
    {
		cur_health -= 10f;
        float calc_health = cur_health / max_health;
        SetHealth(calc_health);
    }*/

	void increaseHealth(float multi)
	{
		cur_health += (10f + 5f * multi);
		float calc_health = cur_health / max_health;
		SetHealth (calc_health);
	}

    void SetHealth(float myhealth)
    {
        HealthBar.fillAmount = myhealth;
    }

	void decreaseHappy(float multi)
	{
		cur_health -= (10f + 5f * multi);
		float calc_health = cur_health / max_health;
		SetHealth(calc_health);
	}

	void increaseHappy(float multi)
	{
		cur_health += (10f + 5f * multi);
		float calc_happy = cur_happy / max_happy;
		SetHappy (calc_happy);
	}

	void SetHappy(float myhappy)
	{
		HappyBar.fillAmount = myhappy;
	}

	/*void decreasehygiene(float multi)
	{
		cur_hygiene -= (10f + 5f * multi);
		float calc_hygiene = cur_hygiene / max_hygiene;
		SetHealth(calc_hygiene);
	}*/

	void increasehygiene(float multi)
	{
		cur_hygiene += (10f + 5f * multi);
		float calc_hygiene = cur_hygiene / max_hygiene;
		Sethygiene (calc_hygiene);
	}

	void Sethygiene(float myhygiene)
	{
		HygieneBar.fillAmount = myhygiene;
	}
		


	/* void decreaseHunger(float multi)
	{
		cur_hunger -= (10f + 5f * multi);
		float calc_hunger = cur_hunger / max_hunger;
		Sethygiene(calc_hunger);
	}*/

	void increaseHunger(float multi)
	{
		cur_hunger += (10f + 5f * multi);
		float calc_hunger = cur_hunger / max_hunger;
		SetHunger (calc_hunger);
	}

	void SetHunger(float myhunger)
	{
		HungerBar.fillAmount = myhunger;
	}

	void CalcAverage()
	{
		average = (cur_happy + cur_health + cur_hunger + cur_hygiene) / 4f;

		cur_health = 20f;
		cur_happy = 20f;
		cur_hunger = 20f;
		cur_hygiene = 20f;


		SetHealth(cur_health / 100f);
		SetHappy(cur_happy / 100f);
		Sethygiene(cur_hygiene / 100f);
		SetHunger(cur_hunger /100f);

		if (average >= 80f) {
			//5 small hearts
			heartcount += 5f; 
		} else if (average >= 60f && average < 80f) {
			//4 hearts
			heartcount += 4f;
		} else if (average >= 40f && average < 60) {
			//3 hearats
			heartcount += 3f;
		} else if (average >= 20f && average < 40f) {
			//2 hearts
			heartcount += 2f;

		} else if (average > 0f && average < 20f) {
			//1 heart
			heartcount += 1f;
		}

	}


	void ChangeDogo()
	{
		if (heartcount == 15f) {
            StarCounter.sprite = ThreeStar;
		} else if (heartcount >= 10f && heartcount <= 14f) {
            StarCounter.sprite = TwoStar;
        } else if (heartcount >= 5f && heartcount <= 9f)
        {
            StarCounter.sprite = OneStar;
        }
		else
		{
            StarCounter.sprite = NoStar;
		}
			
	}







}
	
