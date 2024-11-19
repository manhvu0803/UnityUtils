using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Vun.UnityUtils
{
    [CustomPropertyDrawer(typeof(Range))]
    public class FloatRangeDrawer : RangeDrawer<Range, float>
    {
        protected override Range GetSerializedValue(FieldInfo field, Object target)
        {
            return (Range)field.GetValue(target);
        }

        protected override void SetValue(FieldInfo field, Object target, Range serializedValue)
        {
            field.SetValue(target, serializedValue);
        }

        protected override float DrawSingleField(Rect rect, float value)
        {
            return EditorGUI.DelayedFloatField(rect, value);
        }
    }
}