//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private NavMeshAgent m_NavMeshAgent;

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

            m_NavMeshAgent.SetDestination(m_Destinations[Random.Range(0,m_Destinations.Count)].position); 
            
        }
    }
    #endregion

    #region Public API
    public void SetDestination(Transform destination)
    {
        m_NavMeshAgent.SetDestination(destination.position);
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
