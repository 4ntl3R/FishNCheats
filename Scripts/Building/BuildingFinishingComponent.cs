using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingFinishingComponent : MonoBehaviour
{
	public float cost;
	public Sprite finishedSprite;
	public int stepsToComplete;
	public int xSize;
	public int ySize;
	public float AlertRateByDefault = 5;
	private float completePercent;
	private float stepCost;
	private float stepPercent;
	
	public delegate void BuildMessage(float percent);
	public event BuildMessage OnBuildStep;
	

    void Awake()
    {
	    stepCost = cost / stepsToComplete;
	    completePercent = 0;
	    stepPercent = 100 / stepsToComplete;
    }

    public bool BuilderStep()
    {
	    bool keepDoing = true;
	    if (ResourcesStorage.Instance.IsThereSomeResources())
	    {
		    completePercent += stepPercent;
		    if (OnBuildStep != null)
			    OnBuildStep(completePercent);
		    stepsToComplete--;
		    ChangeResources(-1*stepCost);
		    if (stepsToComplete <= 0)
		    {
			    Complete();
			    keepDoing = false;
		    }
	    }
	    else
	    {
		    keepDoing = false;
	    }

	    return keepDoing;
    }

    private void Complete()
	{
		gameObject.GetComponent<SpriteRenderer>().sprite = finishedSprite;
		gameObject.GetComponent<AudioSource>().Play();
		ActivateBuilding();
		Destroy(this);
	}
	
	private void ActivateBuilding()
	{
		this.gameObject.GetComponent<ProgressFarm>().enabled = true;
		gameObject.tag = "FinishedBuilding";
		Destroy(gameObject.GetComponent<AlertState>());
		Destroy(gameObject.GetComponent<Rigidbody2D>());
		Destroy(gameObject.GetComponent<Collider2D>());
	}
	
	private void Demolish()
	{
		Tilemap buildZones = GameObject.FindWithTag("TilemapBuildZones").GetComponent<Tilemap>();
		for (int x=0;x<xSize;x++)
			for (int y=0;y<ySize;y++)
				buildZones.SetTile(new Vector3Int(buildZones.WorldToCell(transform.position).x+x, buildZones.WorldToCell(transform.position).y+y, 0), null);
		Destroy(this.gameObject);	
	}

	private void ChangeResources(float amount)
	{
		ResourcesStorage.Instance.UpdateResources(amount);
	}
	
}
