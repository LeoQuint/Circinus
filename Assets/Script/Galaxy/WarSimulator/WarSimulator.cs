//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using War;

public class WarSimulator : Subject {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    public static WarSimulator instance;
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
    private Faction m_RedFaction;
    private Faction m_BlueFaction;
    private Faction m_NeutralFaction;

    private Galaxy m_Galaxy;

    private EFaction m_CurrentTurn;
    private float m_NextTurnTimer = 1000f;

    #region Unity API
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    protected void Start()
    {
        Init();
    }

    protected void Update()
    {
        if (m_NextTurnTimer <= Time.time)
        {
            Turn();
        }
    }
    #endregion

    #region Public API
    public void Init()
    {
        m_RedFaction        = new Faction(EFaction.Red);
        m_BlueFaction       = new Faction(EFaction.Blue);
        m_NeutralFaction    = new Faction(EFaction.Neutral);

        Turn();
    }

    public void Load(Galaxy galaxy)
    {
        m_Galaxy = galaxy;
    }

    public void Save()
    {

    }

    public void Turn()
    {
        m_NextTurnTimer = Time.time + GameConstants.TURN_DURATION;
        NextTurn();
        Debug.Log("New Turn begins for " + m_CurrentTurn.ToString());
        switch (m_CurrentTurn)
        {
            case EFaction.Red:
                m_RedFaction.TakeTurn();
                break;
            case EFaction.Blue:
                m_BlueFaction.TakeTurn();
                break;
            case EFaction.Neutral:
                m_NeutralFaction.TakeTurn();
                break;
            default:
                break;
        }
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private EFaction NextTurn()
    {
        if (m_CurrentTurn == EFaction.Neutral)
        {
            m_CurrentTurn = EFaction.Red;
        }
        else
        {
            ++m_CurrentTurn;
        }
        return m_CurrentTurn;
    }

    /// <summary>
    /// Simulate the start of the war by letting players pick star systems
    /// one after an other.
    /// </summary>
    private void SimulateOutbreak()
    {

    }
    #endregion
}
/*

AI Rules for war simulation:

    "TURN BASED"

    Turns will last a set amount of time.

    ->Star System will supply ships(Strenght) overtime.
        -Star System have varying production.
        -Better System will be better defended.
        -Star system produce ArmyGroups. The overall strength of the group will be choosen randomly based on their
         type.

    ->EArmyGroupType defines the type of "Unit".
        -This affect's the unit's overall performance executing various tasks(invasion/hit and run/defence/etc.)
        -The type can be changed but takes time and while waiting makes the unit more vulnerable(Reorganizing)
        -Each types have varying disctances they can cover without resupply(stopping at the base).
        


*/
