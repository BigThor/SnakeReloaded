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

    public static (Vector2 spawnPosition, Movable.Direction direction)
        GeneratePositionOfClearLine(Transform areaTransform)
    {
        int halfWidth = (int)(areaTransform.localScale.x / 2);
        int halfHeight = (int)(areaTransform.localScale.y / 2);
        List<Collider2D> listCollidingWith = new List<Collider2D>();
        Vector2 randomPosition = new Vector2();
        Movable.Direction randomDirection;
        Vector2 size = new Vector2(0.98f, 0.98f);

        int whileCounter = 0;
        bool isSomethingInLine;
        float x, y;

        do
        {
            isSomethingInLine = false;

            // Horizontal or vertical
            randomDirection = (Movable.Direction)Random.Range(0, 4);
            if(randomDirection == Movable.Direction.Right || randomDirection == Movable.Direction.Left)
            {
                int sign = ((int)randomDirection - 1);
                x = (halfWidth + 0.5f) * sign;
                y = Random.Range(-halfHeight, halfHeight) + 0.5f;

                for(int xCheck = -halfWidth; xCheck < halfWidth; xCheck++)
                {
                    Vector2 currentPoint = new Vector2(xCheck, y);
                    if (Physics2D.OverlapBox(currentPoint, size, 0, new ContactFilter2D().NoFilter(), listCollidingWith) > 1)
                        isSomethingInLine = true;
                }
            }
            else
            {
                int sign = ((int)randomDirection - 2);
                x = Random.Range(-halfWidth, halfWidth) + 0.5f;
                y = (halfHeight + 0.5f) * sign;

                for (int yCheck = -halfHeight; yCheck < halfHeight; yCheck++)
                {
                    Vector2 currentPoint = new Vector2(x, yCheck);
                    if (Physics2D.OverlapBox(currentPoint, size, 0, new ContactFilter2D().NoFilter(), listCollidingWith) > 1)
                        isSomethingInLine = true;
                }
            }
            randomPosition = new Vector2(x, y);

            whileCounter++;
        } while (isSomethingInLine && whileCounter < 10);

        return (randomPosition, randomDirection);
    }
}
