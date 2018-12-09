//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Timer = CoreUtility.Timer;

public class Navigator2D : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const float RUN_VELOCITY_THRESHOLD = 3f;
    private const bool MOVE_TO_CENTER_OF_TILES = true;
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [SerializeField]
    protected float m_MovementSpeed = 10f;
    [SerializeField]
    protected float m_RotationSpeed = 10f;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    protected Action m_OnDestinationReached;
    protected float m_WanderSpeed = 5f;
    protected float m_RunSpeed;

    protected Vector2Int m_LayoutPosition;
    protected Vector2 m_TilePosition;

    protected Queue<Vector2> m_Path = new Queue<Vector2>();
    protected bool m_HasDestination = false;
    protected Vector2 m_StartLerpPosition;
    protected Vector2 m_TargetLerpPosition;

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private FloorLayout m_Layout;
    private Timer m_WanderTimer;
    private PathFinder m_Pathfinder;

    public Vector2Int LayoutPosition
    {
        get { return m_LayoutPosition; }
        set { m_LayoutPosition = value; }
    }

    #region Unity API

    private void Update()
    {
        if (m_WanderTimer != null)
        {
            m_WanderTimer.Update();
        }


        Move();       
    }
    #endregion

    #region Public API
    public void Init()
    {
        m_Pathfinder = PathFinder.instance;
        m_WanderTimer = new Timer(2f);
        m_LayoutPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
    }

    public void SetDestination(Vector2Int destination, Action onDestinationReached = null)
    {
        console.logStatus("Set Destination");
        OnLocationSelected(destination, Vector2.zero);
        m_WanderTimer.Stop();
        m_WanderTimer.OnDone = null;
                
        if (onDestinationReached != null)
        {
            m_OnDestinationReached += onDestinationReached;
        }
    }

    public void Wander()
    {
        m_WanderTimer.Start();
        m_WanderTimer.OnDone -= GetNewWanderDestination;
        m_WanderTimer.OnDone += GetNewWanderDestination;
    }

    public void Move()
    {
        if (m_HasDestination)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_TargetLerpPosition, Time.deltaTime * m_MovementSpeed);
            if (Vector2.Distance(transform.position, m_TargetLerpPosition) <= 0.1f)
            {
                m_HasDestination = false;
                m_LayoutPosition = m_TargetLerpPosition.ToInt();
                if (m_Path.Count == 0)
                {
                    console.logStatus("On Destination Reached");
                    if (m_OnDestinationReached != null)
                    {
                        m_OnDestinationReached();
                        m_OnDestinationReached = null;
                    }
                }
            }
        }
        else if (m_Path != null && m_Path.Count > 0)
        {
            m_TargetLerpPosition = m_Path.Dequeue();
            m_HasDestination = true;
        }
    }

    public void OnLocationSelected(Tile destination, Vector2 innerPosition)
    {
        OnLocationSelected(destination.Position, innerPosition);
    }

    public void OnLocationSelected(Vector2Int destination, Vector2 innerPosition)
    {
        List<Vector2Int> path = PathFinder.instance.GetPath(m_LayoutPosition, destination);
        m_Path.Clear();
        m_HasDestination = false;
        if (path != null && path.Count > 0)
        {
            for (int i = 0; i < path.Count; ++i)
            {
                if (!MOVE_TO_CENTER_OF_TILES && i == path.Count - 1)
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

    #region Protect

    #endregion

    #region Private
    private void GetNewWanderDestination()
    {
        //m_NavMeshAgent.SetDestination(RandomNavmeshLocation(3f));
        m_WanderTimer.Start();
    }
    #endregion
}
