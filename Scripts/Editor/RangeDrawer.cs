using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Vun.UnityUtils
{
    // Use 2 generics params to avoid boxing
    public abstract class RangeDrawer<TRange, T> : PropertyDrawer
        where TRange : IRange<T>
        where T : IEquatable<T>
    {
        private const float MIN_LABEL_WIDTH = 32;

        private const float MAX_LABEL_WIDTH = 32;

        private const float SPACING = 6;

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            var target = property.serializedObject.targetObject;
            var field = target.GetType().GetField(property.name);
            var serializedValue = GetSerializedValue(field, target);

            var (min, max) = Draw(rect, property, serializedValue);

            // ReSharper disable CompareOfFloatsByEqualityOperator
            if (!min.Equals(serializedValue.Min))
            {
                serializedValue.Min = min;
            }
            else if (!max.Equals(serializedValue.Max))
            {
                serializedValue.Max = max;
            }

            SetValue(field, target, serializedValue);
        }

        protected virtual TRange GetSerializedValue(FieldInfo field, Object target)
        {
            return (TRange)field.GetValue(target);
        }

        protected virtual void SetValue(FieldInfo field, Object target, TRange serializedValue)
        {
            field.SetValue(target, serializedValue);
        }

        private (T min, T max) Draw(Rect rect, SerializedProperty property, TRange serializedValue)
        {
            EditorGUI.LabelField(rect, property.displayName);

            var innerStart = rect.x + EditorGUIUtility.labelWidth;
            var innerRect = new Rect(innerStart, rect.y, MIN_LABEL_WIDTH, rect.height);
            var totalFieldWidth = EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth - 25;
            var fieldWidth = totalFieldWidth / 2 - SPACING / 2 - MIN_LABEL_WIDTH;

            EditorGUI.LabelField(innerRect, "Min");

            innerRect.x = innerStart + MIN_LABEL_WIDTH;
            innerRect.width = fieldWidth;
            var min = DrawSingleField(innerRect, serializedValue.Min);

            innerRect.x = innerStart + totalFieldWidth / 2 + SPACING / 2;
            innerRect.width = MAX_LABEL_WIDTH;
            EditorGUI.LabelField(innerRect, "Max");

            innerRect.x += MAX_LABEL_WIDTH;
            innerRect.width = fieldWidth;
            var max = DrawSingleField(innerRect, serializedValue.Max);
            return (min, max);
        }

        protected abstract T DrawSingleField(Rect rect, T value);
    }
}