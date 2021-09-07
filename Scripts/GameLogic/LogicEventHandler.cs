using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicEventHandler : Singleton<LogicEventHandler>
{
    public int countHeroes;
    public static int maxCountHeroes = 5;
    public GameObject[] heroes = new GameObject[maxCountHeroes];
    private ActiveObject activeObjectComp;

    private void Awake()
    {
        activeObjectComp = GetComponent<ActiveObject>();
    }

    void Update()
    {
        GameObject activeObject = activeObjectComp.Get();
        if (countHeroes <= maxCountHeroes)
        {
            for (int i = 0; i < countHeroes; i++)
            {
                if (Input.GetButtonUp("Heroes" + (i + 1).ToString()))
                {
                    if ((activeObject != null) && (activeObject != heroes[i]))
                    {
                        activeObject.SendMessage("Deactivate");
                    }
                    heroes[i].SendMessage("Activate");
                }
            }
        }
    }
}
