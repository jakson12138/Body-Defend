using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour {

    public GameObject ToolsPanel;
    public string[] tools;
    public string[] introductionText;
    public GameObject moneyText;
    public GameObject heartText;
    public GameObject waveText;
    public static LevelUI instance;

    //public GameObject testText;

    public Sprite ON;
    public Sprite OFF;
    public GameObject stopBtn;
    private bool on;

	// Use this for initialization
	void Start () {
        on = true;
        instance = this;
	}

	void Update () {
        moneyText.GetComponent<Text>().text = MoneyManager.instance.GetCurMoney().ToString();
        heartText.GetComponent<Text>().text = MoneyManager.instance.GetCurHeart().ToString();

        string waveString = "";
        waveString += LevelManager.instance.curWave + 1;
        waveString += '/';
        waveString += LevelManager.instance.waveNum;
        waveText.GetComponent<Text>().text = waveString;
	}

    public void OnQuitBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelect");
    }

    public void OnPanelWhiteCellBtn()
    {
        LevelManager.instance.GameStop();

        Transform introductionPanel = ToolsPanel.transform.Find("introductionPanel");
        introductionPanel.gameObject.SetActive(true);
        Transform Text = introductionPanel.Find("introductionText");

        Transform WhiteCell = ToolsPanel.transform.Find("WhiteCellBtn");
        Transform YellowRect = ToolsPanel.transform.Find("yellowRect");

        if(YellowRect == null)
        {
            foreach(Transform child in ToolsPanel.transform)
            {
                YellowRect = child.transform.Find("yellowRect");
                if (YellowRect != null) break;
            }
        }

        if (YellowRect.gameObject.activeSelf && YellowRect.transform.parent == WhiteCell)
        {          
            YellowRect.gameObject.SetActive(false);
            YellowRect.SetParent(ToolsPanel.transform);
            CellManager.instance.curToolIndex = -1;

            introductionPanel.gameObject.SetActive(false);
            LevelManager.instance.GameContinue();
        }
        else
        {
            YellowRect.SetParent(WhiteCell);
            YellowRect.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            YellowRect.gameObject.SetActive(true);

            int index = 0;
            for (int i = 0; i < tools.Length; i++)
            {
                if (tools[i] == "WhiteCell")
                {
                    index = i;
                    break;
                }
            }

            CellManager.instance.curToolIndex = index;
            Text.GetComponent<Text>().text = introductionText[CellManager.instance.curToolIndex];
        }

        
    }

    public void OnPanelBloodCellBtn()
    {
        LevelManager.instance.GameStop();

        Transform introductionPanel = ToolsPanel.transform.Find("introductionPanel");
        introductionPanel.gameObject.SetActive(true);
        Transform Text = introductionPanel.Find("introductionText");

        Transform BCell = ToolsPanel.transform.Find("BloodCellBtn");
        Transform YellowRect = ToolsPanel.transform.Find("yellowRect");

        if(YellowRect == null)
        {
            foreach (Transform child in ToolsPanel.transform)
            {
                YellowRect = child.transform.Find("yellowRect");
                if (YellowRect != null) break;
            }
        }

        if (YellowRect.gameObject.activeSelf && YellowRect.transform.parent == BCell)
        {
            YellowRect.gameObject.SetActive(false);
            YellowRect.SetParent(ToolsPanel.transform);
            CellManager.instance.curToolIndex = -1;

            introductionPanel.gameObject.SetActive(false);
            LevelManager.instance.GameContinue();
        }
        else
        {
            YellowRect.SetParent(BCell);
            YellowRect.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            YellowRect.gameObject.SetActive(true);

            int index = 0;
            for (int i = 0; i < tools.Length; i++)
            {
                if (tools[i] == "BloodCell")
                {
                    index = i;
                    break;
                }
            }

            CellManager.instance.curToolIndex = index;

            Text.GetComponent<Text>().text = introductionText[CellManager.instance.curToolIndex];
        }
    }

    public void OnPanelMedicianBtn()
    {
        LevelManager.instance.GameStop();

        Transform introductionPanel = ToolsPanel.transform.Find("introductionPanel");
        introductionPanel.gameObject.SetActive(true);
        Transform Text = introductionPanel.Find("introductionText");

        Transform Medician = ToolsPanel.transform.Find("MedicianBtn");
        Transform YellowRect = ToolsPanel.transform.Find("yellowRect");

        if (YellowRect == null)
        {
            foreach (Transform child in ToolsPanel.transform)
            {
                YellowRect = child.transform.Find("yellowRect");
                if (YellowRect != null) break;
            }
        }

        if (YellowRect.gameObject.activeSelf && YellowRect.transform.parent == Medician)
        {
            YellowRect.gameObject.SetActive(false);
            YellowRect.SetParent(ToolsPanel.transform);
            CellManager.instance.curToolIndex = -1;

            introductionPanel.gameObject.SetActive(false);
            LevelManager.instance.GameContinue();
        }
        else
        {
            YellowRect.SetParent(Medician);
            YellowRect.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            YellowRect.gameObject.SetActive(true);

            int index = 0;
            for (int i = 0; i < tools.Length; i++)
            {
                if (tools[i] == "Medician")
                {
                    index = i;
                    break;
                }
            }

            CellManager.instance.curToolIndex = index;

            Text.GetComponent<Text>().text = introductionText[CellManager.instance.curToolIndex];
        }
    }

    public void OnHealBtn()
    {
        if (ToolsPanel.activeSelf)
        {
            ToolsPanel.SetActive(false);
        }
        else ToolsPanel.SetActive(true);
    }

    public void OnDefeatPanelReplayBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");
    }

    public void OnStopBtn()
    {
        if (on)
        {
            LevelManager.instance.GameStop();
            stopBtn.GetComponent<Image>().sprite = OFF;
            on = false;
        }
        else
        {
            LevelManager.instance.GameContinue();
            stopBtn.GetComponent<Image>().sprite = ON;
            on = true;
        }
    }

    public int IndexOfTools(string name)
    {
        for(int i = 0;i < tools.Length; i++)
        {
            if(tools[i] == name)
            {
                return i;
            }
        }
        return -1;
    }
}
