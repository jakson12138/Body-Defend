using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBar : MonoBehaviour {

    public void SetBarParent(Transform parent)
    {
        transform.SetParent(parent);
        Vector3 spSize = parent.GetComponent<SpriteRenderer>().bounds.size;
        Vector3 scale = parent.localScale;
        transform.localPosition = new Vector3(0, spSize.y / scale.y / 2, 0);
    }

    /*
    private void Update()
    {
        Transform parent = transform.parent;
        Vector3 spSize = parent.GetComponent<SpriteRenderer>().bounds.size;
        Vector3 scale = parent.localScale;
        transform.localPosition = new Vector3(0, spSize.y / scale.y / 2, 0);
    }
    */
}
