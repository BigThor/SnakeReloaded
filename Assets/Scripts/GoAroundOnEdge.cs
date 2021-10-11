using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAroundOnEdge : MonoBehaviour
{
    Movable movable;

    // Start is called before the first frame update
    void Start()
    {
        movable = gameObject.GetComponent<Movable>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag.Equals("Playground"))
        {
            AppearOnOppositeSide(collision.transform.localScale);
        }
    }

    private void AppearOnOppositeSide(Vector3 size)
    {
        Vector2 newPosition = gameObject.transform.position;

        Movable.Direction currentDirection = movable.GetLastDirection();
        if(currentDirection == Movable.Direction.Right)
        {
            newPosition.x -= size.x;
        }
        else if (currentDirection == Movable.Direction.Up)
        {
            newPosition.y -= size.y;
        }
        else if(currentDirection == Movable.Direction.Left)
        {
            newPosition.x += size.x;
        }
        else if (currentDirection == Movable.Direction.Down)
        {
            newPosition.y += size.y;
        }
        gameObject.transform.position = newPosition;
    }
}
