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
    private const float RUN_VELOCITY_THRESHOLD = 3f;
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

    [SerializeField] protected Animator m_Animator;
    [SerializeField] protected Actions m_Actions;
    [SerializeField] protected PlayerController m_PlayerController;
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
    private Vector3 m_DirectionalVector;

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
        m_DirectionalVector = (Vector3.forward * m_Rigidbody.velocity.x) + (Vector3.right * m_Rigidbody.velocity.z);
        if (m_DirectionalVector.magnitude > RUN_VELOCITY_THRESHOLD)
        {
            m_Actions.Run();
        }
        else if (m_DirectionalVector.magnitude > Mathf.Epsilon)
        {
            m_Actions.Walk();
        }
        else
        {
            m_Actions.Stay();
        }
    }

    protected virtual void Jump(bool isJumping)
    {
        m_IsGrounded = Physics.OverlapSphereNonAlloc(transform.position, GROUNDED_THRESHOLD, m_GroundCastResults, m_GroundMask) > 0;            

        if (isJumping && m_IsGrounded)
        {
            m_Rigidbody.AddForce(transform.up * m_JumpForce, ForceMode.Impulse);
            m_Actions.Jump();
        }
    }
    #endregion

    #region Private
    #endregion
}
