using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    ITypeSpawner typeSpawner = null;

    // Start is called before the first frame update
    void Start()
    {
        typeSpawner = GetComponent<ITypeSpawner>();
        if(typeSpawner != null)
            StartCoroutine(typeSpawner.SpawnObject());
    }

}
