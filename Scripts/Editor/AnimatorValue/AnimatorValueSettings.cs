using System.Reflection;
using UnityEditor;
using UnityEditor.SettingsManagement;
using UnityEngine;

namespace Vun.UnityUtils
{
    /// <summary>
    /// Manage settings of the <see cref="AnimatorValue"/> editor
    /// </summary>
    public static class AnimatorValueSettings
    {
        private const string InheritTargetTooltip = "If enabled, AnimatorValueTarget fields in base classes will be inherited by child classes";

        private const string ShowControllerFieldTooltip = "If enabled, the Controller field will always be showed, even for AnimatorValue with defined target";

        internal static readonly Settings Settings = new("animator-value");

        internal static readonly Assembly[] SettingAssemblies = { typeof(AnimatorValueSettings).Assembly };

        [UserSetting(category: "Settings", title: "Inherit AnimatorValueTarget from base classes", InheritTargetTooltip)]
        private static readonly UserSetting<bool> InheritTargetSetting = new(Settings, "inheritTargetFromBase", true);

        [UserSetting(category: "Settings", title: "Always show Controller field", ShowControllerFieldTooltip)]
        private static readonly UserSetting<bool> AlwaysShowControllerFieldSetting = new(Settings, "alwaysShowController", true);

        public static bool InheritTarget
        {
            get => InheritTargetSetting.value;
            internal set => InheritTargetSetting.value = value;
        }

        public static bool AlwaysShowControllerField
        {
            get => AlwaysShowControllerFieldSetting.value;
            internal set => AlwaysShowControllerFieldSetting.value = value;
        }

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new UserSettingsProvider(
                "Project/Animator Value",
                Settings,
                SettingAssemblies,
                SettingsScope.Project
            );
        }

        // ReSharper disable once UnusedMember.Global
        [UserSettingBlock("Settings")]
        public static void DrawCustomGui(string searchContext)
        {
            if (GUILayout.Button("Refresh cache"))
            {
                AnimatorValueManager.RefreshCache();
                Debug.Log("AnimatorValue cache refreshed");
            }
        }
    }
}