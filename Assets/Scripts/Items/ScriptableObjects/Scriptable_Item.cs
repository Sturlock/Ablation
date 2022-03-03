using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Inventory Item", menuName = "Ablation")]
public class Scriptable_Item : ScriptableObject
{
    public string name = "Item";
    public string description = "Item Description";
    public Sprite icon;
    public float weight = 0f;
    public ItemType type;
}

public enum ItemType { Distraction, KeyCard, }
