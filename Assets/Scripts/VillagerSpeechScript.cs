using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VillagerSpeechScript : MonoBehaviour
{
    public TextMeshProUGUI SpeechText;
    public int textCounter;
    public int maxTextCount;
    public bool isInteracting;

    public string README_README_README;

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

    // Start is called before the first frame update
    void Start()
    {
        SpeechText.text = "";
        textCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteracting == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                textCounter += 1;
            }
            if (textCounter == 0)
            {
                Debug.Log("Text is at 0");
                SpeechText.text = dialogue0;
            }
            if (textCounter == 1)
            {
                Debug.Log("text is at 1");
                SpeechText.text = dialogue1;
               
            }
            if (textCounter == 2)
            {
                Debug.Log("text is at 2");
                SpeechText.text = dialogue2;
                
            }
            if (textCounter == 3)
            {
                Debug.Log("text is at 3");
                SpeechText.text = dialogue3;
                
            }
            if (textCounter == 4)
            {
                Debug.Log("text is at 4");
                SpeechText.text = dialogue4;
              
            }
            if (textCounter == 5)
            {
                Debug.Log("text is at 5");
                SpeechText.text = dialogue5;
               
            }
            if (textCounter == 6)
            {
                Debug.Log("text is at 6");
                SpeechText.text = dialogue6;
               
            }
            if (textCounter == 7)
            {
                Debug.Log("text is at 7");
                SpeechText.text = dialogue7;
               
            }
            if (textCounter == 8)
            {
                Debug.Log("text is at 8");
                SpeechText.text = dialogue8;
               
            }
            if (textCounter == 9)
            {
                Debug.Log("text is at 9");
                SpeechText.text = dialogue9;
                
            }
            if (textCounter == 10)
            {
                Debug.Log("text is at 10");
                SpeechText.text = dialogue10;
              
            }
            if (textCounter == 11)
            {
                Debug.Log("text is at 11");
                SpeechText.text = dialogue11;
                
            }
            if (textCounter == 12)
            {
                Debug.Log("text is at 12");
                SpeechText.text = dialogue12;
                
            }
            if (textCounter == 13)
            {
                Debug.Log("text is at 13");
                SpeechText.text = dialogue13;
                
            }
            if (textCounter == 14)
            {
                Debug.Log("text is at 14");
                SpeechText.text = dialogue14;
                
            }
        }
        if (textCounter >= maxTextCount)
        {
            Debug.Log("Text is Done");
        }
        Debug.Log("textCount =" + textCounter);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player entered triggerbox");
        if (collision.gameObject.name == "PlayerObject")
        {
            isInteracting = true;
            //textCounter = 0;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player has left hitbox");
        isInteracting = false;
        SpeechText.text = "";
        Debug.Log("text is being reset");
        textCounter = 0;
    }
    /*private void OnTriggerStay(Collider other)
    {
        Debug.Log("Player entered triggerbox");
        isInteracting = true;
        textCounter = 0;
    }*/
}
