using System.Collections;
using System.Collections.Generic;
using Actions;
using UnityEngine;

[System.Serializable]
public class ActionQueueElement
{
    public ActionType ActionName;
    public System.Object[] Parameters;

    public ActionQueueElement(ActionType actionName)
    {
        ActionName = actionName;
    }
    
    public ActionQueueElement(ActionType actionName, System.Object[] parameters)
    {
        ActionName = actionName;
        Parameters = parameters;
    }
}

public class ActionQueue : MonoBehaviour
{
    Queue<ActionQueueElement> actionQueue = new Queue<ActionQueueElement>();
    private Dictionary<ActionType, BasicActionClass> actionScripts = new Dictionary<ActionType, BasicActionClass>();
    public delegate void ActionMessage();
    public event ActionMessage OnActionChange;
    
    void Awake()
    {
        actionScripts.Add(ActionType.Move, GetComponent<MoveObject>());
        actionScripts.Add(ActionType.Build, GetComponent<Builder>());
        actionScripts.Add(ActionType.Mine, GetComponent<Mining>());
    }


    private void StartNextAction()
    {
        if (OnActionChange != null)
            OnActionChange();
        if (actionQueue.Count > 0)
            actionScripts[actionQueue.Peek().ActionName].StartThisAction(actionQueue.Peek().Parameters); 

    }

    public void EnqueueAction(ActionType action)
    {
        actionQueue.Enqueue(new ActionQueueElement(action, new System.Object[] {}));
        if (actionQueue.Count == 1)
            StartNextAction();
    }
    
    public void EnqueueAction(ActionType action, System.Object[] parameters)
    {
        actionQueue.Enqueue(new ActionQueueElement(action, parameters));
        if (actionQueue.Count == 1)
            StartNextAction();
    }

    public void DequeueAction()
    {
        actionScripts[actionQueue.Peek().ActionName].StopThisAction();
        actionQueue.Dequeue();
        StartNextAction();
    }

    public void ClearActions()
    {
        if (actionQueue.Count > 0)
        {
            foreach (ActionQueueElement actionInQueue in actionQueue)
                actionScripts[actionQueue.Peek().ActionName].StopThisAction();
            actionQueue.Clear();
        }
    }

    public ActionType PeekAction()
    {
        if (actionQueue.Count != 0)
        {
            return actionQueue.Peek().ActionName;
        }
        else
        {
            return ActionType.Afk;
        }
    }
}
