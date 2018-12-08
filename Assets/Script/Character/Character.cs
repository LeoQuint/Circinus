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
    protected float m_RepairPerSeconds = 5f;
    protected float m_RepairRange = 2f;
    protected Navigator2D m_Navigator;

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

    private void Update()
    {
        m_Navigator.Move();
    }
    #endregion

    #region Public API
    public virtual void Init()
    {
        if (m_Navigator == null)
        {
            m_Navigator = gameObject.AddComponent<Navigator2D>();
        }
        m_Navigator.Init();
    }

    public void Select()
    {
        ScreenInputController.instance.OnLocationSelected += m_Navigator.OnLocationSelected;
    }

    public void Deselect()
    {
        ScreenInputController.instance.OnLocationSelected -= m_Navigator.OnLocationSelected;
    }
    #endregion

    #region Protect      
    #endregion

    #region Private
    #endregion
}
