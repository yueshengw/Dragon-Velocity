using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerReputationSystem : MonoBehaviour
{
    public TextMeshProUGUI gameText;
    public static int dragonReputation;
    public static int kingReputation;

    public GameObject canvasObject;

    // Start is called before the first frame update
    void Start()
    {
        gameText = GameObject.Find("Game Text").GetComponent<TextMeshProUGUI>();
        gameText.text = "";

        canvasObject = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if(dragonReputation >= 3)
        {
            //Play Dragon good ending
        }

        if(kingReputation >= 3)
        {
            //Play King Good Ending
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dragon Egg")
        {
            gameText.text = "Press E to Interact";
            if (Input.GetKey(KeyCode.E))
            {
                canvasObject.GetComponent<ChoicePanelManager>().eggInteracted = true;
            }
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dragon Egg")
        {
            gameText.text = "Press E to Interact";
            if (Input.GetKey(KeyCode.E))
            {
                canvasObject.GetComponent<ChoicePanelManager>().eggInteracted = true;
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dragon Egg")
        {
            gameText.text = "";
        }
    }

}
