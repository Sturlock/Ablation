using System;
using System.Collections.Generic;
using System.Reflection;
using Enemy;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomEditor(typeof(Director))] [CanEditMultipleObjects]
	public class InterestPointHandlesDrawerEditor : UnityEditor.Editor
	{
		protected Director _Director;
		protected const Boolean DRAW_WAYPOINTS_HANDLES = true;
		private SerializedProperty _CharacterProperty;

		private void OnEnable()
		{
			_Director = serializedObject.targetObject as Director;
		}

		protected virtual void OnSceneGUI()
		{
			if (!DRAW_WAYPOINTS_HANDLES) return;

			Vector3 refPoint = Vector3.zero;
			if (!_Director) return;
			FieldInfo fieldInfo = typeof(Director).GetField("_InterestPoints", BindingFlags.NonPublic | BindingFlags.Instance);

			if (fieldInfo?.GetValue(_Director) is not List<InterestPoint> interestPoints) return;
			if (interestPoints.Count <= 0) return;

			AblationEditorHelpers.InterestPointHandles(interestPoints, refPoint, _Director);
		}
	}
}
