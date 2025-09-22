using System;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Vun.UnityUtils
{
    [CustomPropertyDrawer(typeof(AnimatorValue))]
    public sealed class AnimatorValueDrawer : PropertyDrawer
    {
        private SerializedProperty _animatorIdProperty;

        private SerializedProperty _parentProperty;

        private SerializedProperty _localControllerProperty;

        private SerializedProperty _animatorControllerProperty;

        private SerializedProperty _intProperty;

        private SerializedProperty _floatProperty;

        private SerializedProperty _boolProperty;

        // We have to use AnimatorController instead of RuntimeAnimatorController
        // for the parameters only exists in the AnimatorController
        private AnimatorController _animatorController;

        private AnimatorControllerParameter _selectedParameter;

        private int _selectedIndex;

        private string[] _animatorParameters = Array.Empty<string>();

        private bool _isFoldoutExpanded;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InitFields(property);
            AnimatorControllerParameter selectedParameter = null;

            if (_selectedIndex >= 0 && _selectedIndex < _animatorParameters.Length)
            {
                selectedParameter = _animatorController?.parameters[_selectedIndex];
            }

            UpdateAnimatorController();
            var isUsingLocalController = SerializedProperty.EqualContents(_animatorControllerProperty,  _localControllerProperty);
            EditorGUI.BeginProperty(position, label, property);

            if (isUsingLocalController || AnimatorValueSettings.AlwaysShowControllerField)
            {
                DrawBoxedContent(selectedParameter, label, isUsingLocalController);
            }
            else if (_animatorController != null)
            {
                DrawContent(selectedParameter, label.text);
            }
            else
            {
                EditorGUILayout.LabelField(label.text, "No Animator Controller found");
            }

            EditorGUI.EndProperty();
        }

        private void InitFields(SerializedProperty property)
        {
            _parentProperty = property;
            _localControllerProperty = FindProperty(nameof(AnimatorValue.Controller));
            _animatorIdProperty = FindProperty(nameof(AnimatorValue.AnimatorId));
            _intProperty = FindProperty(nameof(AnimatorValue.IntValue));
            _floatProperty = FindProperty(nameof(AnimatorValue.FloatValue));
            _boolProperty = FindProperty(nameof(AnimatorValue.BoolValue));
        }

        private void UpdateAnimatorController()
        {
            var controllerData = _parentProperty.GetAnimatorControllerData();
            AnimatorController newController;

            if (string.IsNullOrEmpty(controllerData.PropertyPath))
            {
                newController = controllerData.Value;
            }
            else
            {
                _animatorControllerProperty = _parentProperty.serializedObject.FindProperty(controllerData.PropertyPath);
                newController = _animatorControllerProperty.GetControllerValue();
            }

            UpdateAnimatorController(newController);
        }

        private void UpdateAnimatorController(AnimatorController newController)
        {
            if (newController == _animatorController)
            {
                return;
            }

            _animatorController = newController;
            _localControllerProperty.objectReferenceValue = newController;

            if (_animatorController == null)
            {
                return;
            }

            UpdateParameters();
        }

        private void UpdateParameters()
        {
            var parameters = _animatorController.parameters;
            _animatorParameters = new string[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                _animatorParameters[i] = parameters[i].name;
            }

            UpdateAnimatorId();
        }

        private void UpdateAnimatorId()
        {
            var id = _animatorIdProperty.intValue;

            for (var i = 0; i < _animatorParameters.Length; i++)
            {
                if (Animator.StringToHash(_animatorParameters[i]) == id)
                {
                    _selectedIndex = i;
                    break;
                }
            }
        }

        private void DrawBoxedContent(AnimatorControllerParameter selectedParameter, GUIContent label, bool isUsingLocalController)
        {
            var labelText = AnimatorValueManager.GetLabel(label.text, _animatorController, selectedParameter, GetParamValue);
            EditorStyles.foldout.richText = true;
            _isFoldoutExpanded = EditorGUILayout.Foldout(_isFoldoutExpanded, new GUIContent(labelText), toggleOnLabelClick: true);

            if (!_isFoldoutExpanded)
            {
                return;
            }

            if (EditorGUILayout.BeginFadeGroup(1))
            {
                DrawControllerField(isUsingLocalController);
                DrawContent(selectedParameter, "Parameter");
            }

            EditorGUILayout.EndFadeGroup();
        }

        private void DrawControllerField(bool isUsingLocalController)
        {
            EditorGUI.BeginDisabledGroup(!isUsingLocalController);
            var newController = EditorUtils.ObjectField("Controller", _animatorController);
            UpdateAnimatorController(newController);
            EditorGUI.EndDisabledGroup();
        }

        private object GetParamValue(AnimatorControllerParameterType type)
        {
            return type switch
            {
                AnimatorControllerParameterType.Int => _intProperty.intValue,
                AnimatorControllerParameterType.Float => _floatProperty.floatValue,
                _ => _boolProperty.boolValue,
            };
        }

        private void DrawContent(AnimatorControllerParameter selectedParameter, string label)
        {
            if (_animatorController == null)
            {
                return;
            }

            using var horizontal = new EditorGUILayout.HorizontalScope();
            DrawLabel(label, out var buttonRect, out var fieldRect);
            _selectedIndex = EditorGUI.Popup(buttonRect, _selectedIndex, _animatorParameters.ToArray());

            if (selectedParameter == null)
            {
                return;
            }

            switch (selectedParameter.type)
            {
                case AnimatorControllerParameterType.Int:
                    _intProperty ??= FindProperty(nameof(AnimatorValue.IntValue));
                    _intProperty.intValue = EditorGUI.IntField(fieldRect, new GUIContent(), _intProperty.intValue);
                    break;
                case AnimatorControllerParameterType.Float:
                    _floatProperty ??= FindProperty(nameof(AnimatorValue.FloatValue));
                    _floatProperty.floatValue = EditorGUI.FloatField(fieldRect, new GUIContent(), _floatProperty.floatValue);
                    break;
                default:
                    _boolProperty ??= FindProperty(nameof(AnimatorValue.BoolValue));
                    _boolProperty.boolValue = GUI.Toggle(fieldRect, _boolProperty.boolValue, "");
                    break;
            }
        }

        private static void DrawLabel(string label, out Rect buttonRect, out Rect fieldRect)
        {
            var controlRect = EditorGUILayout.GetControlRect();
            buttonRect = EditorGUI.PrefixLabel(controlRect, new GUIContent(label));
            buttonRect.width /= 1.5f;
            fieldRect = controlRect;
            var fieldMinX = buttonRect.max.x + 5;
            fieldRect.min = new Vector2(fieldMinX, fieldRect.min.y);
            fieldRect.width = controlRect.max.x - fieldMinX;
        }

        private SerializedProperty FindProperty(string name)
        {
            return _parentProperty.FindPropertyRelative(name);
        }
    }
}