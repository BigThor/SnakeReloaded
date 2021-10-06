using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour, ITypeSpawner
{
    [SerializeField] private GameObject areaToSpawn = null;
    [SerializeField] private GameObject objectToSpawn = null;
    [SerializeField] private float secondsBetweenSpawn = 1f;


    public IEnumerator SpawnObject()
    {
        if (objectToSpawn != null && areaToSpawn != null)
        {
            var validPosition = PositionRandomizer.GenerateValidPosition(areaToSpawn.transform);
            Instantiate(objectToSpawn, validPosition, Quaternion.identity);
        }

        yield return new WaitForSeconds(secondsBetweenSpawn);
        StartCoroutine(SpawnObject());
    }
}
