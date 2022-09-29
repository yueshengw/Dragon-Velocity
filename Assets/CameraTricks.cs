using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTricks : MonoBehaviour
{
    public GameObject Camera;
    public float zoomDecrementValue;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        while (Camera.GetComponent<Camera>().fieldOfView < 170.4f)
        {
            Camera.GetComponent<Camera>().fieldOfView += Time.deltaTime/50f;
            
        }

    }

    public void ZoomIn()
    {
        while (Camera.GetComponent<Camera>().fieldOfView < 170.4f)
        {
            Camera.GetComponent<Camera>().fieldOfView += Time.deltaTime;
        }
        
    }
}
