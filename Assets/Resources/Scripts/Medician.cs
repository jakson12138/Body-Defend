using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medician : MonoBehaviour {

    public GameObject EffectPrefab;

    private float lifeTime;
    private float bornTime;
    private float effectTime;
    private int playCount;

    private bool born;
    public float AttackDistance = 3f;
    public float myDamage = 50f;

	// Use this for initialization
	void Start () {
        bornTime = Time.time;
        born = false;
        effectTime = 1f;
        lifeTime = 2f;
        playCount = 1;
	}
	
	// Update is called once per frame
	void Update () {
        bool canAttack = this.GetComponent<CellAttack>().canAttack;

        if (canAttack)
        {
            if (!born)
            {
                born = true;
                bornTime = Time.time;

                this.GetComponent<Animator>().SetBool("canAttack", true);
            }
            else
            {
                if (Time.time >= bornTime + effectTime && playCount-- > 0)
                {
                    GameObject Effect = GameObject.Instantiate<GameObject>(EffectPrefab);
                    Effect.transform.position = this.transform.position;

                    this.GetComponent<AudioSource>().Play();

                    AttackMons();
                }
                if (Time.time >= bornTime + lifeTime)
                {
                    Destroy(this.gameObject);
                }
            }
        }
	}

    private void AttackMons()
    {
        GameObject[] mons = GameObject.FindGameObjectsWithTag("mon");

        for(int i = 0;i < mons.Length; i++)
        {
            Vector3 monPos = mons[i].transform.position;
            float dis = Vector2.Distance(monPos, this.transform.position);

            if (dis <= AttackDistance)
                mons[i].GetComponent<Monster>().BeAttack(myDamage);
        }
    }
}
