using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    protected int curActionIndex;

    public Level()
    {
        curActionIndex = 0;
    }

    public virtual void startAction()
    {

    }

    public virtual void nextAction(int curWave)
    {
 
    }

    public virtual void endAction()
    {

    }
    
    public virtual void print()
    {
        Debug.Log("I am Level");
        Debug.Log("curActionIndex:" + curActionIndex);
    }
}
