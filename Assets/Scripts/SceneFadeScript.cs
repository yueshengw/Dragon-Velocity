using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeScript : MonoBehaviour
{
    public bool isTransitioning;
    public bool running2;
    public Coroutine fadeIn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Image>()));
        running2 = true;
        isTransitioning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTransitioning == true)
        {
            StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Image>()));
        }
        if (running2 == false)
        {
            gameObject.SetActive(false);
            Debug.Log("working");
        }


    }

    public IEnumerator FadeTextToFullAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            //running1 = true;
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
            //running1 = false;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            running2 = true;
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
            running2 = false;
        }
    }
}
