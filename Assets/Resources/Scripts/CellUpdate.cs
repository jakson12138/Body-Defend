using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellUpdate : MonoBehaviour {

    private GameObject ChoosePanel;
    public GameObject Prefab;
    //public GameObject Canvas;

    private int layerMaskCellBtn;

	// Use this for initialization
	void Start () {
        layerMaskCellBtn = LayerMask.GetMask("cellBtn");
        ChoosePanel = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (ChoosePanel != null && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePositionOnScreen = Input.mousePosition;
            Vector3 mousePositionOnWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

            Collider2D hitCellBtn = Physics2D.OverlapPoint(mousePositionOnWorld, layerMaskCellBtn);

            //Debug.Log("hitCellBtn:" + hitCellBtn);

            if(hitCellBtn != null)
            {
                CellBtn CB = hitCellBtn.GetComponent<CellBtn>();
                ShowUpdate();
                if(CB.CBT == CellBtn.CellBtnType.Update)
                {
                    MakeCellUpdate();
                }
                else
                {
                    CancelBtn();
                }
            }
        }
	}

    public void ShowUpdate()
    {
        if(ChoosePanel == null)
        {
            LevelManager.instance.GameStop();

            ChoosePanel = GameObject.Instantiate<GameObject>(Prefab);
            ChoosePanel.transform.SetParent(this.transform);
            Vector3 spSize = this.GetComponent<SpriteRenderer>().bounds.size;
            Vector3 scale = this.transform.localScale;
            ChoosePanel.transform.localPosition = new Vector3(0, spSize.y / scale.y + 0.1f, 0);
        }
        else
        {
            LevelManager.instance.GameContinue();

            Destroy(ChoosePanel);
            ChoosePanel = null;
        }
    }

    public void MakeCellUpdate()
    {

        if(this.GetComponent<Cells>().CT == CellType.BCell)
        {
            this.GetComponent<BCell>().CellUpdate();
        }
        if(this.GetComponent<Cells>().CT == CellType.WhiteCell)
        {
            this.GetComponent<WhiteCell>().CellUpdate();
        }
    }

    private void CancelBtn()
    {
        MoneyManager.instance.IncMoney(50f);
        Destroy(this.gameObject);
    }
}
