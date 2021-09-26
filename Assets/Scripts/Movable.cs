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

    private Direction movementDirection;
    [SerializeField] private float secondsBetweenMoves = 1f;

    // Start is called before the first frame update
    void Start()
    {
        movementDirection = Direction.Right;
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Move()
    {
        Vector3 newPosition = gameObject.transform.position;
        if (movementDirection == Direction.Right)
            newPosition.x += 1;
        else if (movementDirection == Direction.Left)
            newPosition.x -= 1;
        else if (movementDirection == Direction.Up)
            newPosition.y += 1;
        else if(movementDirection == Direction.Down)
            newPosition.y -= 1;
        gameObject.transform.position = newPosition;


        yield return new WaitForSeconds(secondsBetweenMoves);
        StartCoroutine(Move());
    }

    public void ChangeDirection(Direction newDirection)
    {
        gameObject.transform.eulerAngles = new Vector3(0, 0, 90 * ((int)movementDirection));
        movementDirection = newDirection;
    }

    private void OnBecameInvisible()
    {
        AppearOnTheOtherSide();

    }

    private void AppearOnTheOtherSide()
    {
        Vector3 objectPosition = gameObject.transform.position;
        Vector3 objectPositionToScreen = Camera.main.WorldToScreenPoint(objectPosition);
        if(objectPositionToScreen.x < 0)
        {
            objectPosition.x += 10;
            gameObject.transform.position = objectPosition;
        }
    }
}
