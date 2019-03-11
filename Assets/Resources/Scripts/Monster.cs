using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    private GameObject curPoint;
    private int pointIndex;
    private int pointLength;
    private int myPath;
    private int pathNum;
    private int deathCount = 1;

    private bool attackInFlame;

    public float speed = 1f;
    public float maxHp = 100;
    private float curHp;
    public float myDamage = 0.3f;
    public float myMoney = 50f;
    //private bool moreWay;

	// Use this for initialization
	void Awake () {
        //Debug.Log("start");
        //moreWay = false;
        pointIndex = 0;
        pathNum = PointManager.instance.GetPathNum();
        myPath = Random.Range(0,pathNum);
        pointLength = PointManager.instance.GetPointsSize(myPath);
        curPoint = PointManager.instance.GetPointByIndex(myPath,pointIndex);

        curHp = maxHp;

        attackInFlame = false;
    }
	
    public void setMoreWay(int mP)
    {
        //Debug.Log("set");
        pointIndex = 0;
        myPath = mP;
        pointLength = PointManager.instance.GetPointsSize(myPath);
        curPoint = PointManager.instance.GetPointByIndex(myPath, pointIndex);
    }

	// Update is called once per frame
	void Update () {
        if (curPoint != null)
        {
            if (Vector2.Distance(this.transform.position, curPoint.transform.position) < 0.5f)
            {
                curPoint = FindNextPoint();
            }

            if (curPoint != null)
            {
                Vector2 dir = curPoint.transform.position - this.transform.position;
                dir.Normalize();
                this.transform.Translate(dir * speed * Time.deltaTime);
            }
        }
        else
        {
            MoneyManager.instance.DecHeart(1);
            
            LevelManager.instance.curMonNum--;
            Destroy(this.gameObject);
        }

        if (attackInFlame)
            attackInFlame = false;
	}

    GameObject FindNextPoint()
    {
        pointIndex++;
        if(pointIndex >= 0 && pointIndex < pointLength)
        {
            return PointManager.instance.GetPointByIndex(myPath,pointIndex);
        }
        return null;
    }

    public void BeAttack(float damage)
    {
        //Debug.Log("BA");
        curHp -= damage;
        if(curHp <= 0)
        {
            if(deathCount-- > 0)
            {
                Destroy(this.gameObject);
                LevelManager.instance.curMonNum--;
                MoneyManager.instance.IncMoney(myMoney);
            }
        }
        else
        {
            UpdateHp();
        }
    }

    private void UpdateHp()
    {
        MonsterHealth health = transform.GetComponent<MonsterHealth>();
        health.setCurHp(curHp, maxHp);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "cell")
        {
            AttackCell(collision.gameObject);
        }
    }

    private void AttackCell(GameObject cell)
    {
        if(cell.GetComponent<CellAttack>() != null && cell.GetComponent<CellAttack>().canBeAttack && !attackInFlame)
        {
            cell.GetComponent<CellAttack>().BeAttack(myDamage);
            attackInFlame = true;
        } 
    }
}
