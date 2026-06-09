//-----------------------------------------------------------------------
// <copyright file="Sirenix.OdinInspector.Attributes.Stub.cs" company="Sirenix ApS">
// Copyright (c) 2021 Sirenix ApS
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

// Attributes stub generated for Odin Inspector version 3.0.9.0

using System.Diagnostics;
// ReSharper disable UnusedParameter.Local
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
// ReSharper disable PublicConstructorInAbstractClass

#if !ODIN_INSPECTOR
namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class AssetListAttribute : Attribute
    {
        public bool AutoPopulate = false;
        public string Tags = null;
        public string LayerNames = null;
        public string AssetNamePrefix = null;
        public string Path;
        public string CustomFilterMethod = null;
    }
}

namespace Sirenix.OdinInspector
{
    using System;
    using System.Linq;

    [Conditional("UNITY_EDITOR")]
    public class AssetSelectorAttribute : Attribute
    {
        public bool IsUniqueList = true;
        public bool DrawDropdownForListElements = true;
        public bool DisableListAddButtonBehaviour;
        public bool ExcludeExistingValuesInList;
        public bool ExpandAllMenuItems = true;
        public bool FlattenTreeView;
        public int DropdownWidth;
        public int DropdownHeight;
        public string DropdownTitle;
        public string[] SearchInFolders;
        public string Filter;

