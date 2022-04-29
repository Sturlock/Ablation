using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Image _crosshair;

    public Image crosshair
    {
        get { return _crosshair; }
    }
}
