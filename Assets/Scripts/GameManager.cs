using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public Vector2 lastCheckpointPosition;

    public bool respawn;

    public GameObject player;
    public GameObject playerPrefab;

    public GameObject Gate;
    private bool createdGate;

    public GameObject[] fallableBreakableBlocks;

    public float count1;
    public float deathTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player.GetComponent<PlayerMovementScript>().isDead == true)
        {
            //player.GetComponent<PlayerMovement1>().respawn = true;
            player.GetComponent<PlayerMovementScript>().respawn = true;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (player.GetComponent<PlayerMovementScript>().deathTime <= 0f)
        {
            Instantiate (playerPrefab, lastCheckpointPosition, Quaternion.identity);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            Destroy(GameObject.FindGameObjectWithTag("Player"));
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
