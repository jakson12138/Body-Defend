using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bCellAttack : MonoBehaviour {

    private GameObject curTarget;

    public float speed = 5f;
    public float myDamage = 50f;

    private int attackCount;

	// Use this for initialization
	void Start () {
        attackCount = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(curTarget != null)
        {
            Vector3 targetPos = curTarget.transform.position;
            Vector3 pos = this.transform.position;
            Vector3 dir = targetPos - pos;

            dir.Normalize();

            this.transform.Translate(dir * speed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }
	}

    public void SetTarget(GameObject target)
    {
        curTarget = target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "mon" && attackCount-- > 0)
        {
            collision.GetComponent<Monster>().BeAttack(myDamage);
            Destroy(this.gameObject);
        }
    }

    public void SetUpdate(float mD)
    {
        myDamage = mD;
    }
}
