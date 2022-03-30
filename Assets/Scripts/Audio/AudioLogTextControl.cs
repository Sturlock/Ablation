using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ablation.Item;

public class AudioLogTextControl : MonoBehaviour
{
    private List<AudioLogTextItem> texts;
    public List<Button> logbuttons;

    public void Start()
    {
        texts = new List<AudioLogTextItem>();
    }

    public void AddLog(AudioLogTextItem log)
    {
        texts.Add(log);

    }

    // public void LogText(string newTextString)
    // {
    //     GameObject newText = Instantiate(textTemplate) as GameObject;
    //     newText.SetActive(true);
    //
    //     newText.GetComponent<AudioLogTextItem>().SetText(newTextString);
    //     newText.transform.SetParent(textTemplate.transform.parent, false);
    // }

}
