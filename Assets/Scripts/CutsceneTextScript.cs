using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CutsceneTextScript : MonoBehaviour
{
    public TextMeshProUGUI SpeechText;
    public int textCounter;
    public int maxTextCount;
    //public bool isInteracting;

    public string README_README_README;

    public bool placeholder = true;

    public string dialogue0;
    public string dialogue1;
    public string dialogue2;
    public string dialogue3;
    public string dialogue4;
    public string dialogue5;
    public string dialogue6;
    public string dialogue7;
    public string dialogue8;
    public string dialogue9;
    public string dialogue10;
    public string dialogue11;
    public string dialogue12;
    public string dialogue13;
    public string dialogue14;

    public float delay = 0.01f;
    public string fulltext;
    public string currentText = "";
    public bool ShowTextRunning;

    // Start is called before the first frame update
    void Start()
    {
        fulltext = dialogue0;
        StartCoroutine(ShowText());
        //SpeechText.text = "";
        //textCounter = 0;
    }

    IEnumerator ShowText()
    {
         for(int i = 0; i < fulltext.Length + 1; i++)
        {
            ShowTextRunning = true;
            currentText = fulltext.Substring(0,i);
            SpeechText.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
            ShowTextRunning = false;
            //youtube.com/watch?v=1qbjmb_1hV4&t=159s
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ShowTextRunning == false)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                textCounter += 1;
                if (textCounter == 0)
                {
                     
                    fulltext = dialogue1;
                }
                if (textCounter == 1)
                {
                     
                    fulltext = dialogue2;
                }
                if (textCounter == 2)
                {
                     
                    fulltext = dialogue3;
                }
                if (textCounter == 3)
                {
                     
                    fulltext = dialogue4;
                }
                if (textCounter == 4)
                {
                     
                    fulltext = dialogue5;
                }
                if (textCounter == 5)
                {
                     
                    fulltext = dialogue6;
                }
                if (textCounter == 6)
                {
                     
                    fulltext = dialogue7;
                }
                if (textCounter == 7)
                {
                     
                    fulltext = dialogue8;
                }
                if (textCounter == 8)
                {
                     
                    fulltext = dialogue9;
                }
                if (textCounter == 9)
                {
                     
                    fulltext = dialogue10;
                }
                if (textCounter == 10)
                {
                     
                    fulltext = dialogue11;
                }
                if (textCounter == 11)
                {
                     
                    fulltext = dialogue12;
                }
                if (textCounter == 12)
                {
                     
                    fulltext = dialogue13;
                }
                if (textCounter == 13)
                {
                     
                    fulltext = dialogue14;
                }
                currentText = "";
                StartCoroutine(ShowText());
            }
        }
    }
}
