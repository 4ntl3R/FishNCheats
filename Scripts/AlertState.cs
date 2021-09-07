using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AlertState : MonoBehaviour
{
	public float suspiciousness = 5;
	protected Tilemap hideouts;
	protected bool isHidden;

    protected virtual void Awake()
    {
		hideouts = GameObject.FindWithTag("TilemapHideouts").GetComponent<Tilemap>();
		isHidden = false;
    }


    protected virtual void ChangeState()
	{
		if (hideouts.GetTile(hideouts.WorldToCell(transform.position))!=null)
			isHidden = true;
		else
			isHidden = false;
	}
	
	public virtual void AlertOn()
	{
		ChangeState();
		if (!isHidden)
		{
			ScalesLogic.Instance.ChangeAlert(suspiciousness); 
		}

	}	
}
