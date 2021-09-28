using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsUpdater : MonoBehaviour
{
    Text textObject;

    // Start is called before the first frame update
    void Start()
    {
        textObject = GetComponent<Text>();
        textObject.text = "You got " + ScoreCounter.Score.ToString() + " point(s)!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
