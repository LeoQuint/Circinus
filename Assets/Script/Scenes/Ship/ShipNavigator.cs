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
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    //properties
    #region Unity API
    protected void Update()
    {
        Steer(Time.deltaTime);
    }
    protected virtual void OnShipUpdate(float deltaTime)
    {
        Steer(deltaTime);
        Move(deltaTime);
    }
    #endregion

    #region Public API
    public void Init(Ship ship)
    {
        m_Destination = transform.localPosition;
        m_Ship = ship;
        m_Data = m_Ship.Data;
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private void Move(float deltaTime)
    {
        //accelerate towards max speed

        //move

    }

    private void Steer(float deltaTime)
    {
        Vector3 direction = (m_Destination - transform.localPosition);
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        Vector3 rot = Vector3.zero;
        rot.z = rotation + 180f;
        transform.localEulerAngles = rot;
    }
    #endregion
}
