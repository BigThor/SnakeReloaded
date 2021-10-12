using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDirector : MonoBehaviour
{
    Movable movable;
    List<GameObject> bodyparts;
    [SerializeField] private int initialSize = 3;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private float secondsBetweenMoves = 1f;

    // Start is called before the first frame update
    void Start()
    {
        movable = gameObject.GetComponent<Movable>();

        bodyparts = new List<GameObject>();
        for (int i = 0; i < initialSize; i++)
        {
            AddBodypart();
        }

        StartCoroutine(MoveBody());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MoveBody()
    {
        UpdateBodyDirection();
        yield return new WaitForSeconds(secondsBetweenMoves);

        for (int bodypartIndex = bodyparts.Count - 1; bodypartIndex >= 0; bodypartIndex--)
        {
            bodyparts[bodypartIndex].GetComponent<Movable>().Move();
        }
        movable.Move();

        StartCoroutine(MoveBody());
    }


    public void AddBodypart()
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
        Movable.Direction oppositeDirection = Movable.GetOppositeDirection(lastDirection);
        Vector3 newPosition = Movable.GetNextPosition(lastPartPosition, oppositeDirection);

        GameObject newBodyPart = Instantiate(bodyPrefab, newPosition, Quaternion.Euler(Movable.GetEulerAnglesFromDirection(oppositeDirection)), gameObject.transform.parent);
        bodyparts.Add(newBodyPart);
        newBodyPart.GetComponent<Movable>().ChangeDirection(lastDirection);
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

    void OnDestroy()
    {
        foreach(var bodypart in bodyparts)
        {
            Destroy(bodypart);
        }
    }
}
