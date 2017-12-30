//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using War;

[System.Serializable]
[XmlRoot("Faction")]
public class Faction {

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

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////    
    private EFaction m_Faction;
    private List<ArmyGroup> m_Armies = new List<ArmyGroup>();
    private List<StarSystem> m_ControlledSystems = new List<StarSystem>();

    /// Properties
    /// 
    public EFaction Side
    {
        get { return m_Faction; }
    }

    public int Strength
    {
        get
        {
            int strength = 0;
            for (int i = 0; i < m_Armies.Count; ++i)
            {
                strength += m_Armies[i].Strength;
            }
            return strength;
        }
    }

    ///Constructors
    ///
    public Faction() { }

    public Faction(EFaction side)
    {
        m_Faction = side;
    }

    #region Unity API
    #endregion

    #region Public API
    public void Load(List<ArmyGroup> armies, List<StarSystem> starSystems)
    {
        m_Armies = new List<ArmyGroup>(armies);
        m_ControlledSystems = new List<StarSystem>(starSystems);
    }

    public void TakeTurn()
    {
        ExecuteProduction();
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private void ExecuteProduction()
    {
        for (int i = 0; i < m_ControlledSystems.Count; ++i)
        {
            m_ControlledSystems[i].BeginTurn();
        }
    }
    #endregion
}
