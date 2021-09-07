using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : Singleton<ActiveObject>
{
    private GameObject activeObject;

    public void Set(GameObject activate)
    {
        activeObject = activate;
    }

    public GameObject Get()
    {
        return activeObject;
    }

    public void Clear()
    {
        activeObject= null;
    }
}
