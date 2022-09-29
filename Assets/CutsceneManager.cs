using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
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
        if(GameObject.Find("Good Ending Scene Object"))
        {
            if (cutsceneTextObject.GetComponent<CutsceneTextScript>().textCounter == 9)
            {
                SceneManager.LoadScene("End Credits");
            }
        }

        if (GameObject.Find("Introduction Scene Object"))
        {
            if (cutsceneTextObject.GetComponent<CutsceneTextScript>().textCounter == 10)
            {
                SceneManager.LoadScene("The Outskirts");
            }
        }
    }
}
