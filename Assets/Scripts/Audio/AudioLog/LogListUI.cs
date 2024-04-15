using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogListUI : MonoBehaviour
{
    [SerializeField] private AudioLogUI logPrefab;
    private LogList logList;
    // Start is called before the first frame update
    void OnEnable()
    {
        logList = GameObject.FindGameObjectWithTag("Player").GetComponent<LogList>();
        logList.onUpdate += Redraw;
    }

    private void Redraw()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
        foreach (LogStatus log in logList.GetLogs())
        {
            AudioLogUI uiInstance = Instantiate<AudioLogUI>(logPrefab, transform);
            uiInstance.Setup(log);
        }
    }
}
