using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public float active = 0.5f;
    public float deactive = 255;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Activate()
    {
       ActiveObject.Instance.Set(gameObject);
       
       spriteRenderer.color = new Color(active, active, active, 1f);
    }

    public void Deactivate()
    {
        if (ActiveObject.Instance.Get() == gameObject)
        {
            ActiveObject.Instance.Clear();

            spriteRenderer.color = new Color(deactive, deactive, deactive, 1f);
        }
    }
}
