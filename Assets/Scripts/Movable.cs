using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    public enum Direction
    {
        Right,
        Up,
        Left,
        Down
    }

    [SerializeField] private Direction movementDirection;
    [SerializeField] private Direction lastDirection;
    //[SerializeField] private float secondsBetweenMoves = 1f;

    // Start is called before the first frame update
    void Start()
    {
        movementDirection = Direction.Right;
        lastDirection = Direction.Right;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Move()
    {
        lastDirection = movementDirection;
        Vector3 newPosition = GetNextPosition(gameObject.transform.position, movementDirection);
        gameObject.transform.position = newPosition;

    }

    public void ChangeDirection(Direction newDirection)
    {
        gameObject.transform.eulerAngles = GetEulerAngles(movementDirection);
        movementDirection = newDirection;
    }

    public Direction GetDirection()
    {
        return movementDirection;
    }
    public Direction GetLastDirection()
    {
        return lastDirection;
    }

    public static Vector3 GetEulerAngles(Direction direction)
    {
        return new Vector3(0, 0, 90 * ((int)direction));
    }

    public static Vector3 GetNextPosition(Vector3 currentPosition, Direction currentDirection)
    {
        Vector3 newPosition = currentPosition;
        if (currentDirection == Direction.Right)
            newPosition.x += 1;
        else if (currentDirection == Direction.Left)
            newPosition.x -= 1;
        else if (currentDirection == Direction.Up)
            newPosition.y += 1;
        else if (currentDirection == Direction.Down)
            newPosition.y -= 1;
        return newPosition;
    }

    public static Direction GetOppositeDirection(Direction direction)
    {
        return (Direction)(((int)direction + 2) % 4);
    }
}
