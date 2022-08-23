using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NeutralEndingSceneManager : MonoBehaviour
{
    public GameObject textStuff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (textStuff.GetComponent<CutsceneTextScript>().textCounter >= 10)
        {
            SceneManager.LoadScene("End Credits");
        }
    }
}
