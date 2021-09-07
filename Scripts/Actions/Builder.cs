using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using UnityEngine;

public class Builder : BasicActionClass
{
	public GameObject currentBuilding;
	public float buildingTime = 1f;
	
    public void Build()
    {
	    StartCoroutine(BuildingProcess());
    }

    public override void StartThisAction(System.Object[] parameters)
    {
	    if (parameters.Length > 0)
	    {
		    if (parameters[0] is GameObject)
			    currentBuilding = (GameObject) parameters[0];
	    }
	    Build();
    }

    IEnumerator BuildingProcess()
    {
	    while (true)
		{
			yield return new WaitForSeconds(buildingTime);
			try
			{
				if (!currentBuilding.GetComponent<BuildingFinishingComponent>().BuilderStep())
					CallbackAction();
			}
			catch
			{
				CallbackAction();
			}
		}
    }

    public override void StopThisAction()
    {
	    base.StopThisAction();
	    currentBuilding = null;
    }
}
