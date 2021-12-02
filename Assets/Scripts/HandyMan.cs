using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandyMan : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI contence;

    private void Start()
    {
        title.text = "HandyMan";
        contence.text = "HandyMan: Home";
    }
    public void Status()
    {
        title.text = "Status";
        contence.text = "Status Stuff";
    }

	public void Inventory()
    {
        title.text = "Status";
        contence.text = "Status Stuff";
    }

	public void Options()
    {
        title.text = "Status";
        contence.text = "Status Stuff";
    }

	public void AudioLogs()
    {
        title.text = "Status";
        contence.text = "Status Stuff";
    }
	public void MotionDetector()
    {
        title.text = "Status";
        contence.text = "Status Stuff";
    }
}
