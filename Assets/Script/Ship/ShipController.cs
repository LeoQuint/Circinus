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
    private float m_RotationSpeed;
    private float m_Thrust;

    #region Unity API
    protected void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        console.log(m_Rigidbody.velocity + " "  + "jj");
    }

    protected void Update()
    {
        //GetCharacterInput();
        GetMouseInput();
        //ApplyMovement();
        ApplyRotation();
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

    private void GetMouseInput()
    {
        m_DirectionInput = Vector3.zero;
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

    private void ApplyRotation()
    {
        transform.Rotate(m_DirectionInput * m_RotationSpeed * Time.deltaTime);
    }

    private void ApplyMovement()
    {
        m_Rigidbody.AddForce(m_DirectionInput * 1000f * Time.deltaTime);
    }
    #endregion
}
