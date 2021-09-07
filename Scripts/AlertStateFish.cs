using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Tilemaps;
[System.Serializable]
public class AlertStateListEntry
{
	public ActionType actionType;
	public float actionValue;
}
public class AlertStateFish : AlertState
{
	private ActionQueue actionQueue;

	public List<AlertStateListEntry> AlertStateList = new List<AlertStateListEntry>();
	private Dictionary<ActionType, float> AlertStateDict = new Dictionary<ActionType, float>();
	
	 

	protected override void Awake()
	{
		actionQueue = GetComponent<ActionQueue>();
		base.Awake();
		foreach (AlertStateListEntry curr in AlertStateList)
			AlertStateDict.Add(curr.actionType, curr.actionValue);
	}
	

	public override void AlertOn()
	{
		ChangeState();
		if (!isHidden)
		{
			ActionType currState = actionQueue.PeekAction();
			ScalesLogic.Instance.ChangeAlert(AlertStateDict[currState]+suspiciousness);
		}

	}
}
