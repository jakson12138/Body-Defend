using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAttack : MonoBehaviour {

    public float myDamage = 1f;

    public float maxHp = 100f;
    private float curHp;

    public bool canAttack;
    public bool canBeAttack;
    public bool canBA;

    public void CellUpdate(float Hp,float Damage)
    {
        maxHp = Hp;
        curHp = maxHp;
        myDamage = Damage;
        UpdateHp();
    }

	// Use this for initialization
	void Start () {
        curHp = maxHp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack(GameObject mon)
    {
        if(mon.GetComponent<Monster>() != null)
            mon.GetComponent<Monster>().BeAttack(myDamage);
    }

    public void BeAttack(float damage)
    {
        curHp -= damage;
        if (curHp <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            UpdateHp();
        }
    }

    private void UpdateHp()
    {
        MonsterHealth MH = this.GetComponent<MonsterHealth>();
        if(MH != null)
            MH.setCurHp(curHp, maxHp);
    }

    public void SetAttack()
    {
        if (canBA)
            canBeAttack = true;
        else
            canBeAttack = false;

        canAttack = true;
    }
}
