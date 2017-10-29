//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform))]
public class EditorHelper : Editor{

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

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
    private static bool isLocal = true;

    #region Unity API
    public override void OnInspectorGUI()
    {
        _target = target as Transform;
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Local", EditorStyles.miniButtonLeft))
        {
            isLocal = true;
        }
        if(GUILayout.Button("World", EditorStyles.miniButtonMid))
        {
            isLocal = false;
        }
        if (GUILayout.Button("Reset", EditorStyles.miniButtonRight))
        {
            Debug.Log("...");
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
                _target.localScale = new Vector3(1f/(_target.lossyScale.x/_target.localScale.x)  , 1f / (_target.lossyScale.y / _target.localScale.y), 1f / (_target.lossyScale.z / _target.localScale.z));
            }
        }
        GUILayout.EndHorizontal();
        if (isLocal)
        {
            _target.localPosition = EditorGUILayout.Vector3Field("Local Position", _target.localPosition);
            _target.localRotation = Quaternion.Euler(EditorGUILayout.Vector3Field("Local Rotation", _target.localRotation.eulerAngles));
            _target.localScale = EditorGUILayout.Vector3Field("Local Scale", _target.localScale);
        }
        else
        {
            _target.position = EditorGUILayout.Vector3Field("World Position", _target.position);
            _target.rotation = Quaternion.Euler(EditorGUILayout.Vector3Field("World Rotation", _target.rotation.eulerAngles));
            EditorGUILayout.Vector3Field("World Scale", _target.lossyScale);            
        }
    }
    #endregion
}
