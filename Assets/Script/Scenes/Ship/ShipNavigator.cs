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

    protected Vector3 m_Destination;
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
        m_Ship = ship;
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private void Move(float deltaTime)
    {
        
    }

    private void Steer(float deltaTime)
    {
        Vector3 direction = (m_Destination - transform.localPosition);
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Debug.Log(rotation + " From " + direction);
        Vector3 rot = Vector3.zero;
        rot.z = rotation + 180f;
        transform.localEulerAngles = rot;
    }
    #endregion
}
