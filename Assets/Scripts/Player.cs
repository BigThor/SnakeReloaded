using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Movable movable;
    [SerializeField] List<GameObject> bodyparts;

    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private int initialSize = 3;

    // Start is called before the first frame update
    void Start()
    {
        movable = gameObject.GetComponent<Movable>();
        movable.delegateAfter = UpdateBodyDirection;
        bodyparts = new List<GameObject>();
        for(int i = 0; i < initialSize; i++)
        {
            AddBodypart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirectionOnInput();
    }

    private void UpdateBodyDirection()
    {
        for (int bodypartIndex = bodyparts.Count - 1; bodypartIndex > 0; bodypartIndex--)
        {
            Movable.Direction nextPartDirection = bodyparts[bodypartIndex - 1].GetComponent<Movable>().GetDirection();
            bodyparts[bodypartIndex].GetComponent<Movable>().ChangeDirection(nextPartDirection);
        }

        if (bodyparts.Count != 0)
            bodyparts[0].GetComponent<Movable>().ChangeDirection(movable.GetLastDirection());
    }

    private void UpdateDirectionOnInput()
    {
        float horizontalDirection = Input.GetAxis("Horizontal");
        float verticalDirection = Input.GetAxis("Vertical");

        if (horizontalDirection > 0)
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

    private void AddBodypart()
    {
        GameObject lastObject;
        if (bodyparts.Count == 0)
        {
            lastObject = gameObject;
        }
        else
        {
            lastObject = bodyparts[bodyparts.Count - 1];
        }
        Vector3 lastPartPosition = lastObject.transform.position;
        Movable.Direction lastDirection = lastObject.transform.GetComponent<Movable>().GetDirection();
        lastDirection = Movable.GetOppositeDirection(lastDirection);
        Vector3 newPosition = Movable.GetNextPosition(lastPartPosition, lastDirection);

        bodyparts.Add(Instantiate(bodyPrefab, newPosition, Quaternion.Euler( Movable.GetEulerAngles(lastDirection))));
    }
}
