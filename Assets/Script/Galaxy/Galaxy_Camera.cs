//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy_Camera : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [SerializeField]
    private float m_CameraLerpSpeed = 5f;
    [SerializeField]
    private float m_CameraZoomSpeed = 10f;
    [SerializeField]
    private float m_CameraZoomSmoothingRatio = 0.5f;
    [SerializeField]
    private AnimationCurve m_CameraLerpSpeedToZoom;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private float m_MinZPosition = -5f;
    private float m_MaxZPosition = -40f;

    private Vector3 m_InputMovement = Vector3.zero;
    private float m_ZMovement = 0f;
    #region Unity API
    private void Update()
    {
        GetInput();
        Vector3 position = Vector3.Lerp(transform.position, transform.position + m_InputMovement, Time.deltaTime * m_CameraLerpSpeed);
        position.z = Mathf.Lerp(transform.position.z, transform.position.z + m_ZMovement, m_CameraZoomSmoothingRatio);
        position.z = Mathf.Clamp(position.z, m_MaxZPosition ,m_MinZPosition);
        transform.position = position;
    }
    #endregion

    #region Public API
    #endregion

    #region Protect
    protected void GetInput()
    {
        //Temp inputs
        if (Input.GetButton("Fire2"))
        {
            float x = transform.position.z / m_MaxZPosition;
            float lerpSpeed = m_CameraLerpSpeedToZoom.Evaluate(x);
            m_InputMovement.x = -Input.GetAxis("Horizontal") * lerpSpeed;
            m_InputMovement.y = -Input.GetAxis("Vertical") * lerpSpeed;
        }

        m_ZMovement = Input.GetAxis("Mouse ScrollWheel") * m_CameraZoomSpeed;
    }
    #endregion

    #region Private
    #endregion
}
