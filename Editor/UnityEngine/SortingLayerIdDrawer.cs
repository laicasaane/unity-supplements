using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(SortingLayerId))]
    public class SortingLayerIdDrawer : PropertyDrawer
    {
        private GUIContent[] layerNames;
        private List<int> layerValues;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnsureLayersInitialized();

            var valueProperty = property.FindPropertyRelative(nameof(SortingLayerId.id));

            if (valueProperty == null)
            {
                Debug.LogWarning("Could not find the layer index property, was it renamed or removed?");
                return;
            }

            var index = this.layerValues.IndexOf(valueProperty.intValue);

            if (index < 0)
            {
                if (Application.isPlaying)
                {
                    // If application is playing we dont want to change the layers on the fly
                    // Instead, just display them as an int value
                    valueProperty.intValue = EditorGUI.IntField(position, property.displayName, valueProperty.intValue);
                    return;
                }
                else
                {
                    // If the application is not running, reset the layer to the default layer
                    valueProperty.intValue = 0;
                    index = 0;
                }
            }

            var tooltipAttribute = this.fieldInfo.GetCustomAttributes(typeof(TooltipAttribute), true)
                                                 .Cast<TooltipAttribute>()
                                                 .FirstOrDefault();

            if (tooltipAttribute != null)
            {
                label.tooltip = tooltipAttribute.tooltip;
            }

            index = EditorGUI.Popup(position, label, index, this.layerNames);
            valueProperty.intValue = this.layerValues[index];
        }

        private void EnsureLayersInitialized()
        {
            if (this.layerNames == null)
            {
                var idToLayer = new Dictionary<int, GUIContent>();

                foreach (var layer in SortingLayer.layers)
                {
                    idToLayer[layer.id] = new GUIContent(layer.name);
                }

                this.layerValues = idToLayer.Keys.ToList();
                this.layerNames = idToLayer.Values.ToArray();
            }
        }
    }
}