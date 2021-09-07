using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesStorage : Singleton<ResourcesStorage>
{
    public float resources = 100;
	public float mineIncrease = 20;

	public void UpdateResources(float updateValue)
	{
		resources+=updateValue;
		InfoHandler.Instance.OnResourcesChange(resources);
	}
	
	public void MineIncrease()
	{
		UpdateResources(mineIncrease);
	}
	

	public bool IsThereSomeResources()
	{
		return !(resources < 0);
	}
}
