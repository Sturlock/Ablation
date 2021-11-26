using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[UnityEditor.CustomEditor(typeof(CharacterAI))] [CanEditMultipleObjects]
public class WaypointHandlesDrawerEditor : Editor
{
    protected CharacterAI character;
    protected bool drawWaypointsHandles = true;

    SerializedProperty characterProperty;

    void OnEnable()
    {
        //     characterProperty = serializedObject.FindProperty("lookAtPoint");
        character = serializedObject.targetObject as CharacterAI;
    }

    protected virtual void OnSceneGUI()
    {
        
        if (!drawWaypointsHandles) return;
        //Debug.Log("DRAW");
        
        var refPoint = Vector3.zero;
        //var aiBase = ;
        if (character != null)
        {
            AblationEditorHelpers.WaypointHandles(character.Waypoints, refPoint, character);
            //Debug.Log("CHARACTER");
        }
        else { Debug.Log("NULL"); }
        
    }

}
