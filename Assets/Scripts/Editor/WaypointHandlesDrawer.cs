using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[UnityEditor.CustomEditor(typeof(Waypoint), true)] [CanEditMultipleObjects]
public class WaypointHandlesDrawer : Editor
{
    protected MoveToTarget character;
    protected bool drawWaypointsHandles;
    
    protected virtual void OnSceneGUI()
    {
        if (drawWaypointsHandles) return;

        
        var refPoint = Vector3.zero;
        //var aiBase = ;
        if (character != null)
        {
            AblationEditorHelpers.WaypointHandles(character.waypoints, refPoint, character);
        }
        else Debug.Log("NULL");
        
    }

}
