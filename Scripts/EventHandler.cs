using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = System.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class EventHandler : MonoBehaviour
{
    private ActivateObject activateObject;
    private ActionQueue actionQueue;

    private void Awake()
    {
        activateObject = GetComponent<ActivateObject>();
        actionQueue = GetComponent<ActionQueue>();
    }

    public void ActivateThisObject()
    {
        activateObject.Activate();
    }

    public void VoiceOnClick()
    {
        gameObject.SendMessage("PlaySpeech");
    }

    public void DeactivateCurrent()
    {
        activateObject.Deactivate();
    }
    
    
    public void MoveToFreeSpace(Vector2 pos)
    {
        actionQueue.ClearActions();
        actionQueue.EnqueueAction(ActionType.Move, new System.Object[] { pos });
    }

    public void StartConstructingBuilding(GameObject clickedBuilding)
    {
        actionQueue.ClearActions();
        Vector3 buildingPos = clickedBuilding.transform.position;
        Vector2 buildingSize = new Vector2(clickedBuilding.GetComponent<BuildingFinishingComponent>().xSize,
            clickedBuilding.GetComponent<BuildingFinishingComponent>().ySize);
        Vector2 moveToPos = new Vector2(buildingPos.x + UnityEngine.Random.Range(0, buildingSize.x), buildingPos.y+UnityEngine.Random.Range(0, buildingSize.y));
        actionQueue.EnqueueAction(ActionType.Move, new System.Object[] { moveToPos });
        actionQueue.EnqueueAction(ActionType.Build, new System.Object[]{ clickedBuilding });
    }

    public void StartMiningResources(Vector2 pos)
    {
        actionQueue.ClearActions();
        actionQueue.EnqueueAction(ActionType.Move, new System.Object[] { pos });
        actionQueue.EnqueueAction(ActionType.Mine);
    }
	
}
