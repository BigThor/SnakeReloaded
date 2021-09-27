using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector2 GenerateValidPosition(Transform areaTransform)
    {
        int halfWidth = (int)(areaTransform.localScale.x / 2);
        int halfHeight = (int)(areaTransform.localScale.y / 2);
        List<Collider2D> listCollidingWith = new List<Collider2D>();
        Vector2 randomPosition = new Vector2();
        Vector2 size = new Vector2(0.98f, 0.98f);
        do
        {
            float randomX = Random.Range(-halfWidth, halfWidth) + 0.5f;
            float randomY = Random.Range(-halfHeight, halfHeight) + 0.5f;
            randomPosition = new Vector2(randomX, randomY);
        } while (Physics2D.OverlapBox(randomPosition, size, 0, new ContactFilter2D().NoFilter(), listCollidingWith) > 1);

        return randomPosition;
    }
}
