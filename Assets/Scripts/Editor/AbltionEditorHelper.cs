using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AbltionEditorHelpers : MonoBehaviour
{
    public static void WaypointHandles(List<Waypoint> _waypoints, Vector3 _refpoint, Object _objectToUndo)
    {
        //Checking if we have a waypoint list
        if (_waypoints == null) return;

        //is Shift pressed?
        bool shiftPressed = Event.current.shift;

        for (var i = 0; i < _waypoints.Count; i++)
        {
            Vector3 position = _waypoints[i].position + _refpoint;
            var beforeChangePos = _waypoints[i].position;

            Handles.color = Color.white;
            Handles.DrawWireDisc(position, Vector3.up, _waypoints[i].radius);

            EditorGUI.BeginChangeCheck();
            var pos = Handles.PositionHandle(position, Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_objectToUndo, "Waypoint Move");
                _waypoints[i].position = pos - _refpoint;

                if (shiftPressed)
                {
                    var posDelta = _waypoints[i].position - beforeChangePos;

                    for(var index = 0; index < _waypoints.Count; index++)
                    {
                        if (index == i) continue;
                        var waypoint = _waypoints[index];
                        waypoint.position += posDelta;
                    }
                }
                PrefabUtility.RecordPrefabInstancePropertyModifications(_objectToUndo);
            }

            Handles.Label(position, $"Waypoint {i + 1}");
            if(i <_waypoints.Count - 1)
            {
                Handles.DrawLine(position, _waypoints[i + 1].position + _refpoint);
            }
        }
    }
}
