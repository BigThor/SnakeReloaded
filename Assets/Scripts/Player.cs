using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Movable movable;
    BodyDirector bodyDirector;

    [SerializeField] private ScoreCounter scoreCounter;

    AudioSource eatSound;

    // Start is called before the first frame update
    void Start()
    {
        movable = gameObject.GetComponent<Movable>();
        eatSound = gameObject.GetComponent<AudioSource>();
        bodyDirector = gameObject.GetComponent<BodyDirector>();
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
        bodyDirector?.AddBodypart();
        scoreCounter?.AddToScore(1);
    }

    public void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}
