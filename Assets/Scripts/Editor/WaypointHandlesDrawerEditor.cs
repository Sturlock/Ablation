using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[UnityEditor.CustomEditor(typeof(MoveToTarget))] [CanEditMultipleObjects]
public class WaypointHandlesDrawerEditor : Editor
{
    protected MoveToTarget character;
    protected bool drawWaypointsHandles = true;

    SerializedProperty characterProperty;

    void OnEnable()
    {
        //     characterProperty = serializedObject.FindProperty("lookAtPoint");
        character = serializedObject.targetObject as MoveToTarget;
    }

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
