using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{
    public float fadeTimer;
    public bool start;

    // Start is called before the first frame update
    void Start()
    {
        start = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (start == true)
        {
            StartCoroutine(fadeToBlack());
        }
    }

    public IEnumerator fadeToBlack()
    {
        while (gameObject.GetComponent<SpriteRenderer>().color.a < 1)
        {
            gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1/100);
        }
        yield return null;
    }
}
