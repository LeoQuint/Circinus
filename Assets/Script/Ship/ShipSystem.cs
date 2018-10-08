//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class ShipSystem : MonoBehaviour, IDamageable {

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
    [SerializeField] protected HealthComponent m_HealthComponent;

    protected Character m_MainCharacterSlot;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    protected void Awake()
    {
        m_MainCharacterSlot = null;
    }
    #endregion

    #region Public API
    public virtual void Interact(Character character)
    {
        m_MainCharacterSlot = character;
    }

    public void Disengage(Character character)
    {
        m_MainCharacterSlot = null;
    }

    public bool CanRepair()
    {
        return m_HealthComponent.CurrentRatio < 1f;
    }

    public void Damage(float amount)
    {
        m_HealthComponent.Hit(amount);
    }

    public void Repair(float amount)
    {
        m_HealthComponent.Heal(amount);
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
