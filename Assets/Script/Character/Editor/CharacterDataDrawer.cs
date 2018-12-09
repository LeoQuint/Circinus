//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(CharacterData))]
public class CharacterDataDrawer : Editor
{

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

    #region Unity API 
    public override void OnInspectorGUI()
    {
        CharacterData data = target as CharacterData;

        for (int i = 0; i < data.m_Priorities.Count; ++i)
        {
            DrawPriorityBox(data.m_Priorities[i]);
        }

        DrawDefaultInspector();
    }
    #endregion

    #region Public API
    #endregion

    #region Protect
    #endregion

    #region Private
    private void DrawPriorityBox(CharacterData.sTaskPriority priotrity)
    {

    }
    #endregion
}
