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
    protected Vector2Int m_LayoutPosition;
    protected Vector2 m_TilePosition;
    
    protected Queue<Vector2> m_Path = new Queue<Vector2>();
    protected bool m_HasDestination = false;
    protected Vector2 m_StartLerpPosition;
    protected Vector2 m_TargetLerpPosition;

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

    private void Update()
    {
        Move();
    }
    #endregion

    #region Public API
    public void Select()
    {
        ScreenInputController.instance.OnLocationSelected += OnLocationSelected;
    }

    public void Deselect()
    {
        ScreenInputController.instance.OnLocationSelected -= OnLocationSelected;
    }
    #endregion

    #region Protect
    protected virtual void Init()
    {
        m_LayoutPosition = Vector2Int.zero;
    }

    protected void Move()
    {
        if (m_HasDestination)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_TargetLerpPosition, Time.deltaTime * m_MovementSpeed);
            if (Vector2.Distance(transform.position, m_TargetLerpPosition) <= 0.1f)
            {
                m_HasDestination = false;
                m_LayoutPosition = m_TargetLerpPosition.ToInt();
            }
        }
        else if (m_Path != null && m_Path.Count > 0)
        {
            m_TargetLerpPosition = m_Path.Dequeue();
            m_HasDestination = true;
        }
    }

    protected void OnLocationSelected(Tile destination, Vector2 innerPosition)
    {
        List<Vector2Int> path = PathFinder.instance.GetPath(m_LayoutPosition, destination.Position);
        m_Path.Clear();
        m_HasDestination = false;
        if (path != null && path.Count > 0)
        {
            for (int i = 0; i < path.Count; ++i)
            {
                if (i == path.Count - 1)
                {
                    m_Path.Enqueue(path[i] + innerPosition);
                }
                else if (i == 0)
                {
                    m_Path.Enqueue(transform.position);
                }
                else
                {
                    m_Path.Enqueue(path[i]);
                }
            }
        }
    }
    #endregion

    #region Private

    #endregion
}
