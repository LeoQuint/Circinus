//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCamera : MonoBehaviour {

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
    [SerializeField]
    private float m_CameraLerpSpeed = 5f;
    private float m_MinZPosition = -5f;
    private float m_MaxZPosition = -40f;

    private Vector3 m_StartDragPosition = Vector3.zero;
    private ScreenInputController m_InputController;

    #region Unity API
    private void Start()
    {
        m_InputController = ScreenInputController.instance;
    }

    private void Update()
    {
        GetInput();
    }
    #endregion

    #region Public API
    #endregion

    #region Protect
    protected void GetInput()
    {
        if (m_InputController.Inputs.MouseMidDown)
        {
            Debug.Log(m_InputController.Inputs.MouseDelta);
            Debug.Log(m_InputController.Inputs.MouseScreenPosition);
            transform.position += m_InputController.Inputs.MouseDelta * Time.deltaTime;
        }      
    }
    #endregion

    #region Private
    #endregion
}
