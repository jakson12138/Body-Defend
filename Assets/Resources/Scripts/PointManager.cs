using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour {

    public GameObject[] points1;
    public GameObject[] points2;
    public GameObject[] points3;
    public int pathNum;
    public static PointManager instance;

	// Use this for initialization
	void Start () {
        instance = this;
	}

    public int GetPathNum()
    {
        return pathNum;
    }
    
    public int GetPointsSize(int myPath)
    {
        if (myPath == 0) return points1.Length;
        else if (myPath == 1) return points2.Length;
        else if(myPath == 2) return points3.Length;
        return 0;
    }
    
    public GameObject GetPointByIndex(int myPath,int index)
    {
        if(myPath == 0)
        {
            if (index >= 0 && index < points1.Length)
            {
                return points1[index];
            }
        }
        else if(myPath == 1)
        {
            if (index >= 0 && index < points2.Length)
            {
                return points2[index];
            }
        }
        else if(myPath == 2)
        {
            if(index >= 0 && index < points3.Length)
            {
                return points3[index];
            }
        }
        
        return null;
    }
}
