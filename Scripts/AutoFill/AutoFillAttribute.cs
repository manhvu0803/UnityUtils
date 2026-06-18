using System;

namespace Vun.UnityUtils
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AutoFillAttribute : Attribute
    {
        public readonly FillOption FillOption;

        public readonly bool IncludeInactive;

        public AutoFillAttribute(FillOption fillOption = FillOption.FromGameObject, bool includeInactive = true)
        {
            IncludeInactive = includeInactive;
            FillOption = fillOption;
        }
    }
}