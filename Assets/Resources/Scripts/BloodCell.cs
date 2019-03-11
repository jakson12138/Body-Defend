using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCell : MonoBehaviour {

    private float proInterval;//生产速度
    private float startProTime;
    private float deathTime;

	void Start () {
        proInterval = 15f;
        startProTime = 4f;
        deathTime = 80f;
        StartCoroutine("Death");
        InvokeRepeating("ProduceCell", startProTime, proInterval);
	}

    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        CancelInvoke();
        Destroy(this.gameObject);
    }

    private void ProduceCell()
    {
        int index = LevelUI.instance.IndexOfTools("WhiteCell");
        CellManager.instance.ProduceCell(index, this.transform.position, true);
    }
}
