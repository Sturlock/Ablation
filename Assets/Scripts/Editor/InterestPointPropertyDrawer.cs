using System;
using Enemy;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomPropertyDrawer(typeof(InterestPoint)), ExecuteInEditMode]
	public class InterestPointPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty positionProperty = property.FindPropertyRelative(nameof(InterestPoint.Position));
			SerializedProperty weightProperty = property.FindPropertyRelative(nameof(InterestPoint.Weight));
			SerializedProperty durationProperty = property.FindPropertyRelative(nameof(InterestPoint.Duration));

			EditorGUI.BeginProperty(position, label, property);

			// Get standard line height and spacing
			Single lineHeight = EditorGUIUtility.singleLineHeight;
			Single spacing = EditorGUIUtility.standardVerticalSpacing;

			// Draw foldout label (optional)
			property.isExpanded = EditorGUI.Foldout(
				new Rect(position.x, position.y, position.width, lineHeight),
				property.isExpanded,
				label,
				true
			);

			// //Draw lable
			// position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive),
			// 	new GUIContent(label.text.Replace("Interest Point", "")));

			if (property.isExpanded)
			{
				EditorGUI.indentLevel++;

				// Start Y after foldout
				Single y = position.y + lineHeight + spacing;
				DrawPropertyWithDynamicLabel(position.x, y, position.width, positionProperty, nameof(InterestPoint.Position));

				y += lineHeight + spacing;
				DrawPropertyWithDynamicLabel(position.x, y, position.width, weightProperty, nameof(InterestPoint.Weight));

				y += lineHeight + spacing;
				DrawPropertyWithDynamicLabel(position.x, y, position.width, durationProperty, nameof(InterestPoint.Duration));

				EditorGUI.indentLevel--;
			}

			EditorGUI.EndProperty();
		}
		
		public override Single GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			Single lineHeight = EditorGUIUtility.singleLineHeight;
			Single spacing = EditorGUIUtility.standardVerticalSpacing;

			// Always at least one line (the foldout)
			Single height = lineHeight;

			if (property.isExpanded)
			{
				// 3 fields, each with spacing between them
				const Int32 FIELD_COUNT = 3;
				height += (lineHeight + spacing) * FIELD_COUNT;
			}

			return height;
		}
		
		private static void DrawPropertyWithDynamicLabel(Single x, Single y, Single totalWidth, SerializedProperty prop, String labelText)
		{
			Single lineHeight = EditorGUIUtility.singleLineHeight;

			// Calculate label width dynamically
			Vector2 labelSize = EditorStyles.label.CalcSize(new GUIContent(labelText));
			Single labelWidth = labelSize.x + 15f; // small padding

			Rect labelRect = new Rect(x, y, labelWidth, lineHeight);
			Rect propertyRect = new Rect(x + labelWidth, y, totalWidth - labelWidth, lineHeight);

			EditorGUI.LabelField(labelRect, labelText);
			EditorGUI.PropertyField(propertyRect, prop, GUIContent.none);
		}

	}
}
