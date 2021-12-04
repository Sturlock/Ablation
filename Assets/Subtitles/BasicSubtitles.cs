using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicSubtitles : MonoBehaviour
{
    public GameObject textBox;
    public bool subDec;
    public GameObject Lighting;
    public GameObject LightingSound;

    void Start()
    {
        StartCoroutine(TestingSubtitles());
    }

    IEnumerator TestingSubtitles()
    {
        yield return new WaitForSeconds(0F);
        textBox.GetComponent<Text>().text = "Well . . .  Hello and Welcome to Ellen's Unity Test Box";
        yield return new WaitForSeconds(4.5F);
        textBox.GetComponent<Text>().text = "If you're hearing this and you are not part of NOG or the lecturers at UCA";
        yield return new WaitForSeconds(4.369F);
        textBox.GetComponent<Text>().text = "something has gone terribly wrong.";
        yield return new WaitForSeconds(2.542F);
        textBox.GetComponent<Text>().text = "This piece of dialogue was only designed to be used to test";
        yield return new WaitForSeconds(2.972F);
        textBox.GetComponent<Text>().text = "the subtitles feature Ellen has been working on.";
        yield return new WaitForSeconds(3.228F);
        textBox.GetComponent<Text>().text = "As a matter of your health and safety, I request you stop the game immediately";
        yield return new WaitForSeconds(3.784F);
        textBox.GetComponent<Text>().text = "as there is nothing of interest within this section of the game and";
        yield return new WaitForSeconds(2.984F);
        textBox.GetComponent<Text>().text = "staying may place undue stress and anxiety.";
        yield return new WaitForSeconds(3.181F);
        textBox.GetComponent<Text>().text = "Remember, Safety First, People Second!";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<Text>().text = " ";
        yield return new WaitForSeconds(2);
        //Lighting.SetActive(false);
        LightingSound.SetActive(false);

}

    public void SwitchSubs()
    {
        if (subDec == false)
        {
            textBox.SetActive(true);
            subDec = true;
        }
        if (subDec == true)
        {
            textBox.SetActive(false);
            subDec = false;
        }
    }
}

