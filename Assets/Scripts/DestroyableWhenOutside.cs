using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWhenOutside : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.CompareTo("KillZone") == 0)
        {
            Destroy(gameObject);
        }
    }
}
