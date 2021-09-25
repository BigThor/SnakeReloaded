using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
        float horizontalDirection = Input.GetAxis("Horizontal");
        float verticalDirection = Input.GetAxis("Vertical");

        if(horizontalDirection > 0)
        {
            movable.ChangeDirection(Movable.Direction.Right);
        }
        else if (horizontalDirection < 0)
        {
            movable.ChangeDirection(Movable.Direction.Left);
        }
        else if (verticalDirection > 0)
        {
            movable.ChangeDirection(Movable.Direction.Up);
        }
        else if (verticalDirection < 0)
        {
            movable.ChangeDirection(Movable.Direction.Down);
        }
    }
}
