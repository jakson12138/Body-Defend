using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour {

    public GameObject playBtn;
    public GameObject introductionBtn;
    public GameObject introductionPanel;

    public void onTitlePlayBtn()
    {
        SceneManager.LoadScene("LevelSelect");
    }

	// Use this for initialization
	void Start () {
        //playBtn.GetComponent<AudioSource>().enabled = false;
        playBtn.SetActive(false);
        introductionBtn.SetActive(false);
        StartCoroutine(SetBtnActive());
    }

    IEnumerator SetBtnActive()
    {
        yield return new WaitForSeconds(1);
        playBtn.SetActive(true);
        introductionBtn.SetActive(true);
    }

    public void OnIntroductionBtn()
    {
        introductionPanel.SetActive(true);
    }

    public void OnIntroductionQuitBtn()
    {
        introductionPanel.SetActive(false);
    }
}
