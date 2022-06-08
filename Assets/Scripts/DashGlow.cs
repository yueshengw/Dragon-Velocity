using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashGlow : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerMovement1>().moveInput == -1)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (player.GetComponent<PlayerMovement1>().moveInput == 1)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        //transform.position = player.transform.position;
    }
}
