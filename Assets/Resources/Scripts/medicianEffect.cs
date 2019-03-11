using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medicianEffect : MonoBehaviour {

    private float bornTime;
    private float lifeTime;

	// Use this for initialization
	void Start () {
        bornTime = Time.time;
        lifeTime = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= bornTime + lifeTime)
        {
            Destroy(this.gameObject);
        }
	}
}
