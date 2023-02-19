using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterDecider : MonoBehaviour
{

    public Text text;
    char[] acceptableLetters = "zxcvbnm".ToCharArray();
    public string selectedLetter;

    void Start()
    {
        selectedLetter = null;
        text.text = " ";
        StartCoroutine(DecideLetters());
    }

    IEnumerator DecideLetters()
    {
        yield return new WaitForSeconds(2);

        selectedLetter = acceptableLetters[Random.Range(0, 6)].ToString();
        text.text = selectedLetter;

        yield return new WaitForSeconds(1);

        if(text.color != Color.green)
        {
           text.text = " ";
        }

    }
}
