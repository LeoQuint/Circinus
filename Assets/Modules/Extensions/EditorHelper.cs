//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using CoreUtility;

[CustomEditor(typeof(Transform))]
public class EditorHelper : Editor{

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const string MenuNameColorOption = "EditorHelper/Transform/Enable Colors";
    private const string MenuNameLocalWorldOption = "EditorHelper/Transform/Enable Local World";
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    public static bool _EnableColors = true;
    public static bool _EnableLocalWorld = true;

    private static Color m_LocalColor = new Color(0.5f, 1f, 0.5f, 1f);
    private static Color m_WorldColor = new Color(1f, 0.5f, 0.5f, 1f);
    private static bool isLocal = true;
    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private Transform _target;

    [MenuItem(MenuNameColorOption)]
    private static void ToggleColors()
    {
        _EnableColors = !_EnableColors;
        Menu.SetChecked(MenuNameColorOption, _EnableColors);
        EditorPrefs.SetBool(MenuNameColorOption, _EnableColors);
    }
    [MenuItem(MenuNameLocalWorldOption)]
    private static void ToggleLocalWorld()
    {
        _EnableLocalWorld = !_EnableLocalWorld;
        Menu.SetChecked(MenuNameLocalWorldOption, _EnableLocalWorld);
        EditorPrefs.SetBool(MenuNameLocalWorldOption, _EnableLocalWorld);
    }
    
    static void OptionsMenu()
    {
        EditorApplication.delayCall += () =>
        {
            _EnableColors = EditorPrefs.GetBool(MenuNameColorOption, true);
            Menu.SetChecked(MenuNameColorOption, _EnableColors);
        };
        EditorApplication.delayCall += () =>
        {
            _EnableLocalWorld = EditorPrefs.GetBool(MenuNameLocalWorldOption, true);
            Menu.SetChecked(MenuNameLocalWorldOption, _EnableLocalWorld);
        };
    }

    #region Unity API
    private void Awake()
    {
        OptionsMenu();
    }

    public override void OnInspectorGUI()
    {
        CustomTransform();
    }
    #endregion

    #region Private
    private void CustomTransform()
    {
        if (!_EnableLocalWorld)
        {
            DrawDefaultInspector();
            return;
        }

        _target = target as Transform;
        GUILayout.BeginHorizontal();

        if (_EnableColors) { GUI.color = m_LocalColor; }

        if (GUILayout.Toggle(isLocal, "Local", EditorStyles.miniButtonLeft))
        {
            isLocal = true;
        }

        if (_EnableColors) { GUI.color = m_WorldColor; }

        if (GUILayout.Toggle(!isLocal, "World", EditorStyles.miniButtonMid))
        {
            isLocal = false;
        }

        if (_EnableColors) { GUI.color = Color.white; }

        if (GUILayout.Button("Reset", EditorStyles.miniButtonRight))
        {
            if (isLocal)
            {
                _target.localPosition = Vector3.zero;
                _target.localRotation = Quaternion.identity;
                _target.localScale = Vector3.one;
            }
            else
            {
                _target.position = Vector3.zero;
                _target.rotation = Quaternion.identity;
                _target.localScale = new Vector3(1f / (_target.lossyScale.x / _target.localScale.x), 1f / (_target.lossyScale.y / _target.localScale.y), 1f / (_target.lossyScale.z / _target.localScale.z));
            }
        }
        GUILayout.EndHorizontal();

        if (_EnableColors) { GUI.color = isLocal ? m_LocalColor : m_WorldColor; }

        if (isLocal)
        {
            _target.localPosition = EditorGUILayout.Vector3Field("Position", _target.localPosition);
            _target.localRotation = Quaternion.Euler(EditorGUILayout.Vector3Field("Rotation", _target.localRotation.eulerAngles));
            _target.localScale = EditorGUILayout.Vector3Field("Scale", _target.localScale);
        }
        else
        {
            _target.position = EditorGUILayout.Vector3Field("Position", _target.position);
            _target.rotation = Quaternion.Euler(EditorGUILayout.Vector3Field("Rotation", _target.rotation.eulerAngles));
            EditorGUILayout.Vector3Field("Scale", _target.lossyScale);
        }
    }
    #endregion
}

public class InEditorReadOnly : PropertyAttribute
{  
}


[CustomPropertyDrawer(typeof(InEditorReadOnly))]
public class InEditorReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.LabelField(position, label);
        EditorGUI.BeginDisabledGroup(true);
        EditorGUI.PropertyField(position, property, true);
        EditorGUI.EndDisabledGroup();
        EditorGUI.EndProperty();            
    }
}