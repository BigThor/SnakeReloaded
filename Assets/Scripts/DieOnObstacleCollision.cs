using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnObstacleCollision : MonoBehaviour
{
    [SerializeField] Player player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Obstacle"))
            player.Die();
    }
}
