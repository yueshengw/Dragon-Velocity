using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsTextMovement : MonoBehaviour
{
    public float beginMovementTimer;
    public Vector3 moveUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        beginMovementTimer += Time.deltaTime;

        if (beginMovementTimer >= 3 && beginMovementTimer <= 28)
        {
            GetComponent<Transform>().position += moveUp;
        }
        if (beginMovementTimer >= 31)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
