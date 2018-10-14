//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
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
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private NavMeshAgent m_NavMeshAgent;
    private bool m_HasDestination = false;

    #region Unity API
    private void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Debug Navigation test." + m_NavMeshAgent.isOnNavMesh);

            m_NavMeshAgent.SetDestination(m_Destinations[UnityEngine.Random.Range(0,m_Destinations.Count)].position); 
            
        }

        if (m_HasDestination)
        {
            if (!m_NavMeshAgent.hasPath && m_NavMeshAgent.remainingDistance <= Mathf.Epsilon)
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
        m_HasDestination = true;
        if (onDestinationReached != null)
        {
            m_OnDestinationReached += onDestinationReached;
        }
        m_NavMeshAgent.SetDestination(destination.position);
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
