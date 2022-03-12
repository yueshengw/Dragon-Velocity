using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public Vector3 respawnCod1;

    public bool respawn;
    public bool playerIsDead;

    public GameObject player;

    public GameObject checkpoint1;
    public GameObject checkpoint2;

    public GameObject Gate;
    private bool createdGate;

    public GameObject[] fallableBreakableBlocks;

    public GameObject[] checkpointsGroup;

    public float count1;

    private static GameManager instance;
    public Vector2 lastCheckpointPosition;

    public float timer;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
        player = GameObject.Find("Player");
    }
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        player = GameObject.Find("Player");
        if (playerIsDead == true) {

            Destroy(GameObject.Find("Player"));
            timer += Time.deltaTime;

            if(timer >= 1.7f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                timer = 0f;
                playerIsDead = false;
            }
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
