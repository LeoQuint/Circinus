//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using CoreUtility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningComponent : MonoBehaviour {

    public enum eBurningStatus
    {
        NONE,//no fire is burning or acting upon this
        IGNITING,//an adjacent fire is igniting this
        BURNING//this component is bruning
    }

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const float EXTINGUISHED_DURATION_BEFORE_REMOVING = 1f;
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

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private eBurningStatus m_Status;
    private FloorLayout m_Layout;
    private Timer m_Timer;
    private List<BurningComponent> m_NeighbourComponents = new List<BurningComponent>();

    private float m_FireRank;
    private float m_IgniteTicks;


    public eBurningStatus Status
    {
        get { return m_Status; }
    }
    #region Unity API
    #endregion

    #region Public API
    public void Init(FloorLayout layout, float rankValue = 0f, float igniteTicks = 0)
    {
        m_Layout = layout;
        m_FireRank = rankValue;
        m_IgniteTicks = igniteTicks;
    }

    public void UpdateComponent(float deltaTime)
    {
        switch (m_Status)
        {
            case eBurningStatus.NONE:
                if (m_Timer != null)
                {
                    m_Timer.Update();
                }
                break;

            case eBurningStatus.IGNITING:
                break;

            case eBurningStatus.BURNING:
                UpdateBurning(deltaTime);
                break;
        }
    }

    public static BurningComponent CreateInstance(Transform parent)
    {
        GameObject holder = new GameObject();
        holder.transform.SetParent(parent, false);
        return holder.gameObject.AddComponent<BurningComponent>();
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private void UpdateBurning(float deltaTime)
    {
        m_FireRank += GameConstants.FIRE_GROWTH_PER_SECS * deltaTime;
        m_FireRank = Mathf.Clamp(m_FireRank, 0f, GameConstants.MAX_FIRE_RANK);
    }

    private void AddIgniteTicks(float amount)
    {
        if (m_Status == eBurningStatus.IGNITING)
        {
            m_IgniteTicks += amount;
            if (m_IgniteTicks >= GameConstants.IGNITE_TICKS_REQUIRED)
            {
                //Reset the ignite ticks to half whats required in case this gets exinguished.
                m_IgniteTicks = GameConstants.IGNITE_TICKS_REQUIRED * GameConstants.IGNITE_TICKS_RATIO_AFTER_IGNITION;
                //Set on fire
                m_Status = eBurningStatus.BURNING;
                //Add/get neighbours
                
            }
        }
    }

    private void SetVisuals()
    {
        //stub
    }

    private void OnExtinguished()
    {
        m_Timer = new Timer(EXTINGUISHED_DURATION_BEFORE_REMOVING);
        m_Timer.OnDone = RemoveComponent;
        m_Timer.Start();
    }

    private void RemoveComponent()
    {
        Destroy(gameObject);
    }
    #endregion
}
