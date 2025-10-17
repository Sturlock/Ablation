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
		public static void InterestPointHandles(List<InterestPoint> interestPoints, Vector3 referenceOffset, Object objectToUndo)
		{
			if (interestPoints == null || interestPoints.Count == 0)
				return;

			Boolean shiftPressed = Event.current.shift;

			for (Int32 currentIndex = 0; currentIndex < interestPoints.Count; currentIndex++)
			{
				// Copy the struct
				InterestPoint currentPoint = interestPoints[currentIndex];
				Vector3 worldPosition = currentPoint.Position + referenceOffset;
				Vector3 originalPosition = currentPoint.Position;

				// Draw handle
				Handles.color = Color.white;
				Handles.DrawWireDisc(worldPosition, Vector3.up, 1);

				EditorGUI.BeginChangeCheck();
				Vector3 newWorldPosition = Handles.PositionHandle(worldPosition, Quaternion.identity);

				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(objectToUndo, "Interest Point Moved");

					// Update the point's position and reassign to the list
					currentPoint.Position = newWorldPosition - referenceOffset;
					interestPoints[currentIndex] = currentPoint;

					// Optionally shift-move other points
					if (shiftPressed)
					{
						Vector3 delta = currentPoint.Position - originalPosition;
						for (Int32 otherIndex = 0; otherIndex < interestPoints.Count; otherIndex++)
						{
							if (otherIndex == currentIndex)
								continue;

							InterestPoint otherPoint = interestPoints[otherIndex];
							otherPoint.Position += delta;
							interestPoints[otherIndex] = otherPoint;
						}
					}

					PrefabUtility.RecordPrefabInstancePropertyModifications(objectToUndo);
				}

				Handles.Label(interestPoints[currentIndex].Position + referenceOffset, $"Interest Point {currentIndex + 1}");
			}
		}

	}
}
