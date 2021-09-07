using System.Collections;
using System.Collections.Generic;
using Actions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mining : BasicActionClass
{
    public float miningTime;
    public int mineDistance;
	private Tilemap buildZoneTM;
	private Tilemap resourcesTM;
	private float timer;
	private List<Vector3Int> tilesToMine;
	
	void Awake()
	{
		buildZoneTM = GameObject.FindWithTag("TilemapBuildZones").GetComponent<Tilemap>();
		resourcesTM = GameObject.FindWithTag("TilemapResources").GetComponent<Tilemap>();
		tilesToMine = new List<Vector3Int>();
	}
	
	public void Mine()
	{
		Vector3Int minerTile = resourcesTM.WorldToCell(transform.position);
		tilesToMine.Clear();
		for (int x=-1; x<2; x++)
			for (int y=-1; y<2; y++)
				{
					Vector3Int currTile = new Vector3Int(minerTile.x+x, minerTile.y+y, minerTile.z);
					if (resourcesTM.GetTile(currTile)!=null)
						tilesToMine.Add(currTile);			
				}
		StartCoroutine(MiningProcess());
	}

	private void FindNext()
	{
		Vector3Int minerTile = resourcesTM.WorldToCell(transform.position);
		Vector3 newPoint = new Vector3();
		bool isNull = true;
		for (int x = -mineDistance; x < mineDistance + 1; x++)
		for (int y = -mineDistance; y < mineDistance + 1; y++)
		{
			Vector3Int currTile = new Vector3Int(minerTile.x + x, minerTile.y + y, minerTile.z);
			if (resourcesTM.GetTile(currTile) != null)
			{
				if (isNull)
				{
					newPoint = resourcesTM.CellToWorld(currTile);
					isNull = false;
				}
				else if (Vector3.Distance(newPoint, transform.position) >
				         Vector3.Distance(resourcesTM.CellToWorld(currTile), transform.position))
				{
					newPoint = resourcesTM.CellToWorld(currTile);
					isNull = false;
				}
			}
		}

		if (!isNull)
		{
			MoveToNextMineNode(newPoint);
		}
	}

	private void MoveToNextMineNode(Vector3 nodePosition)
	{
		GetComponent<ActionQueue>().EnqueueAction(ActionType.Move, new System.Object[]{ (Vector2)nodePosition });
		GetComponent<ActionQueue>().EnqueueAction(ActionType.Mine);
	}


	private void ZoneComplete()
	{
		CallbackAction();
		FindNext();
	}
	
	private void MineTile()
	{
		if ((tilesToMine.Count != 0))
		{
			buildZoneTM.SetTile(tilesToMine[0], null);
			resourcesTM.SetTile(tilesToMine[0], null);
			GetComponent<AudioSource>().Play();
			IncreaseResources();
			tilesToMine.RemoveAt(0);
		}
	}
	
	
	private void IncreaseResources()
	{
		ResourcesStorage.Instance.MineIncrease();
	}


	public override void StartThisAction(System.Object[] parameters)
    {
	    Mine();
    }
    
    IEnumerator MiningProcess()
    {
	    while (true)
	    {
		    if (tilesToMine.Count == 0)
			    ZoneComplete();
		    yield return new WaitForSeconds(miningTime);
		    MineTile();
	    }
    }
    
}
