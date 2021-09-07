using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildComponent : MonoBehaviour
{
	public GameObject currentBuilding;
	private int cBuildingX;
	private int cBuildingY;
	
	public GameObject buildZones;
	private Tilemap buildZoneTM;
	
	public bool buildModeState;
	public Tilemap newBuildTM;
	private GameObject choosePlace;
	public Tile greenTile;
	private int cnt;
	
	private Vector3Int prevMousePosition;

	
	// Start is called before the first frame update
    void Awake()
    {
        buildModeState = false;
		buildZoneTM = buildZones.GetComponent<Tilemap>();
		SetBuilding();
		prevMousePosition = new Vector3Int(0, 0, 0);
		buildZones.GetComponent<TilemapRenderer>().enabled = false;
		cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
		BuildGear();
	}

	public void BuildGear()
    {
		if (Input.GetKeyDown("mouse 1"))
			ToggleBuildMode();
		GreenBuildZone();
		if (Input.GetKeyDown("mouse 0") && buildModeState)
			BuildNew();
	}
	
	private void GreenBuildZone()
	{
		if (buildModeState)
		{
			Vector3Int newPos = newBuildTM.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			if (prevMousePosition != newPos)
				{
					newBuildTM.ClearAllTiles();
					for (int x=0; x<cBuildingX; x++)
						for (int y=0; y<cBuildingX; y++)
						{
							newBuildTM.SetTile(new Vector3Int (newPos.x+x, newPos.y+y, newPos.z), greenTile);
						}
					prevMousePosition = new Vector3Int(newPos.x, newPos.y, newPos.z);
				}
		}
	}
	
	private void SetBuilding()
	{
		cBuildingX = currentBuilding.GetComponent<BuildingFinishingComponent>().xSize;
		cBuildingY = currentBuilding.GetComponent<BuildingFinishingComponent>().ySize;
	}
	
	private void BuildNew()
	{
		bool isPosible = true;
		for (int x=0; x<cBuildingX; x++)
			for (int y=0; y<cBuildingY; y++)
				if (buildZoneTM.GetTile(new Vector3Int(prevMousePosition.x+x, prevMousePosition.y+y, prevMousePosition.z)) != null)
					isPosible = false;
		for (int x=0; x<cBuildingX; x++)
			if (GameObject.FindWithTag("TilemapGround").GetComponent<Tilemap>().GetTile(new Vector3Int(prevMousePosition.x+x, prevMousePosition.y-1, prevMousePosition.z)) == null)
					isPosible = false;
		if (isPosible)
		{
			StartBuilding();
			ToggleBuildMode();
		}
	}
	
	private void StartBuilding()
	{
		GameObject newBuild = Instantiate(currentBuilding, buildZoneTM.CellToWorld(prevMousePosition), Quaternion.identity);
		cnt++;
		newBuild.name = "building" + cnt.ToString(); 
		for (int x=0; x<cBuildingX; x++)
			for (int y=0; y<cBuildingY; y++)
				buildZoneTM.SetTile(new Vector3Int(prevMousePosition.x+x, prevMousePosition.y+y, prevMousePosition.z), buildZoneTM.GetTile(new Vector3Int(prevMousePosition.x, prevMousePosition.y-1, prevMousePosition.z)));		
	}
	
	public void SetBuilding(GameObject newGO)
	{
		currentBuilding = newGO;
		cBuildingX = currentBuilding.GetComponent<BuildingFinishingComponent>().xSize;
		cBuildingY = currentBuilding.GetComponent<BuildingFinishingComponent>().ySize;
	}
	
	public void ToggleBuildMode()
	{
		if (buildModeState)
		{
			BuildModeOff();
			newBuildTM.ClearAllTiles();
		}
		else
			BuildModeOn();
	}
	
	private void BuildModeOn()
	{
		buildModeState = true;
		buildZones.GetComponent<TilemapRenderer>().enabled = true;
	}
	
	private void BuildModeOff()
	{
		buildModeState = false;
		buildZones.GetComponent<TilemapRenderer>().enabled = false;
	}
}
