using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour {

    private GameObject bar;

    public GameObject barPrefabs;

    private void Awake()
    {
        bar = GameObject.Instantiate<GameObject>(barPrefabs);
        BloodBar Bb = bar.GetComponent<BloodBar>();
        Bb.SetBarParent(this.transform);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setCurHp(float curHp,float maxHp)
    {
        float rate = curHp / maxHp;
        Transform barTransform = bar.transform.Find("blood");
        Vector3 scale = barTransform.localScale;
        scale.x = rate;
        barTransform.localScale = scale;
    }
}
