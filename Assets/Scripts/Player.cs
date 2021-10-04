using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Movable movable;
    [SerializeField] private GameObject bodyPrefab;

    List<GameObject> bodyparts;
    [SerializeField] private int initialSize = 3;

    [SerializeField] private float secondsBetweenMoves = 1f;

    [SerializeField] private ScoreCounter scoreCounter;

    AudioSource eatSound;

    // Start is called before the first frame update
    void Start()
    {
        movable = gameObject.GetComponent<Movable>();
        eatSound = gameObject.GetComponent<AudioSource>();

        bodyparts = new List<GameObject>();
        for(int i = 0; i < initialSize; i++)
        {
            AddBodypart();
        }

        StartCoroutine(MoveBody());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirectionOnInput();
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

    private IEnumerator MoveBody()
    {
        yield return new WaitForSeconds(secondsBetweenMoves);

        UpdateBodyDirection();
        for (int bodypartIndex = bodyparts.Count - 1; bodypartIndex >= 0; bodypartIndex--)
        {
            bodyparts[bodypartIndex].GetComponent<Movable>().Move();
        }
        movable.Move();

        StartCoroutine(MoveBody());
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

        GameObject newBodyPart = Instantiate(bodyPrefab, newPosition, Quaternion.Euler(Movable.GetEulerAngles(oppositeDirection)));
        bodyparts.Add(newBodyPart);
        newBodyPart.GetComponent<Movable>().ChangeDirection(lastDirection);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Eatable"))
        {
            Eat();
        }
    }

    private void Eat()
    {
        eatSound?.Play();
        AddBodypart();
        if (scoreCounter != null)
            scoreCounter.AddToScore(1);
    }

    private void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}
