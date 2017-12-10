//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTestScript : MonoBehaviour {

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
    public UIPanel m_TestPanel;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_TestPanel.Show(OnAnimationDone: OnDone);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            m_TestPanel.Hide(OnAnimationDone: OnDone);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_TestPanel.Show(true, OnAnimationDone: OnDone);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            m_TestPanel.Hide(true, OnAnimationDone: OnDone);
        }
    }
    #endregion

    #region Public API
    #endregion

    #region Protect
    #endregion

    #region Private
    private void OnDone(bool isHidden)
    {
        m_TestPanel.OnAnimationDone -= OnDone;
        Debug.Log("Hidden: " + isHidden);
    }
    #endregion
}
