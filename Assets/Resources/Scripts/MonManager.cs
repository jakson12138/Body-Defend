using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonManager : MonoBehaviour {

    public GameObject[] prefabs;

    public GameObject[] startPos;

    public static MonManager instance;

	// Use this for initialization
	void Start () {
        //InvokeRepeating("preduceMon",0,3);
        //Debug.Log("111");
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void preduceMon()
    {
        GameObject mon = GameObject.Instantiate<GameObject>(prefabs[0]);

        int length = startPos.Length;
        int index = Random.Range(0, length);

        //Debug.Log(index);

        mon.transform.position = startPos[index].transform.position;
        if (LevelManager.instance.getMoreWay())
        {
            //Debug.Log(111);
            mon.transform.GetComponent<Monster>().setMoreWay(index);
        }
        //mon.AddComponent<Monster>();
    }
}
