//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningComponent : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const string FIRE_ANIMATOR_RANK_PARAMETER_KEY = "FireRank";
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [SerializeField] protected Animator m_Animator;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    protected float m_FireRank = 0;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    #endregion

    #region Public API
    public void Init(Vector2Int location)
    {
        transform.localPosition = new Vector3(location.x, location.y, 0f);
        gameObject.SetActive(true);
    }

    public void ResetComponent()
    {
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
        m_FireRank = 0;
        m_Animator.SetInteger(FIRE_ANIMATOR_RANK_PARAMETER_KEY, 0);
    }

    public void SetFireRank(float rank)
    {
        m_FireRank = rank;
        m_Animator.SetInteger(FIRE_ANIMATOR_RANK_PARAMETER_KEY, (int)m_FireRank);
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