        public string Paths
        {
            set { SearchInFolders = value.Split('|').Select(x => x.Trim().Trim('/', '\\')).ToArray(); }
            get => SearchInFolders == null ? null : string.Join(",", SearchInFolders);
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class AssetsOnlyAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class BoxGroupAttribute : PropertyGroupAttribute
    {
        public bool ShowLabel;
        public bool CenterLabel;
        public string LabelText;

        public BoxGroupAttribute(string group, bool showLabel = true, bool centerLabel = false, float order = 0) : base(group, order)
        {
            ShowLabel = showLabel;
            CenterLabel = centerLabel;
        }

        public BoxGroupAttribute() : this("_DefaultBoxGroup", false)
        {
        }

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            var attr = other as BoxGroupAttribute;
            if (!ShowLabel || !attr.ShowLabel)
            {
                ShowLabel = false;
                attr.ShowLabel = false;
            }

            CenterLabel |= attr.CenterLabel;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    [Conditional("UNITY_EDITOR")]
    public class ButtonAttribute : ShowInInspectorAttribute
    {
        public int ButtonHeight;
        public string Name;
        public ButtonStyle Style;
        public bool Expanded;
        public bool DisplayParameters = true;
        public bool DirtyOnClick = true;

        public bool DrawResult
        {
            set
            {
                drawResult = value;
                DrawResultIsSet = true;
            }
            get => drawResult;
        }

        public bool DrawResultIsSet { get; private set; }

        private bool drawResult;

        public ButtonAttribute()
        {
            Name = null;
            ButtonHeight = (int)ButtonSizes.Small;
        }

        public ButtonAttribute(ButtonSizes size)
        {
            Name = null;
            ButtonHeight = (int)size;
        }

        public ButtonAttribute(int buttonSize)
        {
            ButtonHeight = buttonSize;
            Name = null;
        }

        public ButtonAttribute(string name)
        {
            Name = name;
            ButtonHeight = (int)ButtonSizes.Small;
        }

        public ButtonAttribute(string name, ButtonSizes buttonSize)
        {
            Name = name;
            ButtonHeight = (int)buttonSize;
        }

        public ButtonAttribute(string name, int buttonSize)
        {
            Name = name;
            ButtonHeight = buttonSize;
        }

        public ButtonAttribute(ButtonStyle parameterBtnStyle)
        {
            Name = null;
            ButtonHeight = (int)ButtonSizes.Small;
            Style = parameterBtnStyle;
        }

        public ButtonAttribute(int buttonSize, ButtonStyle parameterBtnStyle)
        {
            ButtonHeight = buttonSize;
            Name = null;
            Style = parameterBtnStyle;
        }

        public ButtonAttribute(ButtonSizes size, ButtonStyle parameterBtnStyle)
        {
            ButtonHeight = (int)size;
            Name = null;
            Style = parameterBtnStyle;
        }

        public ButtonAttribute(string name, ButtonStyle parameterBtnStyle)
        {
            Name = name;
            ButtonHeight = (int)ButtonSizes.Small;
            Style = parameterBtnStyle;
        }

        public ButtonAttribute(string name, ButtonSizes buttonSize, ButtonStyle parameterBtnStyle)
        {
            Name = name;
            ButtonHeight = (int)buttonSize;
            Style = parameterBtnStyle;
        }

        public ButtonAttribute(string name, int buttonSize, ButtonStyle parameterBtnStyle)
        {
            Name = name;
            ButtonHeight = buttonSize;
            Style = parameterBtnStyle;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [IncludeMyAttributes]
    [ShowInInspector]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class ButtonGroupAttribute : PropertyGroupAttribute
    {
        public ButtonGroupAttribute(string group = "_DefaultGroup", float order = 0) : base(group, order)
        {
        }
    }
}

namespace Sirenix.OdinInspector
{
    public enum ButtonStyle
    {
        CompactBox,
        FoldoutButton,
        Box
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [Conditional("UNITY_EDITOR")]
    public class ChildGameObjectsOnlyAttribute : Attribute
    {
        public bool IncludeSelf = true;
        public bool IncludeInactive = false;
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class ColorPaletteAttribute : Attribute
    {
        public string PaletteName;
        public bool ShowAlpha;

        public ColorPaletteAttribute()
        {
            PaletteName = null;
            ShowAlpha = true;
        }

        public ColorPaletteAttribute(string paletteName)
        {
            PaletteName = paletteName;
            ShowAlpha = true;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class CustomContextMenuAttribute : Attribute
    {
        public string MenuItem;

        [Obsolete("Use the Action member instead.", false)]
        public string MethodName
        {
            get => Action;
            set => Action = value;
        }

        public string Action;

        public CustomContextMenuAttribute(string menuItem, string action)
        {
            MenuItem = menuItem;
            Action = action;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class CustomValueDrawerAttribute : Attribute
    {
        [Obsolete("Use the Action member instead.", false)]
        public string MethodName
        {
            get => Action;
            set => Action = value;
        }

        public string Action;

        public CustomValueDrawerAttribute(string action)
        {
            Action = action;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class DelayedPropertyAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [DontApplyToListElements]
    [Conditional("UNITY_EDITOR")]
    public class DetailedInfoBoxAttribute : Attribute
    {
        public string Message;
        public string Details;
        public InfoMessageType InfoMessageType;
        public string VisibleIf;

        public DetailedInfoBoxAttribute(string message, string details, InfoMessageType infoMessageType = InfoMessageType.Info, string visibleIf = null)
        {
            Message = message;
            Details = details;
            InfoMessageType = infoMessageType;
            VisibleIf = visibleIf;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [Conditional("UNITY_EDITOR")]
    public sealed class DictionaryDrawerSettings : Attribute
    {
        public string KeyLabel = "Key";
        public string ValueLabel = "Value";
        public DictionaryDisplayOptions DisplayMode;
        public bool IsReadOnly;
        public float KeyColumnWidth = 130f;
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class DisableContextMenuAttribute : Attribute
    {
        public bool DisableForMember;
        public bool DisableForCollectionElements;

        public DisableContextMenuAttribute(bool disableForMember = true, bool disableCollectionElements = false)
        {
            DisableForMember = disableForMember;
            DisableForCollectionElements = disableCollectionElements;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class DisableIfAttribute : Attribute
    {
        [Obsolete("Use the Condition member instead.", false)]
        public string MemberName
        {
            get => Condition;
            set => Condition = value;
        }

        public string Condition;
        public object Value;

        public DisableIfAttribute(string condition)
        {
            Condition = condition;
        }

        public DisableIfAttribute(string condition, object optionalValue)
        {
            Condition = condition;
            Value = optionalValue;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class DisableInEditorModeAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class DisableInInlineEditorsAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class DisableInNonPrefabsAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [DontApplyToListElements]
    [Conditional("UNITY_EDITOR")]
    public class DisableInPlayModeAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class DisableInPrefabAssetsAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class DisableInPrefabInstancesAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class DisableInPrefabsAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class DisplayAsStringAttribute : Attribute
    {
        public bool Overflow;

        public DisplayAsStringAttribute()
        {
            Overflow = true;
        }

        public DisplayAsStringAttribute(bool overflow)
        {
            Overflow = overflow;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [Conditional("UNITY_EDITOR")]
    public sealed class DoNotDrawAsReferenceAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DontApplyToListElementsAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class DontValidateAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class DrawWithUnityAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [Obsolete("Use DisableInPrefabInstance or DisableInPrefabAsset instead.", false)]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class EnableForPrefabOnlyAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class EnableGUIAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class EnableIfAttribute : Attribute
    {
        [Obsolete("Use the Condition member instead.", false)]
        public string MemberName
        {
            get => Condition;
            set => Condition = value;
        }

        public string Condition;
        public object Value;

        public EnableIfAttribute(string condition)
        {
            Condition = condition;
        }

        public EnableIfAttribute(string condition, object optionalValue)
        {
            Condition = condition;
            Value = optionalValue;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class EnumPagingAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [Conditional("UNITY_EDITOR")]
    public class EnumToggleButtonsAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class FilePathAttribute : Attribute
    {
        public bool AbsolutePath;
        public string Extensions;
        public string ParentFolder;

        [Obsolete("Use RequireExistingPath instead.", true)]
        public bool RequireValidPath;

        public bool RequireExistingPath;
        public bool UseBackslashes;

        [Obsolete("Add a ReadOnly attribute to the property instead.", true)]
        public bool ReadOnly { get; set; }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class FolderPathAttribute : Attribute
    {
        public bool AbsolutePath;
        public string ParentFolder;

        [Obsolete("Use RequireExistingPath instead.", true)]
        public bool RequireValidPath;

        public bool RequireExistingPath;
        public bool UseBackslashes;
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class FoldoutGroupAttribute : PropertyGroupAttribute
    {
        private bool expanded;

        public bool Expanded
        {
            get => expanded;
            set
            {
                expanded = value;
                HasDefinedExpanded = true;
            }
        }

        public bool HasDefinedExpanded { get; private set; }

        public FoldoutGroupAttribute(string groupName, float order = 0) : base(groupName, order)
        {
        }

        public FoldoutGroupAttribute(string groupName, bool expanded, float order = 0) : base(groupName, order)
        {
            this.expanded = expanded;
            HasDefinedExpanded = true;
        }

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            var attr = other as FoldoutGroupAttribute;
            if (attr.HasDefinedExpanded)
            {
                HasDefinedExpanded = true;
                Expanded = attr.Expanded;
            }

            if (HasDefinedExpanded)
            {
                attr.HasDefinedExpanded = true;
                attr.Expanded = Expanded;
            }
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;
    using UnityEngine;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class GUIColorAttribute : Attribute
    {
        public Color Color;
        public string GetColor;

        public GUIColorAttribute(float r, float g, float b, float a = 1f)
        {
            Color = new Color(r, g, b, a);
        }

        public GUIColorAttribute(string getColor)
        {
            GetColor = getColor;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [Conditional("UNITY_EDITOR")]
    public class HideDuplicateReferenceBoxAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [DontApplyToListElements]
    [Conditional("UNITY_EDITOR")]
    public sealed class HideIfAttribute : Attribute
    {
        [Obsolete("Use the Condition member instead.", false)]
        public string MemberName
        {
            get => Condition;
            set => Condition = value;
        }

        public string Condition;
        public object Value;
        public bool Animate;

        public HideIfAttribute(string condition, bool animate = true)
        {
            Condition = condition;
            Animate = animate;
        }

        public HideIfAttribute(string condition, object optionalValue, bool animate = true)
        {
            Condition = condition;
            Value = optionalValue;
            Animate = animate;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [Conditional("UNITY_EDITOR")]
    public class HideIfGroupAttribute : PropertyGroupAttribute
    {
        public bool Animate
        {
            get => AnimateVisibility;
            set => AnimateVisibility = value;
        }

        public object Value;

        [Obsolete("Use the Condition member instead.", false)]
        public string MemberName
        {
            get => Condition;
            set => Condition = value;
        }

        public string Condition
        {
            get => string.IsNullOrEmpty(VisibleIf) ? GroupName : VisibleIf;
            set => VisibleIf = value;
        }

        public HideIfGroupAttribute(string path, bool animate = true) : base(path)
        {
            Animate = animate;
        }

        public HideIfGroupAttribute(string path, object value, bool animate = true) : base(path)
        {
            Value = value;
            Animate = animate;
        }

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            var attr = other as HideIfGroupAttribute;
            if (Value != null)
            {
                attr.Value = Value;
            }
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class HideInEditorModeAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class HideInInlineEditorsAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class HideInNonPrefabsAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [DontApplyToListElements]
    [Conditional("UNITY_EDITOR")]
    public class HideInPlayModeAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class HideInPrefabAssetsAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class HideInPrefabInstancesAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class HideInPrefabsAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class HideInTablesAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargetFlags.Default)]
    [Conditional("UNITY_EDITOR")]
    public class HideLabelAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    [Conditional("UNITY_EDITOR")]
    public sealed class HideMonoScriptAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    [Conditional("UNITY_EDITOR")]
    public sealed class HideNetworkBehaviourFieldsAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class HideReferenceObjectPickerAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class HorizontalGroupAttribute : PropertyGroupAttribute
    {
        public float Width;
        public float MarginLeft;
        public float MarginRight;
        public float PaddingLeft;
        public float PaddingRight;
        public float MinWidth;
        public float MaxWidth;
        public string Title;
        public float LabelWidth;

        public HorizontalGroupAttribute(string group, float width = 0, int marginLeft = 0, int marginRight = 0, float order = 0) : base(group, order)
        {
            Width = width;
            MarginLeft = marginLeft;
            MarginRight = marginRight;
        }

        public HorizontalGroupAttribute(float width = 0, int marginLeft = 0, int marginRight = 0, float order = 0) : this("_DefaultHorizontalGroup", width, marginLeft, marginRight, order)
        {
        }

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            Title = Title ?? (other as HorizontalGroupAttribute).Title;
            LabelWidth = Math.Max(LabelWidth, (other as HorizontalGroupAttribute).LabelWidth);
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class IndentAttribute : Attribute
    {
        public int IndentLevel;

        public IndentAttribute(int indentLevel = 1)
        {
            IndentLevel = indentLevel;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class InfoBoxAttribute : Attribute
    {
        public string Message;
        public InfoMessageType InfoMessageType;
        public string VisibleIf;
        public bool GUIAlwaysEnabled;

        public InfoBoxAttribute(string message, InfoMessageType infoMessageType = InfoMessageType.Info, string visibleIfMemberName = null)
        {
            Message = message;
            InfoMessageType = infoMessageType;
            VisibleIf = visibleIfMemberName;
        }

        public InfoBoxAttribute(string message, string visibleIfMemberName)
        {
            Message = message;
            InfoMessageType = InfoMessageType.Info;
            VisibleIf = visibleIfMemberName;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class InlineButtonAttribute : Attribute
    {
        [Obsolete("Use the Action member instead.", false)]
        public string MemberMethod
        {
            get => Action;
            set => Action = value;
        }

        public string Action;
        public string Label;
        public string ShowIf;

        public InlineButtonAttribute(string action, string label = null)
        {
            Action = action;
            Label = label;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class InlineEditorAttribute : Attribute
    {
        private bool expanded;

        public bool Expanded
        {
            get => expanded;
            set
            {
                expanded = value;
                ExpandedHasValue = true;
            }
        }

        public bool DrawHeader;
        public bool DrawGUI;
        public bool DrawPreview;
        public float MaxHeight;
        public float PreviewWidth = 100;
        public float PreviewHeight = 35;
        public bool IncrementInlineEditorDrawerDepth = true;
        public InlineEditorObjectFieldModes ObjectFieldMode;
        public bool DisableGUIForVCSLockedAssets = true;
        public bool ExpandedHasValue { get; private set; }

        public InlineEditorAttribute(InlineEditorModes inlineEditorMode = InlineEditorModes.GUIOnly, InlineEditorObjectFieldModes objectFieldMode = InlineEditorObjectFieldModes.Boxed)
        {
            ObjectFieldMode = objectFieldMode;
            switch (inlineEditorMode)
            {
                case InlineEditorModes.GUIOnly: DrawGUI = true; break;
                case InlineEditorModes.GUIAndHeader:
                    DrawGUI = true;
                    DrawHeader = true;
                    break;
                case InlineEditorModes.GUIAndPreview:
                    DrawGUI = true;
                    DrawPreview = true;
                    break;
                case InlineEditorModes.SmallPreview:
                    expanded = true;
                    DrawPreview = true;
                    break;
                case InlineEditorModes.LargePreview:
                    expanded = true;
                    DrawPreview = true;
                    PreviewHeight = 170;
                    break;
                case InlineEditorModes.FullEditor:
                    DrawGUI = true;
                    DrawHeader = true;
                    DrawPreview = true;
                    break;
                default: throw new NotImplementedException();
            }
        }

        public InlineEditorAttribute(InlineEditorObjectFieldModes objectFieldMode) : this(InlineEditorModes.GUIOnly, objectFieldMode)
        {
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    [Conditional("UNITY_EDITOR")]
    public class InlinePropertyAttribute : Attribute
    {
        public int LabelWidth;
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class LabelTextAttribute : Attribute
    {
        public string Text;
        public bool NicifyText;

        public LabelTextAttribute(string text)
        {
            Text = text;
        }

        public LabelTextAttribute(string text, bool nicifyText)
        {
            Text = text;
            NicifyText = nicifyText;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class LabelWidthAttribute : Attribute
    {
        public float Width;

        public LabelWidthAttribute(float width)
        {
            Width = width;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    [DontApplyToListElements]
    public sealed class ListDrawerSettingsAttribute : Attribute
    {
        public bool HideAddButton;
        public bool HideRemoveButton;
        public string ListElementLabelName;
        public string CustomAddFunction;
        public string CustomRemoveIndexFunction;
        public string CustomRemoveElementFunction;
        public string OnBeginListElementGUI;
        public string OnEndListElementGUI;
        public bool AlwaysAddDefaultValue;
        public bool AddCopiesLastElement = false;
        public string ElementColor;
        private int numberOfItemsPerPage;
        private bool paging;
        private bool draggable;
        private bool isReadOnly;
        private bool showItemCount;
        private bool expanded;
        private bool showIndexLabels;

        public bool ShowPaging
        {
            get => paging;
            set
            {
                paging = value;
                PagingHasValue = true;
            }
        }

        public bool DraggableItems
        {
            get => draggable;
            set
            {
                draggable = value;
                DraggableHasValue = true;
            }
        }

        public int NumberOfItemsPerPage
        {
            get => numberOfItemsPerPage;
            set
            {
                numberOfItemsPerPage = value;
                NumberOfItemsPerPageHasValue = true;
            }
        }

        public bool IsReadOnly
        {
            get => isReadOnly;
            set
            {
                isReadOnly = value;
                IsReadOnlyHasValue = true;
            }
        }

        public bool ShowItemCount
        {
            get => showItemCount;
            set
            {
                showItemCount = value;
                ShowItemCountHasValue = true;
            }
        }

        public bool Expanded
        {
            get => expanded;
            set
            {
                expanded = value;
                ExpandedHasValue = true;
            }
        }

        public bool ShowIndexLabels
        {
            get => showIndexLabels;
            set
            {
                showIndexLabels = value;
                ShowIndexLabelsHasValue = true;
            }
        }

        public string OnTitleBarGUI { get; set; }

        public bool PagingHasValue { get; private set; }

        public bool ShowItemCountHasValue { get; private set; }

        public bool NumberOfItemsPerPageHasValue { get; private set; }

        public bool DraggableHasValue { get; private set; }

        public bool IsReadOnlyHasValue { get; private set; }

        public bool ExpandedHasValue { get; private set; }

        public bool ShowIndexLabelsHasValue { get; private set; }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class MaxValueAttribute : Attribute
    {
        public double MaxValue;
        public string Expression;

        public MaxValueAttribute(double maxValue)
        {
            MaxValue = maxValue;
        }

        public MaxValueAttribute(string expression)
        {
            Expression = expression;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class MinMaxSliderAttribute : Attribute
    {
        public float MinValue;
        public float MaxValue;

        [Obsolete("Use the MinValueGetter member instead.", false)]
        public string MinMember
        {
            get => MinValueGetter;
            set => MinValueGetter = value;
        }

        public string MinValueGetter;

        [Obsolete("Use the MaxValueGetter member instead.", false)]
        public string MaxMember
        {
            get => MaxValueGetter;
            set => MaxValueGetter = value;
        }

        public string MaxValueGetter;

        [Obsolete("Use the MinMaxValueGetter member instead.", false)]
        public string MinMaxMember
        {
            get => MinMaxValueGetter;
            set => MinMaxValueGetter = value;
        }

        public string MinMaxValueGetter;
        public bool ShowFields;

        public MinMaxSliderAttribute(float minValue, float maxValue, bool showFields = false)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            ShowFields = showFields;
        }

        public MinMaxSliderAttribute(string minValueGetter, float maxValue, bool showFields = false)
        {
            MinValueGetter = minValueGetter;
            MaxValue = maxValue;
            ShowFields = showFields;
        }

        public MinMaxSliderAttribute(float minValue, string maxValueGetter, bool showFields = false)
        {
            MinValue = minValue;
            MaxValueGetter = maxValueGetter;
            ShowFields = showFields;
        }

        public MinMaxSliderAttribute(string minValueGetter, string maxValueGetter, bool showFields = false)
        {
            MinValueGetter = minValueGetter;
            MaxValueGetter = maxValueGetter;
            ShowFields = showFields;
        }

        public MinMaxSliderAttribute(string minMaxValueGetter, bool showFields = false)
        {
            MinMaxValueGetter = minMaxValueGetter;
            ShowFields = showFields;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class MinValueAttribute : Attribute
    {
        public double MinValue;
        public string Expression;

        public MinValueAttribute(double minValue)
        {
            MinValue = minValue;
        }

        public MinValueAttribute(string expression)
        {
            Expression = expression;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class MultiLinePropertyAttribute : Attribute
    {
        public int Lines;

        public MultiLinePropertyAttribute(int lines = 3)
        {
            Lines = Math.Max(1, lines);
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class OnCollectionChangedAttribute : Attribute
    {
        public string Before;
        public string After;

        public OnCollectionChangedAttribute()
        {
        }

        public OnCollectionChangedAttribute(string after)
        {
            After = after;
        }

        public OnCollectionChangedAttribute(string before, string after)
        {
            Before = before;
            After = after;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    [Conditional("UNITY_EDITOR")]
    [DontApplyToListElements]
    [IncludeMyAttributes]
    [HideInTables]
    public class OnInspectorDisposeAttribute : ShowInInspectorAttribute
    {
        public string Action;

        public OnInspectorDisposeAttribute()
        {
        }

        public OnInspectorDisposeAttribute(string action)
        {
            Action = action;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class OnInspectorGUIAttribute : ShowInInspectorAttribute
    {
        public string Prepend;
        public string Append;

        [Obsolete("Use the Prepend member instead.", false)]
        public string PrependMethodName;

        [Obsolete("Use the Append member instead.", false)]
        public string AppendMethodName;

        public OnInspectorGUIAttribute()
        {
        }

        public OnInspectorGUIAttribute(string action, bool append = true)
        {
            if (append)
            {
                Append = action;
            }
            else
            {
                Prepend = action;
            }
        }

        public OnInspectorGUIAttribute(string prepend, string append)
        {
            Prepend = prepend;
            Append = append;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    [Conditional("UNITY_EDITOR")]
    [DontApplyToListElements]
    [IncludeMyAttributes]
    [HideInTables]
    public class OnInspectorInitAttribute : ShowInInspectorAttribute
    {
        public string Action;

        public OnInspectorInitAttribute()
        {
        }

        public OnInspectorInitAttribute(string action)
        {
            Action = action;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    [IncludeMyAttributes]
    [HideInTables]
    public sealed class OnStateUpdateAttribute : Attribute
    {
        public string Action;

        public OnStateUpdateAttribute(string action)
        {
            Action = action;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class OnValueChangedAttribute : Attribute
    {
        [Obsolete("Use the Action member instead.", false)]
        public string MethodName
        {
            get => Action;
            set => Action = value;
        }

        public string Action;
        public bool IncludeChildren;
        public bool InvokeOnUndoRedo = true;
        public bool InvokeOnInitialize = false;

        public OnValueChangedAttribute(string action, bool includeChildren = false)
        {
            Action = action;
            IncludeChildren = includeChildren;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class PreviewFieldAttribute : Attribute
    {
        private ObjectFieldAlignment alignment;
        public float Height;

        public ObjectFieldAlignment Alignment
        {
            get => alignment;
            set
            {
                alignment = value;
                AlignmentHasValue = true;
            }
        }

        public bool AlignmentHasValue { get; private set; }

        public PreviewFieldAttribute()
        {
            Height = 0;
        }

        public PreviewFieldAttribute(float height)
        {
            Height = height;
        }

        public PreviewFieldAttribute(float height, ObjectFieldAlignment alignment)
        {
            Height = height;
            Alignment = alignment;
        }

        public PreviewFieldAttribute(ObjectFieldAlignment alignment)
        {
            Alignment = alignment;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;
    using UnityEngine;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class ProgressBarAttribute : Attribute
    {
        public double Min;
        public double Max;

        [Obsolete("Use the MinGetter member instead.", false)]
        public string MinMember
        {
            get => MinGetter;
            set => MinGetter = value;
        }

        public string MinGetter;

        [Obsolete("Use the MaxGetter member instead.", false)]
        public string MaxMember
        {
            get => MaxGetter;
            set => MaxGetter = value;
        }

        public string MaxGetter;
        public float R;
        public float G;
        public float B;
        public int Height;

        [Obsolete("Use the ColorGetter member instead.", false)]
        public string ColorMember
        {
            get => ColorGetter;
            set => ColorGetter = value;
        }

        public string ColorGetter;

        [Obsolete("Use the BackgroundColorGetter member instead.", false)]
        public string BackgroundColorMember
        {
            get => BackgroundColorGetter;
            set => BackgroundColorGetter = value;
        }

        public string BackgroundColorGetter;
        public bool Segmented;

        [Obsolete("Use the CustomValueStringGetter member instead.", false)]
        public string CustomValueStringMember
        {
            get => CustomValueStringGetter;
            set => CustomValueStringGetter = value;
        }

        public string CustomValueStringGetter;
        private bool drawValueLabel;
        private TextAlignment valueLabelAlignment;

        public ProgressBarAttribute(double min, double max, float r = 0.15f, float g = 0.47f, float b = 0.74f)
        {
            Min = min;
            Max = max;
            R = r;
            G = g;
            B = b;
            Height = 12;
            Segmented = false;
            drawValueLabel = true;
            DrawValueLabelHasValue = false;
            valueLabelAlignment = TextAlignment.Center;
            ValueLabelAlignmentHasValue = false;
        }

        public ProgressBarAttribute(string minGetter, double max, float r = 0.15f, float g = 0.47f, float b = 0.74f)
        {
            MinGetter = minGetter;
            Max = max;
            R = r;
            G = g;
            B = b;
            Height = 12;
            Segmented = false;
            drawValueLabel = true;
            DrawValueLabelHasValue = false;
            valueLabelAlignment = TextAlignment.Center;
            ValueLabelAlignmentHasValue = false;
        }

        public ProgressBarAttribute(double min, string maxGetter, float r = 0.15f, float g = 0.47f, float b = 0.74f)
        {
            Min = min;
            MaxGetter = maxGetter;
            R = r;
            G = g;
            B = b;
            Height = 12;
            Segmented = false;
            drawValueLabel = true;
            DrawValueLabelHasValue = false;
            valueLabelAlignment = TextAlignment.Center;
            ValueLabelAlignmentHasValue = false;
        }

        public ProgressBarAttribute(string minGetter, string maxGetter, float r = 0.15f, float g = 0.47f, float b = 0.74f)
        {
            MinGetter = minGetter;
            MaxGetter = maxGetter;
            R = r;
            G = g;
            B = b;
            Height = 12;
            Segmented = false;
            drawValueLabel = true;
            DrawValueLabelHasValue = false;
            valueLabelAlignment = TextAlignment.Center;
            ValueLabelAlignmentHasValue = false;
        }

        public bool DrawValueLabel
        {
            get => drawValueLabel;
            set
            {
                drawValueLabel = value;
                DrawValueLabelHasValue = true;
            }
        }

        public bool DrawValueLabelHasValue { get; private set; }

        public TextAlignment ValueLabelAlignment
        {
            get => valueLabelAlignment;
            set
            {
                valueLabelAlignment = value;
                ValueLabelAlignmentHasValue = true;
            }
        }

        public bool ValueLabelAlignmentHasValue { get; private set; }
        public Color Color => new(R, G, B, 1f);
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public abstract class PropertyGroupAttribute : Attribute
    {
        public string GroupID;
        public string GroupName;
        public float Order;
        public bool HideWhenChildrenAreInvisible = true;
        public string VisibleIf;
        public bool AnimateVisibility = true;

        public PropertyGroupAttribute(string groupId, float order)
        {
            GroupID = groupId;
            Order = order;
            var index = groupId.LastIndexOf('/');
            GroupName = index >= 0 && index < groupId.Length ? groupId.Substring(index + 1) : groupId;
        }

        public PropertyGroupAttribute(string groupId) : this(groupId, 0)
        {
        }

        public PropertyGroupAttribute Combine(PropertyGroupAttribute other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (other.GetType() != GetType())
            {
                throw new ArgumentException("Attributes to combine are not of the same type.");
            }

            if (other.GroupID != GroupID)
            {
                throw new ArgumentException("PropertyGroupAttributes to combine must have the same group id.");
            }

            if (Order == 0)
            {
                Order = other.Order;
            }
            else if (other.Order != 0)
            {
                Order = Math.Min(Order, other.Order);
            }

            HideWhenChildrenAreInvisible &= other.HideWhenChildrenAreInvisible;
            VisibleIf ??= other.VisibleIf;

            AnimateVisibility &= other.AnimateVisibility;
            CombineValuesWith(other);
            return this;
        }

        protected virtual void CombineValuesWith(PropertyGroupAttribute other)
        {
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class PropertyOrderAttribute : Attribute
    {
        public float Order;

        public PropertyOrderAttribute()
        {
        }

        public PropertyOrderAttribute(float order)
        {
            Order = order;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class PropertyRangeAttribute : Attribute
    {
        public double Min;
        public double Max;

        [Obsolete("Use the MinGetter member instead.", false)]
        public string MinMember
        {
            get => MinGetter;
            set => MinGetter = value;
        }

        public string MinGetter;

        [Obsolete("Use the MaxGetter member instead.", false)]
        public string MaxMember
        {
            get => MaxGetter;
            set => MaxGetter = value;
        }

        public string MaxGetter;

        public PropertyRangeAttribute(double min, double max)
        {
            Min = min < max ? min : max;
            Max = max > min ? max : min;
        }

        public PropertyRangeAttribute(string minGetter, double max)
        {
            MinGetter = minGetter;
            Max = max;
        }

        public PropertyRangeAttribute(double min, string maxGetter)
        {
            Min = min;
            MaxGetter = maxGetter;
        }

        public PropertyRangeAttribute(string minGetter, string maxGetter)
        {
            MinGetter = minGetter;
            MaxGetter = maxGetter;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [DontApplyToListElements]
    [Conditional("UNITY_EDITOR")]
    public class PropertySpaceAttribute : Attribute
    {
        public float SpaceBefore;
        public float SpaceAfter;

        public PropertySpaceAttribute()
        {
            SpaceBefore = 8f;
            SpaceAfter = 0f;
        }

        public PropertySpaceAttribute(float spaceBefore)
        {
            SpaceBefore = spaceBefore;
            SpaceAfter = 0f;
        }

        public PropertySpaceAttribute(float spaceBefore, float spaceAfter)
        {
            SpaceBefore = spaceBefore;
            SpaceAfter = spaceAfter;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class PropertyTooltipAttribute : Attribute
    {
        public string Tooltip;

        public PropertyTooltipAttribute(string tooltip)
        {
            Tooltip = tooltip;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class ReadOnlyAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class RequiredAttribute : Attribute
    {
        public string ErrorMessage;
        public InfoMessageType MessageType;

        public RequiredAttribute()
        {
            MessageType = InfoMessageType.Error;
        }

        public RequiredAttribute(string errorMessage, InfoMessageType messageType)
        {
            ErrorMessage = errorMessage;
            MessageType = messageType;
        }

        public RequiredAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
            MessageType = InfoMessageType.Error;
        }

        public RequiredAttribute(InfoMessageType messageType)
        {
            MessageType = messageType;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [IncludeMyAttributes]
    [ShowInInspector]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class ResponsiveButtonGroupAttribute : PropertyGroupAttribute
    {
        public ButtonSizes DefaultButtonSize = ButtonSizes.Medium;
        public bool UniformLayout;

        public ResponsiveButtonGroupAttribute(string group = "_DefaultResponsiveButtonGroup") : base(group)
        {
        }

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            var otherAttr = other as ResponsiveButtonGroupAttribute;
            if (other == null)
            {
                return;
            }

            if (otherAttr.DefaultButtonSize != ButtonSizes.Medium)
            {
                DefaultButtonSize = otherAttr.DefaultButtonSize;
            }
            else if (DefaultButtonSize != ButtonSizes.Medium)
            {
                otherAttr.DefaultButtonSize = DefaultButtonSize;
            }

            UniformLayout = UniformLayout || otherAttr.UniformLayout;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class SceneObjectsOnlyAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [Conditional("UNITY_EDITOR")]
    [DontApplyToListElements]
    public class SearchableAttribute : Attribute
    {
        public bool FuzzySearch = true;
        public SearchFilterOptions FilterOptions = SearchFilterOptions.All;
        public bool Recursive = true;
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class ShowDrawerChainAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [Obsolete("Use HideInPrefabInstance or HideInPrefabAsset instead.", false)]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class ShowForPrefabOnlyAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class ShowIfAttribute : Attribute
    {
        [Obsolete("Use the Condition member instead.", false)]
        public string MemberName
        {
            get => Condition;
            set => Condition = value;
        }

        public string Condition;
        public object Value;
        public bool Animate;

        public ShowIfAttribute(string condition, bool animate = true)
        {
            Condition = condition;
            Animate = animate;
        }

        public ShowIfAttribute(string condition, object optionalValue, bool animate = true)
        {
            Condition = condition;
            Value = optionalValue;
            Animate = animate;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [Conditional("UNITY_EDITOR")]
    public class ShowIfGroupAttribute : PropertyGroupAttribute
    {
        public bool Animate
        {
            get => AnimateVisibility;
            set => AnimateVisibility = value;
        }

        public object Value;

        [Obsolete("Use the Condition member instead.", false)]
        public string MemberName
        {
            get => Condition;
            set => Condition = value;
        }

        public string Condition
        {
            get => string.IsNullOrEmpty(VisibleIf) ? GroupName : VisibleIf;
            set => VisibleIf = value;
        }

        public ShowIfGroupAttribute(string path, bool animate = true) : base(path)
        {
            Animate = animate;
        }

        public ShowIfGroupAttribute(string path, object value, bool animate = true) : base(path)
        {
            Value = value;
            Animate = animate;
        }

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            var attr = other as ShowIfGroupAttribute;
            if (Value != null)
            {
                attr.Value = Value;
            }
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class ShowInInlineEditorsAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    [Conditional("UNITY_EDITOR")]
    public class ShowInInspectorAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    [Conditional("UNITY_EDITOR")]
    public class ShowOdinSerializedPropertiesInInspectorAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [Conditional("UNITY_EDITOR")]
    public class ShowPropertyResolverAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    [Conditional("UNITY_EDITOR")]
    public sealed class SuffixLabelAttribute : Attribute
    {
        public string Label;
        public bool Overlay;

        public SuffixLabelAttribute(string label, bool overlay = false)
        {
            Label = label;
            Overlay = overlay;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class SuppressInvalidAttributeErrorAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;
    using System.Collections.Generic;
    using Internal;

    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class TabGroupAttribute : PropertyGroupAttribute, ISubGroupProviderAttribute
    {
        public const string DEFAULT_NAME = "_DefaultTabGroup";
        public string TabName;
        public bool UseFixedHeight;
        public bool Paddingless;
        public bool HideTabGroupIfTabGroupOnlyHasOneTab;

        public TabGroupAttribute(string tab, bool useFixedHeight = false, float order = 0) : this(DEFAULT_NAME, tab, useFixedHeight, order)
        {
        }

        public TabGroupAttribute(string group, string tab, bool useFixedHeight = false, float order = 0) : base(group, order)
        {
            TabName = tab;
            UseFixedHeight = useFixedHeight;
            Tabs = new List<string>();
            if (tab != null)
            {
                Tabs.Add(tab);
            }

            Tabs = new List<string>(Tabs);
        }

        public List<string> Tabs { get; }

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            base.CombineValuesWith(other);
            var otherTab = other as TabGroupAttribute;
            if (otherTab.TabName != null)
            {
                UseFixedHeight = UseFixedHeight || otherTab.UseFixedHeight;
                Paddingless = Paddingless || otherTab.Paddingless;
                HideTabGroupIfTabGroupOnlyHasOneTab = HideTabGroupIfTabGroupOnlyHasOneTab || otherTab.HideTabGroupIfTabGroupOnlyHasOneTab;
                if (!Tabs.Contains(otherTab.TabName))
                {
                    Tabs.Add(otherTab.TabName);
                }
            }
        }

        IList<PropertyGroupAttribute> ISubGroupProviderAttribute.GetSubGroupAttributes()
        {
            var count = 0;
            var result = new List<PropertyGroupAttribute>(Tabs.Count);
            foreach (var tab in Tabs) result.Add(new TabSubGroupAttribute(GroupID + "/" + tab, count++));
            return result;
        }

        string ISubGroupProviderAttribute.RepathMemberAttribute(PropertyGroupAttribute attr)
        {
            var tabAttr = (TabGroupAttribute)attr;
            return GroupID + "/" + tabAttr.TabName;
        }

        [Conditional("UNITY_EDITOR")]
        public class TabSubGroupAttribute : PropertyGroupAttribute
        {
            public TabSubGroupAttribute(string groupId, float order) : base(groupId, order)
            {
            }
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class TableColumnWidthAttribute : Attribute
    {
        public int Width;
        public bool Resizable;

        public TableColumnWidthAttribute(int width, bool resizable = true)
        {
            Width = width;
            Resizable = resizable;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;
    using UnityEngine;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class TableListAttribute : Attribute
    {
        public int NumberOfItemsPerPage;
        public bool IsReadOnly;
        public int DefaultMinColumnWidth = 40;
        public bool ShowIndexLabels;
        public bool DrawScrollView = true;
        public int MinScrollViewHeight = 350;
        public int MaxScrollViewHeight;
        public bool AlwaysExpanded;
        public bool HideToolbar = false;
        public int CellPadding = 2;

        [SerializeField]
        [HideInInspector]
        private bool showPagingHasValue;

        [SerializeField]
        [HideInInspector]
        private bool showPaging;

        public bool ShowPaging
        {
            get => showPaging;
            set
            {
                showPaging = value;
                showPagingHasValue = true;
            }
        }

        public bool ShowPagingHasValue => showPagingHasValue;

        public int ScrollViewHeight
        {
            get => Math.Min(MinScrollViewHeight, MaxScrollViewHeight);
            set => MinScrollViewHeight = MaxScrollViewHeight = value;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class TableMatrixAttribute : Attribute
    {
        public bool IsReadOnly = false;
        public bool ResizableColumns = true;
        public string VerticalTitle = null;
        public string HorizontalTitle = null;
        public string DrawElementMethod = null;
        public int RowHeight = 0;
        public bool SquareCells = false;
        public bool HideColumnIndices = false;
        public bool HideRowIndices = false;
        public bool RespectIndentLevel = true;
        public bool Transpose = false;
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class TitleAttribute : Attribute
    {
        public string Title;
        public string Subtitle;
        public bool Bold;
        public bool HorizontalLine;
        public TitleAlignments TitleAlignment;

        public TitleAttribute(string title, string subtitle = null, TitleAlignments titleAlignment = TitleAlignments.Left, bool horizontalLine = true, bool bold = true)
        {
            Title = title ?? "null";
            Subtitle = subtitle;
            Bold = bold;
            TitleAlignment = titleAlignment;
            HorizontalLine = horizontalLine;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class TitleGroupAttribute : PropertyGroupAttribute
    {
        public string Subtitle;
        public TitleAlignments Alignment;
        public bool HorizontalLine;
        public bool BoldTitle;
        public bool Indent;

        public TitleGroupAttribute(string title, string subtitle = null, TitleAlignments alignment = TitleAlignments.Left, bool horizontalLine = true, bool boldTitle = true, bool indent = false, float order = 0) : base(title, order)
        {
            Subtitle = subtitle;
            Alignment = alignment;
            HorizontalLine = horizontalLine;
            BoldTitle = boldTitle;
            Indent = indent;
        }

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            var t = other as TitleGroupAttribute;
            if (Subtitle != null)
            {
                t.Subtitle = Subtitle;
            }
            else
            {
                Subtitle = t.Subtitle;
            }

            if (Alignment != TitleAlignments.Left)
            {
                t.Alignment = Alignment;
            }
            else
            {
                Alignment = t.Alignment;
            }

            if (!HorizontalLine)
            {
                t.HorizontalLine = HorizontalLine;
            }
            else
            {
                HorizontalLine = t.HorizontalLine;
            }

            if (!BoldTitle)
            {
                t.BoldTitle = BoldTitle;
            }
            else
            {
                BoldTitle = t.BoldTitle;
            }

            if (Indent == true)
            {
                t.Indent = Indent;
            }
            else
            {
                Indent = t.Indent;
            }
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class ToggleAttribute : Attribute
    {
        public string ToggleMemberName;
        public bool CollapseOthersOnExpand;

        public ToggleAttribute(string toggleMemberName)
        {
            ToggleMemberName = toggleMemberName;
            CollapseOthersOnExpand = true;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class ToggleGroupAttribute : PropertyGroupAttribute
    {
        public string ToggleGroupTitle;
        public bool CollapseOthersOnExpand;

        public ToggleGroupAttribute(string toggleMemberName, float order = 0, string groupTitle = null) : base(toggleMemberName, order)
        {
            ToggleGroupTitle = groupTitle;
            CollapseOthersOnExpand = true;
        }

        public ToggleGroupAttribute(string toggleMemberName, string groupTitle) : this(toggleMemberName, 0, groupTitle)
        {
        }

        [Obsolete("Use [ToggleGroup(\"toggleMemberName\", groupTitle: \"$titleStringMemberName\")] instead")]
        public ToggleGroupAttribute(string toggleMemberName, float order, string groupTitle, string titleStringMemberName) : base(toggleMemberName, order)
        {
            ToggleGroupTitle = groupTitle;
            CollapseOthersOnExpand = true;
        }

        public string ToggleMemberName => GroupName;

        [Obsolete("Add a $ infront of group title instead, i.e: \"$MyStringMember\".")]
        public string TitleStringMemberName { get; set; }

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            var attr = other as ToggleGroupAttribute;
            if (ToggleGroupTitle == null)
            {
                ToggleGroupTitle = attr.ToggleGroupTitle;
            }
            else if (attr.ToggleGroupTitle == null)
            {
                attr.ToggleGroupTitle = ToggleGroupTitle;
            }

            CollapseOthersOnExpand = CollapseOthersOnExpand || attr.CollapseOthersOnExpand;
            attr.CollapseOthersOnExpand = CollapseOthersOnExpand;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class ToggleLeftAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class TypeFilterAttribute : Attribute
    {
        [Obsolete("Use the FilterGetter member instead.", false)]
        public string MemberName
        {
            get => FilterGetter;
            set => FilterGetter = value;
        }

        public string FilterGetter;
        public string DropdownTitle;
        public bool DrawValueNormally;

        public TypeFilterAttribute(string filterGetter)
        {
            FilterGetter = filterGetter;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class TypeInfoBoxAttribute : Attribute
    {
        public string Message;

        public TypeInfoBoxAttribute(string message)
        {
            Message = message;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class ValidateInputAttribute : Attribute
    {
        public string DefaultMessage;

        [Obsolete("Use the Condition member instead.", false)]
        public string MemberName
        {
            get => Condition;
            set => Condition = value;
        }

        public string Condition;
        public InfoMessageType MessageType;
        public bool IncludeChildren;

        [Obsolete("Use the ContinuousValidationCheck member instead.")]
        public bool ContiniousValidationCheck
        {
            get => ContinuousValidationCheck;
            set => ContinuousValidationCheck = value;
        }

        public bool ContinuousValidationCheck;

        public ValidateInputAttribute(string condition, string defaultMessage = null, InfoMessageType messageType = InfoMessageType.Error)
        {
            Condition = condition;
            DefaultMessage = defaultMessage;
            MessageType = messageType;
            IncludeChildren = true;
        }

        [Obsolete("Rejecting invalid input is no longer supported. Use the other constructor instead.", true)]
        public ValidateInputAttribute(string condition, string message, InfoMessageType messageType, bool rejectedInvalidInput)
        {
            Condition = condition;
            DefaultMessage = message;
            MessageType = messageType;
            IncludeChildren = true;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;
    using System.Collections.Generic;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class ValueDropdownAttribute : Attribute
    {
        [Obsolete("Use the ValuesGetter member instead.", false)]
        public string MemberName
        {
            get => ValuesGetter;
            set => ValuesGetter = value;
        }

        public string ValuesGetter;
        public int NumberOfItemsBeforeEnablingSearch;
        public bool IsUniqueList;
        public bool DrawDropdownForListElements;
        public bool DisableListAddButtonBehaviour;
        public bool ExcludeExistingValuesInList;
        public bool ExpandAllMenuItems;
        public bool AppendNextDrawer;
        public bool DisableGUIInAppendedDrawer;
        public bool DoubleClickToConfirm;
        public bool FlattenTreeView;
        public int DropdownWidth;
        public int DropdownHeight;
        public string DropdownTitle;
        public bool SortDropdownItems;
        public bool HideChildProperties = false;
        public bool CopyValues = true;

        public ValueDropdownAttribute(string valuesGetter)
        {
            NumberOfItemsBeforeEnablingSearch = 10;
            ValuesGetter = valuesGetter;
            DrawDropdownForListElements = true;
        }
    }

    public interface IValueDropdownItem
    {
        string GetText();
        object GetValue();
    }

    public class ValueDropdownList<T> : List<ValueDropdownItem<T>>
    {
        public void Add(string text, T value)
        {
            Add(new ValueDropdownItem<T>(text, value));
        }

        public void Add(T value)
        {
            Add(new ValueDropdownItem<T>(value.ToString(), value));
        }
    }

    public struct ValueDropdownItem : IValueDropdownItem
    {
        public string Text;
        public object Value;

        public ValueDropdownItem(string text, object value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text ?? Value + "";
        }

        string IValueDropdownItem.GetText()
        {
            return Text;
        }

        object IValueDropdownItem.GetValue()
        {
            return Value;
        }
    }

    public struct ValueDropdownItem<T> : IValueDropdownItem
    {
        public string Text;
        public T Value;

        public ValueDropdownItem(string text, T value)
        {
            Text = text;
            Value = value;
        }

        string IValueDropdownItem.GetText()
        {
            return Text;
        }

        object IValueDropdownItem.GetValue()
        {
            return Value;
        }

        public override string ToString()
        {
            return Text ?? Value + "";
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class VerticalGroupAttribute : PropertyGroupAttribute
    {
        public float PaddingTop;
        public float PaddingBottom;

        public VerticalGroupAttribute(string groupId, float order = 0) : base(groupId, order)
        {
        }

        public VerticalGroupAttribute(float order = 0) : this("_DefaultVerticalGroup", order)
        {
        }

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            if (other is VerticalGroupAttribute a)
            {
                if (a.PaddingTop != 0)
                {
                    PaddingTop = a.PaddingTop;
                }

                if (a.PaddingBottom != 0)
                {
                    PaddingBottom = a.PaddingBottom;
                }
            }
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class WrapAttribute : Attribute
    {
        public double Min;
        public double Max;

        public WrapAttribute(double min, double max)
        {
            Min = min < max ? min : max;
            Max = max > min ? max : min;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    public static class AttributeTargetFlags
    {
        public const AttributeTargets Default = AttributeTargets.All;
    }
}

namespace Sirenix.OdinInspector
{
    public enum ButtonSizes
    {
        Small = 0,
        Medium = 22,
        Large = 31,
        Gigantic = 62
    }
}

namespace Sirenix.OdinInspector
{
    public enum DictionaryDisplayOptions
    {
        OneLine,
        Foldout,
        CollapsedFoldout,
        ExpandedFoldout
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class IncludeMyAttributesAttribute : Attribute
    {
    }
}

namespace Sirenix.OdinInspector
{
    public enum InfoMessageType
    {
        None,
        Info,
        Warning,
        Error
    }
}

namespace Sirenix.OdinInspector
{
    public enum InlineEditorModes
    {
        GUIOnly = 0,
        GUIAndHeader = 1,
        GUIAndPreview = 2,
        SmallPreview = 3,
        LargePreview = 4,
        FullEditor = 5
    }
}

namespace Sirenix.OdinInspector
{
    public enum InlineEditorObjectFieldModes
    {
        Boxed,
        Foldout,
        Hidden,
        CompletelyHidden
    }
}

namespace Sirenix.OdinInspector
{
    public interface ISearchFilterable
    {
        bool IsMatch(string searchString);
    }
}

namespace Sirenix.OdinInspector.Internal
{
    using System.Collections.Generic;

    public interface ISubGroupProviderAttribute
    {
        IList<PropertyGroupAttribute> GetSubGroupAttributes();
        string RepathMemberAttribute(PropertyGroupAttribute attr);
    }
}

namespace Sirenix.OdinInspector
{
    public enum ObjectFieldAlignment
    {
        Left = 0,
        Center = 1,
        Right = 2
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class OdinRegisterAttributeAttribute : Attribute
    {
        public Type AttributeType;
        public string Categories;
        public string Description;
        public string DocumentationUrl;
        public bool IsEnterprise;

        public OdinRegisterAttributeAttribute(Type attributeType, string category, string description, bool isEnterprise)
        {
            AttributeType = attributeType;
            Categories = category;
            Description = description;
            IsEnterprise = isEnterprise;
        }

        public OdinRegisterAttributeAttribute(Type attributeType, string category, string description, bool isEnterprise, string url)
        {
            AttributeType = attributeType;
            Categories = category;
            Description = description;
            IsEnterprise = isEnterprise;
            DocumentationUrl = url;
        }
    }
}

namespace Sirenix.OdinInspector
{
    using System;

    [Flags]
    public enum SearchFilterOptions
    {
        PropertyName = 1 << 0,
        PropertyNiceName = 1 << 1,
        TypeOfValue = 1 << 2,
        ValueToString = 1 << 3,
        ISearchFilterableInterface = 1 << 4,
        All = ~0
    }
}

namespace Sirenix.OdinInspector
{
    public enum TitleAlignments
    {
        Left,
        Centered,
        Right,
        Split
    }
}
#endif