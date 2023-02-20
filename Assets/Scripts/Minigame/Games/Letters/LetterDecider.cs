using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterDecider : MonoBehaviour
{

    public Text text;
    char[] acceptableLetters = "ZXCVBNM".ToCharArray();
    public string selectedLetter;

    void Start()
    {
        selectedLetter = null;
        text.text = " ";
        StartCoroutine(DecideLetters());
    }

    IEnumerator DecideLetters()
    {
        yield return new WaitForSeconds(0.5f);

        selectedLetter = acceptableLetters[Random.Range(0, 6)].ToString();
        text.text = selectedLetter;

        yield return new WaitForSeconds(1.6f);

        if(text.color != Color.green)
        {
           text.text = " ";
        }

    }
}
