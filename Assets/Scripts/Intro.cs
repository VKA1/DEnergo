using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public TextMeshProUGUI[] intros;
    public float simbolSpeed = 0.3f;
    public int blinkCount = 3;
    public float blinkTime = 3f;

    string text = "";
    bool onAnimation = false;
    bool onBlink = false;
    int activeIntro = 0;
    int textLenght = 1;
    float time = 0f;
    int blink = 0;

    // Start is called before the first frame update
    void Start()
    { 
        intros[0].gameObject.SetActive(true);
        text = intros[0].text;
        intros[0].text = "";
        for (int i = 1; i < intros.Length; i++)
        {
            intros[1].gameObject.SetActive(false);
        }
        onAnimation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (onAnimation)
        {
            time += Time.deltaTime;
            if (time >= simbolSpeed)
            {
                intros[activeIntro].text = text.Substring(0, textLenght);
                textLenght++;
                time = 0;
                if (intros[activeIntro].text.Length == text.Length) 
                { 
                    onAnimation = false;
                    onBlink = true;
                    textLenght = 1;
                }

            }
        }
        else if (onBlink)
        {
            if (blink <= blinkCount)
            {
                time += Time.deltaTime;
                if (time >= blinkTime / blinkCount / 2 && time < blinkTime / blinkCount)
                {
                    intros[activeIntro].gameObject.SetActive(false);
                }
                else if (time >= blinkTime / blinkCount)
                {
                    intros[activeIntro].gameObject.SetActive(true);
                    blink++;
                    time = 0f;
                }
            }
            else
            {
                onBlink = false;
                blink = 0;
                SwichIntro();
            }
        }

    }

    void SwichIntro()
    {
        if (activeIntro < intros.Length - 1)
        {
            activeIntro++;
            text = intros[activeIntro].text;
            intros[activeIntro - 1].gameObject.SetActive(false);
            intros[activeIntro].gameObject.SetActive(true);
            intros[activeIntro].text = "";
            onAnimation = true;
        }
        else
        {
            activeIntro = 0;
            text = intros[0].text;
            intros[intros.Length - 1].gameObject.SetActive(false);
            intros[0].gameObject.SetActive(true);
            intros[0].text = "";
            onAnimation = true;
        }
    }
}
