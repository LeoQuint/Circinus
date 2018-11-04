//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const float GROUNDED_THRESHOLD = 0.2f;
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [SerializeField] protected Rigidbody m_Rigidbody;
    [SerializeField] protected float m_MovementSpeed = 10f;
    [SerializeField] protected float m_RotationSpeed = 10f;
    [SerializeField] protected float m_JumpForce = 10f;
    [SerializeField] protected LayerMask m_GroundMask;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////    

    protected float m_RepairPerSeconds = 1f;
    protected float m_RepairRange = 2f;
    protected bool m_IsGrounded = true;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private Collider[] m_GroundCastResults;

    #region Unity API
    private void Awake()
    {
        Init();
    }
    #endregion

    #region Public API
    #endregion

    #region Protect
    protected virtual void Init()
    {
        m_GroundCastResults = new Collider[10];
    }

    protected virtual void Steer(float rotationDelta)
    {
        transform.Rotate(transform.up, rotationDelta * Time.deltaTime * m_RotationSpeed);
    }

    protected virtual void AddImpulse(Vector3 impulseVelocity)
    {
        m_Rigidbody.AddForce(impulseVelocity * m_MovementSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    protected virtual void Jump(bool isJumping)
    {
        m_IsGrounded = Physics.OverlapSphereNonAlloc(transform.position, GROUNDED_THRESHOLD, m_GroundCastResults, m_GroundMask) > 0;            

        if (isJumping && m_IsGrounded)
        {
            m_Rigidbody.AddForce(transform.up * m_JumpForce, ForceMode.Impulse);
        }
    }
    #endregion

    #region Private
    #endregion
}
