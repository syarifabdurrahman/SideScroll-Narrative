using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeSystem : MonoBehaviour
{
    public static FadeSystem instance;

    public Image blackScreen;

    public float fadeSpeed = 2f;
    public bool fadeToBlack, fadeFromBlack;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator StartGameFade()
    {
        fadeFromBlack = true;
        yield return new WaitForSeconds(.5f);
        fadeToBlack = false;
    }

    private void Update()
    {
        if (fadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 0)
            {
                fadeFromBlack = false;
            }
        }
    }
}
