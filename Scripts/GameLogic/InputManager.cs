using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ActionType
{
    Build,
    Move,
    Mine,
    Afk,
}
public class InputManager : Singleton<InputManager>
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ClickHandler(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }


    void ClickHandler(Vector2 clickPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
        if (ActiveObject.Instance.Get() != null)
        {
            EventHandler currHeroEventHandler = ActiveObject.Instance.Get().GetComponent<EventHandler>();
            if (hit.collider != null)
            {
                switch (hit.transform.gameObject.tag)
                {
                    case "Building":
                        currHeroEventHandler.StartConstructingBuilding(hit.transform.gameObject);
                        currHeroEventHandler.DeactivateCurrent();
                        ActiveObject.Instance.Clear();
                        break;
                    case "TilemapResources":
                        currHeroEventHandler.StartMiningResources(clickPosition);
                        currHeroEventHandler.DeactivateCurrent();
                        ActiveObject.Instance.Clear();
                        break;
                    case "TilemapGround":
                        break;
                    case "Heroes":
                        if (ActiveObject.Instance.Get().Equals(hit.transform.gameObject))
                            currHeroEventHandler.VoiceOnClick();
                        else
                        {
                            currHeroEventHandler.DeactivateCurrent();
                            hit.transform.gameObject.GetComponent<EventHandler>().ActivateThisObject();
                        }

                        break;
                    case null:
                        currHeroEventHandler.MoveToFreeSpace(clickPosition);
                        break;
                    default:
                        currHeroEventHandler.MoveToFreeSpace(clickPosition);
                        break;
                }
            }
            else
            {
                currHeroEventHandler.MoveToFreeSpace(clickPosition);
            }
        }
        else
        {
            if (hit.collider != null) 
                if (hit.transform.gameObject.tag.Equals("Heroes"))
                    hit.transform.gameObject.GetComponent<EventHandler>().ActivateThisObject();
        }
    }
}
