using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Vun.UnityUtils
{
    [CustomPropertyDrawer(typeof(IntRange))]
    public class IntRangeDrawer : RangeDrawer<IntRange, int>
    {
        protected override IntRange GetSerializedValue(FieldInfo field, Object target)
        {
            return (IntRange)field.GetValue(target);
        }

        protected override void SetValue(FieldInfo field, Object target, IntRange serializedValue)
        {
            field.SetValue(target, serializedValue);
        }

        protected override int DrawSingleField(Rect rect, int value)
        {
            return EditorGUI.DelayedIntField(rect, value);
        }
    }
}