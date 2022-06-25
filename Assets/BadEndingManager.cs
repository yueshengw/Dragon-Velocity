using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadEndingManager : MonoBehaviour
{
    public GameObject cutsceneTextObject;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneTextObject = GameObject.Find("CutsceneTextObject");
    }

    // Update is called once per frame
    void Update()
    {
        if (cutsceneTextObject.GetComponent<CutsceneTextScript>().textCounter >= 11)
        {
            SceneManager.LoadScene("End Credits");
        }
    }
}
