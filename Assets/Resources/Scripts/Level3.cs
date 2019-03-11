using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : Level
{
    private int[] Waves = { };

    public GameObject startPanel1;
    //public GameObject startPanel2;
    //public GameObject startPanel3;

    //public GameObject updateIntroPanel;

    // Use this for initialization
    public Level3() { }

    public override void startAction()
    {
        LevelManager.instance.setMoreWay(true);
        LevelManager.instance.GameStop();
        startPanel1.SetActive(true);
    }

    /*
    public override void nextAction(int curWave)
    {
        
    }

    public override void endAction()
    {

    }
    */

    public void OnStartPanelQuitBtn()
    {
        startPanel1.SetActive(false);
        LevelManager.instance.GameContinue();
    }
}
