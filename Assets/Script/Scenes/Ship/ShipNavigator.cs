//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipNavigator : MonoBehaviour {

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
    protected Ship m_Ship;
    protected ShipData m_Data;

    protected Vector3 m_Destination;
    protected float m_CurrentSpeed;
    protected float m_CurrentSteeringSpeed;
    protected bool m_HasReachedDestination = true;
    protected bool m_IsStopped = true;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    //properties
    #region Unity API
   
    #endregion

    #region Public API
    public void Init(Ship ship)
    {
        m_Destination = transform.localPosition;
        m_Ship = ship;
        m_Data = m_Ship.Data;
    }

    public virtual void OnShipUpdate(float deltaTime)
    {
        if (!m_HasReachedDestination)
        {
            Steer(deltaTime);
            Move(deltaTime);
            CheckDestination();
        }
        else 
        {
            if (!m_IsStopped)
            {
                Decelerate(deltaTime);
            }
        }
    }

    //test
    public Vector3 DEBUG_POSITION;
    [ContextMenu("Go TO")]
    public void DEBUG_GO_TO()
    {
        SetDestination(DEBUG_POSITION);
    }

    public void SetDestination(Vector3 destination)
    {
        if (Vector3.Distance(destination, transform.position) > 1f)
        {
            m_HasReachedDestination = false;
            m_IsStopped = false;
            m_Destination = destination;
        }
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private void Move(float deltaTime)
    {
        //accelerate towards max speed
        m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed + (m_Data.BaseAcceleration * deltaTime), 0f, m_Data.BaseMaxSpeed);
        //move
        transform.position += -transform.right * deltaTime * m_CurrentSpeed;
    }

    private void Decelerate(float deltaTime)
    {
        //break towards 0 speed
        m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed - (m_Data.BaseDeceleration * deltaTime), 0f, m_Data.BaseMaxSpeed);
        //move
        transform.position += -transform.right * deltaTime * m_CurrentSpeed;
        //check for stoppage
        if (m_CurrentSpeed <= 0f)
        {
            m_IsStopped = true;
        }
    }

    private void Steer(float deltaTime)
    {
        //calculate target rotation
        Vector3 direction = (m_Destination - transform.localPosition);
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        Vector3 rot = Vector3.zero;
        rot.z = rotation + 180f;
        transform.localEulerAngles = rot;
        //accelerate towards max steering

        //steer

    }

    private void CheckDestination()
    {
        if (Vector3.Distance(transform.position, m_Destination) < 1f)
        {
            m_HasReachedDestination = true;
        }
    }
    #endregion
}
