using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeSpawner : MonoBehaviour, ITypeSpawner
{
    [SerializeField] private GameObject areaToSpawn = null;
    [SerializeField] private GameObject objectToSpawn = null;
    [SerializeField] private float secondsBetweenSpawn = 1f;


    public IEnumerator SpawnObject()
    {
        if (objectToSpawn != null && areaToSpawn != null)
        {
            var data = PositionRandomizer.GeneratePositionOfClearLine(areaToSpawn.transform);
            GameObject newObject = Instantiate(objectToSpawn, data.spawnPosition, Quaternion.identity);
            newObject.GetComponent<Movable>()?.ChangeDirection(data.direction);
            Debug.Log(data.direction);
            Debug.Log(data.spawnPosition);
        }

        yield return new WaitForSeconds(secondsBetweenSpawn);
        StartCoroutine(SpawnObject());
    }
}
