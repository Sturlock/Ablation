using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[UnityEditor.CustomEditor(typeof(MoveToTarget))]
public class WaypointHandlesDrawerEditor : Editor
{
    protected MoveToTarget character;
    protected bool drawWaypointsHandles = true;
    
    protected virtual void OnSceneGUI()
    {
        if (!drawWaypointsHandles) return;
        Debug.Log("DRAW");
        
        var refPoint = Vector3.zero;
        //var aiBase = ;
        if (character != null)
        {
            
            AblationEditorHelpers.WaypointHandles(character.waypoints, refPoint, character);
            Debug.Log("CHARACTER");
        }
        else { Debug.Log("NULL"); }
        
    }

}
