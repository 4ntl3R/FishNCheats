using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InstrumentPair
{
    public ActionType actionType;
    public Sprite sprite;
}

public class SwitchInstrument : MonoBehaviour
{
    public List<InstrumentPair> instruments;
    private ActionQueue actionQueue;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        actionQueue = transform.parent.GetComponent<ActionQueue>();
        transform.parent.GetComponent<MoveObject>().onFlipped += ChangeOrientation;
        actionQueue.OnActionChange += ChangeInstrument;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void ChangeInstrument()
    {
        bool isFound = false;
        foreach (InstrumentPair pair in  instruments)
        {
            if (pair.actionType == actionQueue.PeekAction())
            {
                isFound = true;
                spriteRenderer.sprite = pair.sprite;
            }

            if (!isFound)
                spriteRenderer.sprite = null;
        }
    }

    private void ChangeOrientation(bool newOrientation)
    {
        spriteRenderer.flipX = !newOrientation;
    }
    
}
