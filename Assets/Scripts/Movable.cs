using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movable : MonoBehaviour
{
    [SerializeField] public UnityEvent callOnMove;
    SpriteRenderer sprite;

    public enum Direction
    {
        Right,
        Up,
        Left,
        Down
    }

    [SerializeField] private Direction movementDirection;
    [SerializeField] private Direction lastDirection;

    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        lastDirection = movementDirection;
        UpdateRotation();
    }

    private void FixedUpdate()
    {
        UpdateRotation();
    }

    public void Move()
    {
        Vector3 newPosition = GetNextPosition(gameObject.transform.position, movementDirection);
        gameObject.transform.position = newPosition;

        lastDirection = movementDirection;
        UpdateRotation();

        //callOnMove?.Invoke();
    }

    public void ChangeDirection(Direction newDirection)
    {
        movementDirection = newDirection;
        callOnMove?.Invoke();
        UpdateRotation();
    }

    public Direction GetDirection()
    {
        return movementDirection;
    }

    public Direction GetLastDirection()
    {
        return lastDirection;
    }

    public void UpdateRotation()
    {
        int directionSums = (int)lastDirection - (int)movementDirection;
        if(sprite != null)
            sprite.flipY = directionSums == 1 || directionSums == -3;

        gameObject.transform.eulerAngles = new Vector3(0, 0, 90 * ((int)lastDirection));
    }

    public static Vector3 GetEulerAnglesFromDirection(Direction currentDirection)
    {
        return new Vector3(0, 0, 90 * ((int)currentDirection));
    }

    //public static Vector3 GetEulerAnglesFromDirection(Direction currentDirection, Direction lastDirection)
    //{
    //    int directionSums = (int)lastDirection - (int)currentDirection;
    //    Debug.Log(directionSums);

    //    if (directionSums == 1 || directionSums == -3)
    //        return new Vector3(0, 0, 90 * ((int)currentDirection) + 90);

    //    return new Vector3(0, 0, 90 * ((int)currentDirection));
    //}

    public bool IsTurning()
    {
        return movementDirection != lastDirection;
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
