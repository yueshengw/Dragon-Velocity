using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicePanelManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject choicePanelUI;
    public GameObject gameText;

    public bool eggInteracted = false;
    //public PlayerReputationSystem player;

    // Start is called before the first frame update
    void Start()
    {
        gameText = GameObject.Find("Game Text");
        choicePanelUI.SetActive(false);

        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerReputationSystem>();
    }
    // Update is called once per frame
    void Update()
    {
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
        choicePanelUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        gameText.SetActive(true);
        PlayerReputationSystem.kingReputation += 1;
        eggInteracted = false;
        Destroy(GameObject.FindGameObjectWithTag("Dragon Egg"));
    }
    public void LeftEggAlone()
    {
        choicePanelUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        gameText.SetActive(true);
        PlayerReputationSystem.dragonReputation += 1;
        GameObject.FindGameObjectWithTag("Dragon Egg").GetComponent<BoxCollider2D>().enabled = false;
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
