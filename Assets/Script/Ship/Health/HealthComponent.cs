//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour {

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
    public Action<float> OnHit;
    public Action OnHealthDepleted;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    [InEditorReadOnly][SerializeField]
    protected float m_CurrentHealth;
    [InEditorReadOnly][SerializeField]
    protected float m_MaxHealth;
    [InEditorReadOnly][SerializeField]
    protected bool m_IsInvulnerable = false;
	////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    public float CurrentHealth
    {
        get { return m_CurrentHealth;  }
    }

    public float CurrentRatio
    {
        get { return m_CurrentHealth / m_MaxHealth;  }
    }

    public bool IsInvulnerable
    {
        get { return m_IsInvulnerable;  }
        set { m_IsInvulnerable = value;  }
    }
    #region Unity API
    #endregion

    #region Public API
    public void Init(float maxHealth)
    {
        m_MaxHealth = maxHealth;
        m_CurrentHealth = maxHealth;
    }

    public void Hit(float amount)
    {
        if (m_IsInvulnerable && amount > 0f)
        {
            if (OnHit != null)
            { 
                OnHit(0f);
            }
            return;
        }

        m_CurrentHealth = Mathf.Clamp(m_CurrentHealth - amount, 0f, m_MaxHealth);

        if (OnHit != null)
        {
            OnHit(0f);
        }

        if (m_CurrentHealth == 0)
        {
            if (OnHealthDepleted != null)
            {
                OnHealthDepleted();
            }          
        }
    }

    public void Heal(float amount)
    {
        Hit(-amount);
    }
    #endregion

    #region Protect

    #endregion

    #region Private
    #endregion
}
