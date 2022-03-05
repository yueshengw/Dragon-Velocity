using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingCutsceneManager : MonoBehaviour
{
    public GameObject Player, TextObject, King;
    public GameObject sceneFader;
    public float rightForce, textObjectTimer;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(playerMovesForward());
        //TextObject.active = false;
        textObjectTimer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x < -8)
        {
            Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(rightForce, 0));
        }
        textObjectTimer -= Time.deltaTime;
        if (Player.GetComponent<Rigidbody2D>().velocity.x == 0 && textObjectTimer <= 0)
        {
            Debug.Log("Movement is done");
        }
        if (TextObject.GetComponent<CutsceneTextScript>().textCounter >= 11)
        {
            sceneFader.GetComponent<SceneFadeScript>().running2 = true;
            sceneFader.SetActive(true);
            sceneFader.GetComponent<SceneFadeScript>().isTransitioning = true;
        }
    }

    /*private IEnumerator playerMovesForward()
    {
        while (Player.transform.position.x < -8)
        {
            Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(rightForce, 0));
        }
        yield return null;
    }*/
}
