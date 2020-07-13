/******************************************************************************
 * Copyright (C) Leap Motion, Inc. 2011-2017.                                 *
 * Leap Motion proprietary and  confidential.                                 *
 *                                                                            *
 * Use subject to the terms of the Leap Motion SDK Agreement available at     *
 * https://developer.leapmotion.com/sdk_agreement, or another agreement       *
 * between Leap Motion and you, your company or other organization.           *
 ******************************************************************************/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(SingleLayer))]
    public class SingleLayerDrawer : PropertyDrawer
    {
        private GUIContent[] layerNames;
        private List<int> layerValues;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnsureLayersInitialized();

            var valueProperty = property.FindPropertyRelative(nameof(SingleLayer.value));

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
                var valueToLayer = new Dictionary<int, GUIContent>();

                for (var i = 0; i < 32; i++)
                {
                    var layerName = LayerMask.LayerToName(i);

                    if (!string.IsNullOrEmpty(layerName))
                    {
                        valueToLayer[i] = new GUIContent(layerName);
                    }
                }

                this.layerValues = valueToLayer.Keys.ToList();
                this.layerNames = valueToLayer.Values.ToArray();
            }
        }
    }
}