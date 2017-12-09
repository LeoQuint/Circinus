//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour {

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
    protected Rigidbody m_Rigidbody;
    protected Vector3 m_DirectionInput;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////


    #region Unity API
    protected void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    protected void Update()
    {
        //GetCharacterInput();
        GetMouseInput();
        ApplyMovement();
    }
    #endregion

    #region Public API
    #endregion

    #region Protect
    #endregion

    #region Private
    private void GetCharacterInput()
    {
        m_DirectionInput = Vector3.zero;
        //temp
        if (Input.GetKey(KeyCode.W))
        {
            m_DirectionInput += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_DirectionInput += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_DirectionInput += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_DirectionInput += Vector3.right;
        }
    }

    //private float previous
    private void GetMouseInput()
    {
        float zMovement = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            zMovement += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            zMovement += -1f;
        }
        m_DirectionInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), zMovement);
    }

    private void ApplyMovement()
    {
        m_Rigidbody.AddForce(m_DirectionInput * 1000f * Time.deltaTime);
    }
    #endregion
}
