using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCell : MonoBehaviour {

    private GameObject curTarget;
    private float speed;

    private bool attackInFlame;

    private int myLevel;

    private float[] mySpeeds = { 1.5f, 2.5f, 4f };
    private float[] myHps = { 100f, 150f, 200f };
    private float[] myDamages = { 1f, 2f, 3f };

	// Use this for initialization
	void Start () {
        FindClosestMon();
        myLevel = 0;
        speed = mySpeeds[myLevel];
	}
	
	// Update is called once per frame
	void Update () {
		if(curTarget != null)
        {
            Vector2 dir = curTarget.transform.position - this.transform.position;

            dir.Normalize();
            this.transform.Translate(dir * speed * Time.deltaTime);
        }
        else
        {
            FindClosestMon();
        }

        if (attackInFlame)
            attackInFlame = false;
	}

    private void FindClosestMon()
    {
        Monster[] Mons = GameObject.FindObjectsOfType<Monster>();
        float minDistance = float.PositiveInfinity;

        Vector2 pos = this.transform.position;

        for(int i = 0;i < Mons.Length; i++)
        {
            Vector2 monPos = Mons[i].transform.position;
            float dis = Vector2.Distance(pos, monPos);
            if(dis < minDistance)
            {
                curTarget = Mons[i].gameObject;
                minDistance = dis;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.GetComponent<AudioSource>().Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "mon")
        {
            //Destroy(this.gameObject);
            if(this.GetComponent<CellAttack>().canAttack && !attackInFlame)
            {
                attackInFlame = true;
                this.GetComponent<CellAttack>().Attack(collision.gameObject);
            }
        }
    }

    public void CellUpdate()
    {
        if (myLevel >= 2 || !MoneyManager.instance.DecMoney(100f)) return;
        myLevel++;
        speed = mySpeeds[myLevel];
        float Hp = myHps[myLevel];
        float Damage = myDamages[myLevel];
        this.GetComponent<CellAttack>().CellUpdate(Hp, Damage);
    }
}
