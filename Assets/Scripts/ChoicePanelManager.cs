using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoicePanelManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject choicePanelUI;
    public GameObject gameText;
    public GameObject cutsceneTextObject;
    public GameObject playerReputationObject;

    public bool eggInteracted = false;
    //public PlayerReputationSystem player;

    // Start is called before the first frame update
    void Start()
    {
        gameText = GameObject.Find("Game Text");
        cutsceneTextObject = GameObject.Find("CutsceneTextObject");
        choicePanelUI = GameObject.Find("Choice Panel");
        choicePanelUI.SetActive(false);
        playerReputationObject = GameObject.FindGameObjectWithTag("Player");

        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerReputationSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        playerReputationObject = GameObject.FindGameObjectWithTag("Player");

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!gameIsPaused && eggInteracted)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }

        if (cutsceneTextObject.GetComponent<CutsceneTextScript>().textCounter == 8)
        {
            SceneManager.LoadScene("Forest");
        }
        if (cutsceneTextObject.GetComponent<CutsceneTextScript>().textCounter == 13)
        {
            SceneManager.LoadScene("Forest");
        }

        if (cutsceneTextObject.GetComponent<CutsceneTextScript>().textCounter == 4)
        {
            playerReputationObject.GetComponent<PlayerReputationSystem>().eggInteractable = true;
            cutsceneTextObject.SetActive(false);
        }

    }
    public void Resume()
    {
        choicePanelUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        gameText.SetActive(true);
    }
    public void DestroyedEgg()
    {
        cutsceneTextObject.SetActive(true);
        cutsceneTextObject.GetComponent<CutsceneTextScript>().textCounter = 5;
        cutsceneTextObject.GetComponent<CutsceneTextScript>().StartCoroutine(cutsceneTextObject.GetComponent<CutsceneTextScript>().ShowText());
        cutsceneTextObject.GetComponent<CutsceneTextScript>().fulltext = "(Press Space to Continue Dialogue)";
        choicePanelUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        gameText.SetActive(true);
        PlayerPrefs.SetInt("kingReputation", PlayerPrefs.GetInt("kingReputation", 0) + 1);
        eggInteracted = false;
        Destroy(GameObject.Find("EST Dragon Egg"));
    }
    public void LeftEggAlone()
    {
        cutsceneTextObject.SetActive(true);
        cutsceneTextObject.GetComponent<CutsceneTextScript>().ShowTextRunning = false;
        cutsceneTextObject.GetComponent<CutsceneTextScript>().textCounter = 9;
        cutsceneTextObject.GetComponent<CutsceneTextScript>().StartCoroutine(cutsceneTextObject.GetComponent<CutsceneTextScript>().ShowText());
        cutsceneTextObject.GetComponent<CutsceneTextScript>().fulltext = "(Press Space to Continue Dialogue)";
        choicePanelUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        gameText.SetActive(true);
        PlayerPrefs.SetInt("dragonReputation", PlayerPrefs.GetInt("dragonReputation", 0) + 1);
        GameObject.Find("EST Dragon Egg").GetComponent<BoxCollider2D>().enabled = false;
        playerReputationObject.GetComponent<PlayerReputationSystem>().eggInteractable = false;
        eggInteracted = false;
    }
    public void Pause()
    {
        choicePanelUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        gameText.SetActive(false);
    }
    /*public void LoadMenu()
    {

    }*/
}
