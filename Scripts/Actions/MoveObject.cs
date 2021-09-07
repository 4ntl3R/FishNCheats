using System.Collections;
using System.Collections.Generic;
using Actions;
using UnityEngine;

public class MoveObject : BasicActionClass
{
    Vector2 positionObject, destination;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    
    public float minDist = 0.1f;
    public float speed = 5.0f;
    
    public delegate void FlipMessage(bool flipped);
    public event FlipMessage onFlipped;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move()
    {
        Move(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    
    public void Move(Vector2 newDestination)
    {
        destination = newDestination;
        positionObject = transform.position;
        StartCoroutine(MovingProcess());
        rb.velocity = new Vector2(destination.x - positionObject.x, destination.y - positionObject.y).normalized * speed; 
        LookAt2D(destination);
    }
    

    public void LookAt2D(Vector2 target)
    {
        var dir = target - (Vector2)transform.position;

        if (dir.x < 0 && !spriteRenderer.flipY)
        {
            spriteRenderer.flipY = true;
            if (onFlipped != null)
                onFlipped(true);
        }

        if (dir.x > 0 && spriteRenderer.flipY)
        {
            spriteRenderer.flipY = false;
            if (onFlipped != null)
                onFlipped(false);
        }

        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void StartThisAction(System.Object[] parameters)
    {
        if (parameters.Length == 0)
            Move();
        else
        {
            if (parameters[0] is Vector2)
                Move((Vector2) parameters[0]);
        }
    }

    public override void StopThisAction()
    {
        StopMoving();
        base.StopThisAction();
    }


    IEnumerator MovingProcess()
    {
        while (true)
        {
            // transform.position = Vector2.MoveTowards(positionObject, moveHere.transform.position, Time.deltaTime * velocity);
            if ((Vector2.Distance(transform.position, destination) < minDist))
            {
                StopMoving();
                CallbackAction();
                break;
            }
            else
            {
                yield return null;
            }
        }
    }

    private void StopMoving()
    {
        rb.velocity = Vector2.zero;
        destination = Vector2.zero;
        if(spriteRenderer.flipY)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        }
    }
}

