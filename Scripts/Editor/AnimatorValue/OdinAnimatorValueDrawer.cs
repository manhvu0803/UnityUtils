#if ODIN_INSPECTOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Vun.UnityUtils
{
    public class OdinAnimatorValueDrawer : OdinValueDrawer<AnimatorValue>
    {
        private LocalPersistentContext<bool> _expandedContext;

        private AnimatorController _animatorController;

        private InspectorProperty _animatorControllerProperty;

        private InspectorProperty _animatorIdProperty;

        private InspectorProperty _paramTypeProperty;

        private InspectorProperty _localControllerProperty;

        private InspectorProperty _intProperty;

        private InspectorProperty _floatProperty;

        private InspectorProperty _boolProperty;

        private int _selectedIndex;

        private readonly List<string> _animatorParameters  = new();

        private bool _isLayoutDone;

        private SerializedProperty _serializedProperty;

        private Rect _controlRect;

        private bool IsUsingLocalController => _animatorControllerProperty == _localControllerProperty;

        private bool IsShowingControllerField => IsUsingLocalController || AnimatorValueSettings.AlwaysShowControllerField;

        protected override void Initialize()
        {
            _expandedContext = this.GetPersistentValue<bool>($"{nameof(OdinAnimatorValueDrawer)}.{nameof(_expandedContext)}");
            InitializeProperties();
        }

        private void InitializeProperties()
        {
            _localControllerProperty = FindProperty(nameof(AnimatorValue.Controller));
            _paramTypeProperty = FindProperty(nameof(AnimatorValue.Type));
            _animatorIdProperty = FindProperty(nameof(AnimatorValue.AnimatorId));
            _intProperty = FindProperty(nameof(AnimatorValue.IntValue));
            _floatProperty = FindProperty(nameof(AnimatorValue.FloatValue));
            _boolProperty = FindProperty(nameof(AnimatorValue.BoolValue));
        }

        private InspectorProperty FindProperty(string name)
        {
            return Property.FindChild(property => property.Name == name, includeSelf: false);
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            _serializedProperty = Property.Tree.GetUnityPropertyForPath(Property.UnityPropertyPath);
            AnimatorControllerParameter selectedParameter = null;

            if (_selectedIndex >= 0 && _selectedIndex < _animatorParameters.Count)
            {
                selectedParameter = _animatorController?.parameters[_selectedIndex];
            }

            SirenixGUIStyles.Foldout.richText = true;
            UpdateAnimatorController();

            if (IsShowingControllerField)
            {
                DrawBoxedContent(selectedParameter, label);
            }
            else if (_animatorController != null)
            {
                _controlRect = EditorGUILayout.GetControlRect();
                label = EditorGUI.BeginProperty(_controlRect, label, _serializedProperty);
                DrawContent(selectedParameter, label);
                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUILayout.LabelField(label.text, "No Animator Controller found");
            }
        }

        private void DrawBoxedContent(AnimatorControllerParameter selectedParameter, GUIContent label)
        {
            SirenixEditorGUI.BeginBox();

            SirenixEditorGUI.BeginBoxHeader();
            _controlRect = EditorGUILayout.GetControlRect();
            label = EditorGUI.BeginProperty(_controlRect, label, _serializedProperty);
            label.SetLabel(_animatorController, selectedParameter, GetParamValue);
            _expandedContext.Value = SirenixEditorGUI.Foldout(_controlRect, _expandedContext.Value, label);
            SirenixEditorGUI.EndBoxHeader();

            if (SirenixEditorGUI.BeginFadeGroup(this, _expandedContext.Value))
            {
                DrawControllerField();
                DrawContent(selectedParameter, new GUIContent("Parameter"));
            }

            SirenixEditorGUI.EndFadeGroup();
            EditorGUI.EndProperty();
            SirenixEditorGUI.EndBox();
        }

        private void DrawControllerField()
        {
            EditorGUI.BeginDisabledGroup(!IsUsingLocalController);
            var newController = EditorUtils.OdinObjectField("Controller", _animatorController);
            SetAnimatorController(newController);
            EditorGUI.EndDisabledGroup();
        }

        private object GetParamValue(AnimatorControllerParameterType type)
        {
            var property = type switch
            {
                AnimatorControllerParameterType.Int => _intProperty,
                AnimatorControllerParameterType.Float => _floatProperty,
                _ => _boolProperty,
            };

            return property.ValueEntry.WeakSmartValue;
        }

        private void DrawContent(AnimatorControllerParameter selectedParameter, GUIContent label)
        {
            // When something changes, Repaint event is invoked before Layout event,
            // which causes error if the layout changes during repainting.
            // So we wait for Layout event before drawing the dynamic content
            _isLayoutDone = _isLayoutDone || Event.current.type == EventType.Layout;

            if (_animatorController == null || !_isLayoutDone)
            {
                return;
            }

            var isInsideArray = _serializedProperty.propertyPath.EndsWith(']');
            using var horizontal = new EditorGUILayout.HorizontalScope();
            DrawLabel(label, isInsideArray, out var buttonRect, out var fieldRect);

            if (GUI.Button(buttonRect, selectedParameter?.name, EditorStyles.popup))
            {
                ShowSelector(_animatorParameters, selectedParameter?.name, buttonRect);
            }

            if (selectedParameter == null)
            {
                return;
            }

            // indentLevel mess with horizontal UI
            var indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            IPropertyValueEntry valueEntry;

            switch (selectedParameter.type)
            {
                case AnimatorControllerParameterType.Int:
                    valueEntry = _intProperty.ValueEntry;
                    valueEntry.WeakSmartValue = SirenixEditorFields.DelayedIntField(fieldRect, (int)valueEntry.WeakSmartValue);
                    break;
                case AnimatorControllerParameterType.Float:
                    valueEntry = _floatProperty.ValueEntry;
                    valueEntry.WeakSmartValue = SirenixEditorFields.DelayedFloatField(fieldRect, (float)valueEntry.WeakSmartValue);
                    break;
                default:
                    valueEntry = _boolProperty.ValueEntry;
                    valueEntry.WeakSmartValue = GUI.Toggle(fieldRect, (bool)valueEntry.WeakSmartValue, "");
                    break;
            }

            EditorGUI.indentLevel = indentLevel;
        }

        private void DrawLabel(GUIContent label, bool isInsideArray, out Rect buttonRect, out Rect fieldRect)
        {
            var controlRect = _controlRect;

            if (IsShowingControllerField)
            {
                controlRect = EditorGUILayout.GetControlRect();
                buttonRect = EditorGUI.PrefixLabel(controlRect, label);
            }
            else if (isInsideArray)
            {
                buttonRect = controlRect;
            }
            else
            {
                buttonRect = EditorGUI.PrefixLabel(controlRect, label);
            }

            buttonRect.width /= 1.5f;
            fieldRect = controlRect;
            var fieldMinX = buttonRect.max.x + 5;
            fieldRect.min = new Vector2(fieldMinX, fieldRect.min.y);
            fieldRect.width = controlRect.max.x - fieldMinX;
        }

        private void UpdateAnimatorController()
        {
            var controllerData = _serializedProperty.GetAnimatorControllerData();
            AnimatorController newController;

            if (string.IsNullOrEmpty(controllerData.PropertyPath))
            {
                newController = controllerData.Value;
            }
            else
            {
                _animatorControllerProperty = Property.Tree.GetPropertyAtUnityPath(controllerData.PropertyPath);
                newController = _animatorControllerProperty.ValueEntry.WeakSmartValue.GetControllerValue();
            }

            SetAnimatorController(newController);
        }

        private void SetAnimatorController(AnimatorController newController)
        {
            if (newController == _animatorController)
            {
                return;
            }

            _isLayoutDone = false;
            _animatorController = newController;
            _localControllerProperty.ValueEntry.WeakSmartValue = newController;

            if (_animatorController == null)
            {
                return;
            }

            _animatorParameters.Clear();
            _animatorParameters.AddRange(_animatorController.parameters.Select(parameter => parameter.name));
            UpdateSelectedIndex();
        }

        private void UpdateSelectedIndex()
        {
            var id = _animatorIdProperty.ValueEntry.WeakSmartValue as int? ?? 0;

            for (var i = 0; i < _animatorParameters.Count; i++)
            {
                if (Animator.StringToHash(_animatorParameters[i]) == id)
                {
                    SetSelectedIndex(i);
                    return;
                }
            }

            SetSelectedIndex(0);
        }

        private void ShowSelector(List<string> options, string selectedOption, in Rect buttonRect)
        {
            var selector = new GenericSelector<string>("Parameter", false, options);
            selector.SetSelection(selectedOption);
            selector.EnableSingleClickToSelect();
            selector.SelectionConfirmed += OnSelectParam;
            selector.ShowInPopup(buttonRect);
        }

        private void OnSelectParam(IEnumerable<string> selection)
        {
            var index = _animatorParameters.IndexOf(selection.FirstOrDefault());
            SetSelectedIndex(index);
        }

        private void SetSelectedIndex(int index)
        {
            _selectedIndex = index;

            if (_selectedIndex < 0 || _selectedIndex >= _animatorParameters.Count)
            {
                return;
            }

            var parameter = _animatorController.parameters[_selectedIndex];
            _animatorIdProperty.ValueEntry.WeakSmartValue = Animator.StringToHash(parameter.name);
            _paramTypeProperty.ValueEntry.WeakSmartValue = parameter.type;
        }
    }
}
#endif