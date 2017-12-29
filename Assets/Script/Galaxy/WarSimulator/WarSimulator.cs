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

    private EFaction m_CurrentTurn;
    private List<EFaction> m_Factions;

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
    #endregion

    #region Public API
    public void Initialize()
    {

    }

    public void Load()
    {

    }

    public void Save()
    {

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
