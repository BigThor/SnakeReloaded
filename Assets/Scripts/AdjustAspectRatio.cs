using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustAspectRatio : MonoBehaviour
{
    private Camera cameraToAdjust;
    [SerializeField] GameObject playground;

    // Start is called before the first frame update
    void Start()
    {
        cameraToAdjust = gameObject.GetComponent<Camera>();
        RescaleCamera();
    }

    private void RescaleCamera()
    {
        if (cameraToAdjust == null || playground == null)
            return;

        // Make camera height adjust to playground height
        cameraToAdjust.orthographicSize = playground.transform.localScale.y / 2;


        float targetAspect = 4f / 3f;
        float windowAspect = (float)Screen.width / (float)Screen.height;

        float scaleheight = windowAspect / targetAspect;
        float scalewidth = 1.0f / scaleheight;

        Rect newCameraRect = cameraToAdjust.rect;

        newCameraRect.width = scalewidth;
        newCameraRect.height = 1.0f;

        // Center the camera view
        newCameraRect.x = (1.0f - scalewidth) / 2.0f;

        cameraToAdjust.rect = newCameraRect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
