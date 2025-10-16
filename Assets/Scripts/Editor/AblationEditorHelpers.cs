using System;
using System.Collections.Generic;
using Enemy;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor
{
	public class AblationEditorHelpers : MonoBehaviour
	{
		public static void WaypointHandles(List<InterestPoint> interestPoints, Vector3 refPoint, Object objectToUndo)
		{
			//Checking if we have a waypoint list
			if (interestPoints == null) return;
			if (interestPoints.Count == 0) return;

			// are we pressing l shift?
			Boolean shiftPressed = Event.current.shift;

			for (Int32 i = 0; i < interestPoints.Count; i++)
			{
				InterestPoint interestPoint = interestPoints[i];

				Vector3 position = interestPoint.Position + refPoint;
				Vector3 beforeChangePos = interestPoint.Position;

				Handles.color = Color.white;
				Handles.DrawWireDisc(position, Vector3.up, 1);

				EditorGUI.BeginChangeCheck();
				Vector3 pos = Handles.PositionHandle(position, Quaternion.identity);

				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(objectToUndo, "Waypoint move");
					interestPoint.Position = pos - refPoint;

					if (shiftPressed)
					{
						Vector3 posDelta = interestPoint.Position - beforeChangePos;

						for (Int32 index = 0; index < interestPoints.Count; index++)
						{
							if (index == i) continue;
							InterestPoint waypoint = interestPoints[index];
							waypoint.Position += posDelta;
						}
					}
					PrefabUtility.RecordPrefabInstancePropertyModifications(objectToUndo);
				}

				Handles.Label(position, $"Waypoint {i + 1}");
				if (i < interestPoints.Count - 1)
					Handles.DrawLine(position, interestPoints[i + 1].Position + refPoint);
			}
		}
	}
}
