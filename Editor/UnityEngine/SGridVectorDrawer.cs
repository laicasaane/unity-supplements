using UnityEngine;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(SGridVector))]
    public class SGridVectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var rowProperty = property.FindPropertyRelative(nameof(SGridVector.Row).ToLower());

            if (rowProperty == null)
            {
                Debug.LogWarning("Could not find the row property, was it renamed or removed?");
                return;
            }

            var colProperty = property.FindPropertyRelative(nameof(SGridVector.Column).ToLower());

            if (colProperty == null)
            {
                Debug.LogWarning("Could not find the column property, was it renamed or removed?");
                return;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            var labelWidth = EditorGUIUtility.labelWidth;
            Rect contentPosition = EditorGUI.PrefixLabel(position, label);

            if (position.height > 16f)
            {
                position.height = 16f;
                EditorGUI.indentLevel += 1;
                contentPosition = EditorGUI.IndentedRect(position);
                contentPosition.y += 18f;
            }

            contentPosition.width *= 0.5f;
            EditorGUI.indentLevel = 0;
            EditorGUIUtility.labelWidth = 14f;
            EditorGUI.PropertyField(contentPosition, rowProperty, new GUIContent("R", nameof(SGridVector.Row)));
            contentPosition.x += contentPosition.width + 2f;
            contentPosition.width -= 3f;
            EditorGUI.PropertyField(contentPosition, colProperty, new GUIContent("C", nameof(SGridVector.Column)));
            EditorGUIUtility.labelWidth = labelWidth;
            EditorGUI.EndProperty();
        }
    }
}