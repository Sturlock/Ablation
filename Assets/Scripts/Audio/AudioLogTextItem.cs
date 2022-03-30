using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Ablation.Item 
{
    [CreateAssetMenu(fileName = "AudioLogItem", menuName = "Ablation/audioLog")]
    public class AudioLogTextItem : ScriptableObject
    {
        public AudioClip log;
        public string text;
        public ItemType type = ItemType.AudioLog;

        public string logText
        {
            set => text = value;
            //GetComponent<Text>().text = myText;
        }

    }
}

