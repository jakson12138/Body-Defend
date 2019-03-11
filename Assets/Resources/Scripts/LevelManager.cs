using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;

    public GameObject DefeatPanel;
    public GameObject VictoryPanel;
    public GameObject TC;

    public int waveNum = 10;
    public int[] monNums;
    public float[] waveProduceInterval;
    public int curMonNum;

    public int curWave;
    private float waveInterval = 10f;
    private float timeCountStart = 5f;
    private float lastWaveTime;
    private float produceMonInterval;
    private float lastProduce;
    private bool isWait = false;
    private int playCount;
    private int timeCount;

    public Level curLevel;

    public bool moreWay;

    
    public void setMoreWay(bool mw)
    {
        moreWay = mw;
    }

    public bool getMoreWay()
    {
        return moreWay;
    }
    

	// Use this for initialization
	void Start () {
        instance = this;
        moreWay = false;
        //curLevel.print();
        GameStart();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isWait && curWave < waveNum && monNums[curWave] > 0)
        {
            if (Time.time >= lastProduce + produceMonInterval)
            {
                MonManager.instance.preduceMon();
                curMonNum++;
                lastProduce = Time.time;
                monNums[curWave]--;
            }
        }
        else if (curMonNum <= 0)
        {
            if (!isWait)
            {
                curWave++;
                isWait = true;
                lastWaveTime = Time.time;
            }
            else
            {
                if(curWave >= waveNum && NoMons())
                {
                    if(playCount-- > 0)
                        GameOver(true);
                }

                curLevel.nextAction(curWave);

                if (Time.time >= lastWaveTime + waveInterval - timeCountStart && timeCount-- > 0)
                {
                    TimeCount();
                }
                else if(Time.time >= lastWaveTime + waveInterval && curWave < waveNum)
                {
                    isWait = false;
                    produceMonInterval = waveProduceInterval[curWave];
                    timeCount = 1;
                    transform.Find("MonsterAudio").GetComponent<AudioSource>().Play();
                }
            }
        }
	}

    private void TimeCount()
    {
        this.GetComponent<AudioSource>().Play();
        TC.SetActive(true);
        TC.GetComponent<Animator>().Play("timeCount");
        StartCoroutine(StopTimeCount());
    }

    IEnumerator StopTimeCount()
    {
        yield return new WaitForSeconds(timeCountStart);
        TC.SetActive(false);
        this.GetComponent<AudioSource>().Stop();
    }

    public void GameStart()
    {
        isWait = true;
        playCount = 1;
        timeCount = 1;
        curWave = 0;
        produceMonInterval = waveProduceInterval[curWave];
        lastWaveTime = Time.time;
        curMonNum = 0;

        curLevel.startAction();
    }

    public void GameStop()
    {
        Time.timeScale = 0;
    }

    public void GameContinue()
    {
        Time.timeScale = 1;
    }

    public void GameOver(bool win)
    {
        Time.timeScale = 0;

        if (win)
        {
            VictoryPanel.SetActive(true);

            Transform VictoryAudio = this.transform.Find("VictoryAudio");
            //Debug.Log(VictoryAudio.GetComponent<AudioSource>().clip);

            VictoryAudio.GetComponent<AudioSource>().Play();
        }
        else
        {
            DefeatPanel.SetActive(true);

            Transform DefeatAudio = this.transform.Find("DefeatAudio");
            DefeatAudio.GetComponent<AudioSource>().Play();
        }

        curLevel.endAction();
        //Time.timeScale = 0;
    }

    private bool NoMons()
    {
        GameObject[] mons = GameObject.FindGameObjectsWithTag("mon");

        if (mons.Length == 0) return true;
        else return false;
    }
}
