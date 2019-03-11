using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCell : MonoBehaviour {

    public GameObject bCellAttack;
    private GameObject curTarget;
    public float maxDistance;
    public float shotInterval;
    private float lastShotTime;
    private float myDamage;
    private Vector3 myScale;

    private float[] myScales = { 1f, 1.1f, 1.2f };
    private int myLevel;
    private float[] myMaxDis = { 3f, 4f, 5f };
    private float[] myShotInterval = { 1f, 0.7f, 0.4f };
    private float[] myDamages = { 20f, 30f, 40f };

	// Use this for initialization
	void Start () {
        myLevel = 0;
        lastShotTime = 0;

        maxDistance = myMaxDis[myLevel];
        shotInterval = myShotInterval[myLevel];
        myDamage = myDamages[myLevel];
        myScale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        bool canAttack = this.GetComponent<CellAttack>().canAttack;
		if(curTarget != null && Time.time >= lastShotTime + shotInterval && canAttack)
        {
            lastShotTime = Time.time;

            this.GetComponent<AudioSource>().Play();

            GameObject bCA = GameObject.Instantiate<GameObject>(bCellAttack);
            bCA.transform.position = this.transform.position;
            bCA.GetComponent<bCellAttack>().SetUpdate(myDamage);
            bCA.GetComponent<bCellAttack>().SetTarget(curTarget);
        }
        else
        {
            FindNextTarget();
        }
	}

    private void FindNextTarget()
    {
        GameObject[] mons = GameObject.FindGameObjectsWithTag("mon");
        float minDistance = maxDistance;

        for(int i = 0;i < mons.Length; i++)
        {
            Vector3 monPos = mons[i].transform.position;
            float dis = Vector3.Distance(this.transform.position, monPos);
            if(dis <= minDistance)
            {
                curTarget = mons[i];
                minDistance = dis;
            }
        }
    }

    public void CellUpdate()
    {
        if (myLevel >= 2 || !MoneyManager.instance.DecMoney(100f)) return;

        myLevel++;
        myDamage = myDamages[myLevel];
        shotInterval = myShotInterval[myLevel];
        maxDistance = myMaxDis[myLevel];

        this.transform.localScale = myScale * myScales[myLevel];
    }
}
