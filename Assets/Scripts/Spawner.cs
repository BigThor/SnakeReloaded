using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject areaToSpawn = null;
    [SerializeField] private GameObject objectToSpawn = null;
    [SerializeField] private float secondsBetweenSpawn = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnObject()
    {
        if (objectToSpawn != null && areaToSpawn != null)
        {
            Vector2 validPosition = PositionRandomizer.GenerateValidPosition(areaToSpawn.transform);
            Instantiate(objectToSpawn, validPosition, Quaternion.identity);
        }

        yield return new WaitForSeconds(secondsBetweenSpawn);
        StartCoroutine(SpawnObject());
    }

}
