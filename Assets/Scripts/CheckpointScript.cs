using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public bool isCheckpoint;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isCheckpoint)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            isCheckpoint = true;
            Player.GetComponent<PlayerMovement1>().newCheckpoint = true;
        }
    }
}
