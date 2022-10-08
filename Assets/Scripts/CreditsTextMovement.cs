using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsTextMovement : MonoBehaviour
{
    public float beginMovementTimer;
    public Vector3 moveUp;
    public float max;
    public float debug;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        beginMovementTimer += Time.deltaTime;
        debug = transform.position.y;
        if (beginMovementTimer >= 0.7 && transform.position.y < max)
        {
            GetComponent<Transform>().position += moveUp;
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Escape)){
            SceneManager.LoadScene("MainMenu");
        }
    }
}
