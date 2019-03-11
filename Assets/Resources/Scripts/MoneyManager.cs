using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour {

    public float startMoney = 100f;
    private float curMoney;
    public static MoneyManager instance;

    public int startHeart = 10;
    private float curHeart;

	// Use this for initialization
	void Start () {
        curMoney = startMoney;
        curHeart = startHeart;
        instance = this;
        InvokeRepeating("IncMoneyAuto", 2, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncMoney(float money)
    {
        curMoney += money;
        transform.Find("moneyAudio").GetComponent<AudioSource>().Play();
    }

    public bool DecMoney(float money)
    {
        float temp = curMoney - money;
        if (temp >= 0)
        {
            curMoney = temp;
            return true;
        }
        else return false;
    }

    public float GetCurMoney()
    {
        return curMoney;
    }

    public void IncHeart(int heart)
    {
        curHeart += heart;
    }

    public void DecHeart(int heart)
    {
        curHeart -= heart;
        transform.Find("heartAudio").GetComponent<AudioSource>().Play();
        if (curHeart <= 0)
        {
            LevelManager.instance.GameOver(false);
        }
    }

    public float GetCurHeart()
    {
        return curHeart;
    }

    private void IncMoneyAuto()
    {
        curMoney += 5;
    }
}
