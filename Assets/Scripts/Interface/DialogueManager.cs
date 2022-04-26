using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class DialogueManager : Singleton<DialogueManager>
{
    [ReadOnly] private AudioClip dialogueAudio;
    [ReadOnly] private AudioSource dialogueSource;
    private const float _RATE = 44100.0f;

    private string[] fileLines;

    //Subtitles Variables
    [Header("Subtitle Variables"),Space]
    private List<string> subtitleLines = new List<string>();

    private List<string> subtitleTimingStrings = new List<string>();
    public List<float> subtitleTimings = new List<float>();

    public List<string> subtitleText = new List<string>();
    private int nextSubtitle = 0;

    private string displaySubtitle;

    //Trigger Variables
    [Header("Trigger Variables"), Space]
    private List<string> triggerLines = new List<string>();

    private List<string> triggerTimingStrings = new List<string>();
    public List<float> triggerTimings = new List<float>();

    private List<string> triggers = new List<string>();
    public List<string> triggerObjectNames = new List<string>();
    public List<string> triggerMethodNames = new List<string>();

    private int nextTrigger = 0;

    //GUI
    private GUIStyle subtitleStyle = new GUIStyle();
    //BAD WE HAVE SINGLETON FOR THIS
    //public static DialogueManager Instance { get; private set; }
    void Start()
    {
        //if(Instance != null && Instance != this) 
        //{
        //    Destroy(gameObject);
        //}
        //Instance = this;
        dialogueSource = gameObject.AddComponent<AudioSource>();
    }

    public void StopDialogue()
    {
        if (dialogueSource != null)
        {
            dialogueSource.Stop();
        }
    }

    public void BeginDialogue (AudioClip passedClip)
    {
        dialogueAudio = passedClip;

        //reset variables
        ResetVars();
        nextSubtitle = 0;
        nextTrigger = 0;

        //TEXTFILE READING
        TextAsset temp = Resources.Load("Dialogues/" + dialogueAudio.name) as TextAsset;
        fileLines = temp.text.Split('\n');

        //split subtitle lines
        foreach(string line in fileLines)
        {
            if (line.Contains("<trigger/>"))
            {
                triggerLines.Add(line);
            }
            else
            {
                subtitleLines.Add(line);
            }
        }

        //split subtitle elements
        for(int count = 0; count < subtitleLines.Count; count++)
        {
            string[] splitTemp = subtitleLines[count].Split('|');
            subtitleTimingStrings.Add(splitTemp[0]);
            subtitleTimings.Add(float.Parse(CleanTimeString(subtitleTimingStrings[count])));
            subtitleText.Add(splitTemp[1]);
        }

        //split trigger elements
        for (int count = 0; count < triggerLines.Count; count++)
        {
            string[] splitTemp1 = triggerLines[count].Split('|');
            triggerTimingStrings.Add(splitTemp1[0]);
            triggerTimings.Add(float.Parse(CleanTimeString(triggerTimingStrings[count])));

            //splitting triggers more
            triggers.Add(splitTemp1[1]);
            string[] splitTemp2 = triggers[count].Split('-');
            splitTemp2[0] = splitTemp2[0].Replace("<trigger/>", "");
            triggerObjectNames.Add(splitTemp2[0]);
            triggerMethodNames.Add(splitTemp2[1]);
        }

        if (subtitleText[0] != null)
        {
            displaySubtitle = subtitleText[0];
        }


        //set and play
        dialogueSource.clip = dialogueAudio;
        dialogueSource.Play();
    }

    
    private string CleanTimeString(string timeString)
    {
        Regex digitsOnly = new Regex(@"[^\d+(\.\d+)*s]"); /////
        return digitsOnly.Replace(timeString, "");
    }

    void OnGUI()
    {
        //using dialogueAudio File?
        //Debug.Log(GetComponent<AudioSource>().clip.name);
        //Debug.Log(dialogueAudio.name);
        if (dialogueAudio != null && dialogueSource.clip.name == dialogueAudio.name)
        {
            //check for breaks and negatives
            if (nextSubtitle > 0 && !subtitleText[nextSubtitle - 1].Contains("<break/>"))
            {
                //create GUI
                GUI.depth = -1001;
                subtitleStyle.fixedWidth = Screen.width / 1.5f;
                subtitleStyle.wordWrap = true;
                subtitleStyle.alignment = TextAnchor.MiddleCenter;
                subtitleStyle.normal.textColor = Color.white;
                subtitleStyle.fontSize = Mathf.FloorToInt(Screen.height * 0.0225f);

                Vector2 size = subtitleStyle.CalcSize(new GUIContent());
                GUI.contentColor = Color.black;
                GUI.Label(new Rect(Screen.width / 2 - size.x / 2 + 1, Screen.height / 1.05f - size.y + 1, size.x, size.y), displaySubtitle, subtitleStyle);
                GUI.contentColor = Color.white;
                GUI.Label(new Rect(Screen.width / 2 - size.x / 2, Screen.height / 1.05f - size.y, size.x, size.y), displaySubtitle, subtitleStyle);
            }

            //increment it bb
            if (nextSubtitle < subtitleText.Count)
            {
                if (GetComponent<AudioSource>().timeSamples / _RATE > subtitleTimings[nextSubtitle])
                {
                    displaySubtitle = subtitleText[nextSubtitle];
                    nextSubtitle++;
                }
            }

            if (nextTrigger < triggers.Count)
            {
                if (GetComponent<AudioSource>().timeSamples / _RATE > triggerTimings[nextTrigger])
                {
                    GameObject.Find(triggerObjectNames[nextTrigger]).SendMessage(triggerMethodNames[nextTrigger]);
                    nextTrigger++;
                }

            }
        }
    }
    private void ResetVars()
    {
        subtitleLines = new List<string>();
        subtitleTimingStrings = new List<string>();
        subtitleTimings = new List<float>();
        subtitleText = new List<string>();

        triggerLines = new List<string>();
        triggerTimingStrings = new List<string>();
        triggerTimings = new List<float>();
        triggers = new List<string>();
        triggerObjectNames = new List<string>();
        triggerMethodNames = new List<string>();
    }
}
