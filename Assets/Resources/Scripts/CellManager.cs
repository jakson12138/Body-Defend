using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellManager : MonoBehaviour {

    public GameObject[] Prefabs;

    public static CellManager instance;

    public int curToolIndex;
    private Vector3 ClickPosOnWorld;
    private int layerMaskCell;
    private int layerMaskCellBtn;
    public float[] moneyOfTools;

    private GameObject curCell;

    // Use this for initialization
    void Start () {
        instance = this;

        ClickPosOnWorld = Vector3.zero;
        curToolIndex = -1;
        layerMaskCell = LayerMask.GetMask("cell");
        layerMaskCellBtn = LayerMask.GetMask("cellBtn");

        curCell = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (curToolIndex != -1)
        {
            if (Input.touchCount == 1)
            {
                Vector3 mousePositionOnScreen = Input.touches[0].position;
                Vector3 mousePositionOnWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

                ClickPosOnWorld = mousePositionOnWorld;
                ClickPosOnWorld.z = 0;

                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    Collider2D hitCell = Physics2D.OverlapPoint(mousePositionOnWorld, layerMaskCell);
                    Collider2D hitCellBtn = Physics2D.OverlapPoint(mousePositionOnWorld, layerMaskCellBtn);

                    if (hitCell == null && hitCellBtn == null && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    {
                        if (MoneyManager.instance.DecMoney(moneyOfTools[curToolIndex]))
                        {
                            ProduceCell(curToolIndex, ClickPosOnWorld, false);
                            if (curCell != null && curCell.GetComponent<CellAttack>() != null)
                                curCell.GetComponent<CellAttack>().SetAttack();
                            curCell = null;
                        }
                    }
                    else if (hitCell != null)
                    {
                        if (hitCell.GetComponent<CellUpdate>() != null)
                            hitCell.GetComponent<CellUpdate>().ShowUpdate();
                    }
                }
            }
            else if (Input.GetButton("Fire1"))
            {
                Vector3 mousePositionOnScreen = Input.mousePosition;
                Vector3 mousePositionOnWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

                ClickPosOnWorld = mousePositionOnWorld;
                ClickPosOnWorld.z = 0;

                if (Input.GetButtonDown("Fire1"))
                {
                    Collider2D hitCell = Physics2D.OverlapPoint(mousePositionOnWorld, layerMaskCell);
                    Collider2D hitCellBtn = Physics2D.OverlapPoint(mousePositionOnWorld, layerMaskCellBtn);

                    if (curCell == null)
                    {
                        if (hitCell == null && hitCellBtn == null && !EventSystem.current.IsPointerOverGameObject())
                        {
                            if (MoneyManager.instance.DecMoney(moneyOfTools[curToolIndex]))
                            {
                                ProduceCell(curToolIndex, ClickPosOnWorld, false);
                                if (curCell != null && curCell.GetComponent<CellAttack>() != null)
                                    curCell.GetComponent<CellAttack>().SetAttack();
                                curCell = null;
                            }
                        }
                        else if (hitCell != null)
                        {
                            if (hitCell.GetComponent<CellUpdate>() != null)
                                hitCell.GetComponent<CellUpdate>().ShowUpdate();
                        }
                    }
                }
            }
        }
        else
        {
            if (Input.touchCount == 1)
            {
                Vector3 mousePositionOnScreen = Input.touches[0].position;
                Vector3 mousePositionOnWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

                Collider2D hitCell = Physics2D.OverlapPoint(mousePositionOnWorld, layerMaskCell);

                if (hitCell != null)
                {
                    if (hitCell.GetComponent<CellUpdate>() != null)
                        hitCell.GetComponent<CellUpdate>().ShowUpdate();
                }
            }
            else if(Input.GetButtonDown("Fire1"))
            {
                Vector3 mousePositionOnScreen = Input.mousePosition;
                Vector3 mousePositionOnWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

                Collider2D hitCell = Physics2D.OverlapPoint(mousePositionOnWorld, layerMaskCell);

                //Debug.Log(hitCell);

                if (hitCell != null)
                {
                    if (hitCell.GetComponent<CellUpdate>() != null)
                        hitCell.GetComponent<CellUpdate>().ShowUpdate();
                }
            }
        }
    }   

    public void ProduceCell(int index,Vector3 pos,bool isBloodCell)
    {
        pos.z = 0;

        if(index >= 0 && index < Prefabs.Length)
        {
            if (!isBloodCell)
            {
                GameObject cell = GameObject.Instantiate<GameObject>(Prefabs[index]);
                //Debug.Log(cell.layer);
                cell.transform.position = pos;

                if (cell.GetComponent<Cells>().CT == CellType.WhiteCell)
                {
                    cell.GetComponent<Animator>().SetBool("Produce", false);
                    cell.GetComponent<Animator>().SetBool("Attack", true);
                }

                curCell = cell;
            }
            else
            {
                GameObject cell = GameObject.Instantiate<GameObject>(Prefabs[index]);
                cell.transform.position = pos;

                if(cell.GetComponent<Cells>().CT == CellType.WhiteCell)
                {
                    cell.GetComponent<Animator>().SetBool("Produce", true);
                    cell.GetComponent<Animator>().SetBool("Attack", false);

                    cell.GetComponent<CellAttack>().SetAttack();
                }
            }
        }

        LevelManager.instance.GameContinue();
    }
}
