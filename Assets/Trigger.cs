using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public string task;
    public GameObject taskObject;

    public bool isTriggered;
    public string text;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isTriggered == true)
        {
            if (task == "FallingStone")
            {
                taskObject.GetComponent<FallingCrusher>().isTriggered = true;
            }
        }
        else
        {
            if (task == "FallingStone")
            {
                taskObject.GetComponent<FallingCrusher>().isTriggered = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            text = collision.tag;
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isTriggered = false;
        }
    }
}
