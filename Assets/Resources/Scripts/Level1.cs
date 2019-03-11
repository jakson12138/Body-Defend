using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : Level {

    private int[] Waves = {1};

    public GameObject startPanel1;
    public GameObject startPanel2;
    public GameObject startPanel3;

    public GameObject updateIntroPanel;

	// Use this for initialization
	public Level1() { }

    public override void startAction()
    {
        LevelManager.instance.GameStop();
        startPanel1.SetActive(true);
    }

    public override void nextAction(int curWave)
    {
        for(int i = 0;i < Waves.Length; i++)
        {
            if(curWave == Waves[i])
            {
                curActionIndex++;
                Waves[i] = -1;
                if(curActionIndex == 1)
                {
                    UpdateIntroduction();
                }
            }
        }
    }

    public override void endAction()
    {
        
    }

    private void UpdateIntroduction()
    {
        LevelManager.instance.GameStop();
        updateIntroPanel.SetActive(true);
    }

    public override void print()
    {
        Debug.Log("I am Level1");
        Debug.Log("curActionIndex:" + curActionIndex);
    }

    public void OnStartPanelQuitBtn()
    {
        startPanel1.SetActive(false);
        //LevelManager.instance.GameContinue();
        startPanel2.SetActive(true);
    }

    public void OnStartPanel2Btn()
    {
        startPanel2.SetActive(false);
        startPanel3.SetActive(true);
    }

    public void OnStartPanel3Btn()
    {
        startPanel3.SetActive(false);
        LevelManager.instance.GameContinue();
    }

    public void OnUpdateIntroPanelQuitBtn()
    {
        updateIntroPanel.SetActive(false);
        LevelManager.instance.GameContinue();
    }
}
