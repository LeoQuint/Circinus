//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour {

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
    protected bool m_IsFired = false;
    protected float m_Speed = 5f;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    #endregion

    #region Public API
    public void Init()
    {

    }

    public void ResetProjectile()
    {
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
        m_IsFired = false;
    }

    public void OnFire()
    {
        m_IsFired = true; 
    }
    #endregion

    #region Protect
    protected void Update()
    {
        if (m_IsFired)
        {
            transform.Translate(Vector3.right * m_Speed * Time.deltaTime, Space.Self);
        }
    }

    protected void OnHit(bool isVisible)
    {
        //TODO: if visible spawn particles & sfx

    }
    #endregion

    #region Private
    #endregion
}
