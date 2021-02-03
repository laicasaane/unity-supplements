using UnityEngine;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(GridVector))]
    public class GridVectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var rowProperty = property.FindPropertyRelative(nameof(GridVector.Row).ToLower());

            if (rowProperty == null)
            {
                Debug.LogWarning("Could not find the row property, was it renamed or removed?");
                return;
            }

            var colProperty = property.FindPropertyRelative(nameof(GridVector.Column).ToLower());

            if (colProperty == null)
            {
                Debug.LogWarning("Could not find the column property, was it renamed or removed?");
                return;
            }

            label = EditorGUI.BeginProperty(position, label, property);

            var labelWidth = EditorGUIUtility.labelWidth;
            var contentPosition = EditorGUI.PrefixLabel(position, label);

            contentPosition.width *= 0.5f;
            EditorGUI.indentLevel = 0;
            EditorGUIUtility.labelWidth = 14f;
            EditorGUI.PropertyField(contentPosition, rowProperty, new GUIContent("R", nameof(GridVector.Row)));
            contentPosition.x += contentPosition.width + 2f;
            contentPosition.width -= 3f;
            EditorGUI.PropertyField(contentPosition, colProperty, new GUIContent("C", nameof(GridVector.Column)));
            EditorGUIUtility.labelWidth = labelWidth;
            EditorGUI.EndProperty();
        }
    }
}