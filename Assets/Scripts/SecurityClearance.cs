using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ClearenceLvl { white, blue };

public class SecurityClearance : MonoBehaviour
{
    
    public ClearenceLvl lvl = ClearenceLvl.white;

    public ClearenceLvl GetCurrentLevel
    {
        get { return lvl; }
    }

    public void IncreaseLevel()
    {
        lvl++;
    }
}
