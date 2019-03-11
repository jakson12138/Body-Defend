using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelShow : MonoBehaviour {

    public string[] introText;
    public string[] adviceText;
    public string[] moreIntroText;
    public string[] moreCellText;
    public GameObject moreIntro;
    public GameObject moreCell;
    public GameObject introduction;
    public GameObject advice;
    public GameObject[] buttons;
    private int nowLevel;

	// Use this for initialization
	void Start () {
        nowLevel = 0;
        //Show(nowLevel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Show(int index)
    {
        if (index == nowLevel) return;

        introduction.transform.GetComponent<Text>().text = introText[index];
        advice.transform.GetComponent<Text>().text = adviceText[index];

        moreIntro.transform.GetComponent<Text>().text = moreIntroText[index];
        moreCell.transform.GetComponent<Text>().text = moreCellText[index];

        Vector4 color1 = new Vector4(1.0f, 1.0f, 1.0f, 0.196f);
        Vector4 color2 = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        //Vector4 color3 = new Vector4(255, 255, 255, 0);

        //Debug.Log(buttons[nowLevel].transform.GetComponent<Image>().color);

        if (index == 0)
        {
            buttons[nowLevel].transform.GetComponent<Image>().color = color1;
        }
        else if (nowLevel == 0)
        {
            buttons[index].transform.GetComponent<Image>().color = color2;
        }
        else
        {
            buttons[nowLevel].transform.GetComponent<Image>().color = color1;
            buttons[index].transform.GetComponent<Image>().color = color2;
        }
        
        //Debug.Log(buttons[nowLevel].transform.GetComponent<Image>().color);

        nowLevel = index;

        LevelSelectUI.index = index;
    }

    public void Level0()
    {
        Show(0);
    }

    public void Level1()
    {
        Show(1);
    }

    public void Level2()
    {
        Show(2);
    }

    public void Level3()
    {
        Show(3);
    }
}
