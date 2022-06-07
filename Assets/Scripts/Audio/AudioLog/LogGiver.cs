using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogGiver : MonoBehaviour
{
    [SerializeField] private AudioLogItem audioLog;

    public void GiveLog()
    {
        LogList logList = GameObject.FindGameObjectWithTag
            ("Player").GetComponent<LogList>();
        logList.AddLog(audioLog);
    }

}
