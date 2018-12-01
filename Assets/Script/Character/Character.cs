//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ISelectable {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const float RUN_VELOCITY_THRESHOLD = 3f;
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [SerializeField] protected float m_MovementSpeed = 10f;
    [SerializeField] protected float m_RotationSpeed = 10f;  
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////  
    protected float m_RepairPerSeconds = 1f;
    protected float m_RepairRange = 2f;

    protected bool m_IsSelected = false;

    public bool CanControl
    {
        get
        {
            return true;
        }
    }

    public int SelectPriority
    {
        get
        {
            return 10;
        }
    }

    public eSelectableType SelectableType
    {
        get
        {
            return eSelectableType.CHARACTER;
        }
    }

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    private void Awake()
    {
        Init();
    }
    #endregion

    #region Public API
    public void Select()
    {        
    }

    public void Deselect()
    {
    }
    #endregion

    #region Protect
    protected virtual void Init()
    {
       
    }   
    #endregion

    #region Private
    #endregion
}
