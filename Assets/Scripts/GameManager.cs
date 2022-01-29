using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public Vector3 respawnCod1;

    public bool respawn;

    public GameObject player;

    public GameObject checkpoint1;
    public GameObject checkpoint2;

    public GameObject Gate;
    private bool createdGate;

    public GameObject[] fallableBreakableBlocks;

    public float count1;
    void Start()
    {
        //player.transform.position = new Vector3(respawnCod1.x,respawnCod1.y,respawnCod1.z);
        checkpoint1 = GameObject.Find("Checkpoint/Checkpoint_1");
        checkpoint2 = GameObject.Find("Checkpoint/Checkpoint_2");
        player.transform.position = checkpoint1.transform.position;
    }

    void Update()
    {
        if (player.GetComponent<PlayerMovement1>().dead == true) {
            if (player.GetComponent<PlayerMovement1>().newCheckpoint == false)
            {
                //player.transform.position = new Vector3(respawnCod1.x, respawnCod1.y, respawnCod1.z);
                player.transform.position = checkpoint1.transform.position;

            }
            else
            {
                player.transform.position = checkpoint2.transform.position;
            }
            player.GetComponent<PlayerMovement1>().dead = false;
        }
    }

    public void Call1()
    {
        Invoke("CallGate1", 0);
        Invoke("CallGate2", 2f);
    }

    public void CallGate1()
    {
        foreach (GameObject n in fallableBreakableBlocks)
        {
            if (n != null)
            {
                n.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                n.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    public void CallGate2()
    {
        foreach (GameObject n in fallableBreakableBlocks)
        {
            if (n != null)
            {
                n.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                n.tag = "Breakable";
            }
        }
    }
}
