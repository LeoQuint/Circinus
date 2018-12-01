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

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [SerializeField]
    List<Transform> m_Destinations;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    protected Action m_OnDestinationReached;

    protected float m_WanderSpeed = 5f;
    protected float m_RunSpeed;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private FloorLayout m_Layout;
    private bool m_HasDestination = false;
    private Timer m_WanderTimer;


    #region Unity API
    private void Awake()
    {       
        m_WanderTimer = new Timer();
        m_WanderTimer.Init(2f);
    }

    private void Update()
    {
        if (m_WanderTimer != null)
        {
            m_WanderTimer.Update();
        }

        if (m_HasDestination)
        {
            if (true)
            {
                console.logStatus("On Destination Reached");
                m_HasDestination = false;
                if (m_OnDestinationReached != null)
                {
                    m_OnDestinationReached();
                    m_OnDestinationReached = null;
                }
            }
        }
    }
    #endregion

    #region Public API
    public void SetDestination(Transform destination, Action onDestinationReached = null)
    {
        console.logStatus("Set Destination");
        m_WanderTimer.Stop();
        m_WanderTimer.OnDone = null;

        m_HasDestination = true;
        if (onDestinationReached != null)
        {
            m_OnDestinationReached += onDestinationReached;
        }
    }

    public void Wander()
    {
        //SetDestination(RandomNavmeshLocation(10f));
        m_WanderTimer.Start();
        m_WanderTimer.OnDone -= GetNewWanderDestination;
        m_WanderTimer.OnDone += GetNewWanderDestination;
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

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
    #endregion
}
