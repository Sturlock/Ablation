using Enemy;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(Waypoint)), ExecuteInEditMode]
	public class WaypointPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			//Draw lable
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), 
				new GUIContent(label.text.Replace("Waypoint", "")));

			var indent = EditorGUI.indentLevel;

			position.xMin = indent;

			//Rect Calculation
			float width = position.width - 55 - indent - position.xMin;

			Rect posRect = new Rect(position.xMin + 55, position.y, width * .45f, position.height);
			Rect radiusLableRect = new Rect(posRect.xMax + width * .01f, position.y, width * .1f, position.height);
			Rect radiusRect = new Rect(radiusLableRect.xMax + width * .01f, position.y, width * .42f, position.height);

			//Draw Fields
			EditorGUI.PropertyField(posRect, property.FindPropertyRelative("position"), GUIContent.none);
			EditorGUI.LabelField(radiusLableRect, "Radius");
			EditorGUI.Slider(radiusRect, property.FindPropertyRelative("radius"), 0, 50, GUIContent.none);

			EditorGUI.EndProperty();
		}
	}
}
