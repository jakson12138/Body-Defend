using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour {

    public GameObject MorePanel;

    private string[] Levels = { "Level1", "Level2", "Level3", "Level4" };
    static public int index;

	// Use this for initialization
	void Start () {
        index = 0;
        MorePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnQuitBtn()
    {
        SceneManager.LoadScene("Title");
    }

    public void OnPlayBtn()
    {
        SceneManager.LoadScene(Levels[index]);
    }

    public void OnMoreBtn()
    {
        MorePanel.SetActive(true);
    }
    
    public void OnQuitMoreBtn()
    {
        MorePanel.SetActive(false);
    }
}
