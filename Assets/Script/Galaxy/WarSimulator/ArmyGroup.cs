//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using War;

/// <summary>
/// Grouping of units that move/behave as a group.
/// </summary>
[System.Serializable]
[XmlRoot("ArmyGroup")]
public class ArmyGroup {

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
    [XmlElement("Faction")]
    public EFaction m_Faction;
    [XmlElement("ArmyGroupType")]
    public EArmyGroupType m_Type;
    [XmlElement("ArmyUnits")]
    public List<ArmyUnit> m_ArmyUnits = new List<ArmyUnit>();

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    ///Properties
    public int Strength
    {
        get
        {
            int strength = 0;
            for (int i = 0; i < m_ArmyUnits.Count; ++i)
            {
                strength += m_ArmyUnits[i].Strength;
            }
            return strength;
        }
    }

    //Constructors
    public ArmyGroup() { }
    public ArmyGroup(EArmyGroupType type)
    {
        m_Type = type;
    }

    #region Unity API
    #endregion

    #region Public API
    public void ChangeFaction(EFaction faction)
    {
        m_Faction = faction;
    }
    /// <summary>
    /// Add a single Ship.
    /// </summary>
    /// <param name="ship"></param>
    public void Add(EShipClass ship)
    {
        for (int i = 0; i < m_ArmyUnits.Count; ++i)
        {
            if (m_ArmyUnits[i].m_ShipClass == ship)
            {
                ++m_ArmyUnits[i].m_Number;
                return;
            }
        }
        m_ArmyUnits.Add(new ArmyUnit(ship, 1));
    }
    /// <summary>
    /// Add a one or more ships of the same type.
    /// </summary>
    /// <param name="ships"></param>
    public void Add(ArmyUnit ships)
    {
        for (int i = 0; i < m_ArmyUnits.Count; ++i)
        {
            if (m_ArmyUnits[i].m_ShipClass == ships.m_ShipClass)
            {
                m_ArmyUnits[i].m_Number += ships.m_Number;
                return;
            }
        }
        m_ArmyUnits.Add(ships);
    }

    /// <summary>
    /// Merge 2 army group togeter.
    /// </summary>
    /// <param name="group"></param>
    public void Add(ArmyGroup group)
    {
        for (int i = 0; i < group.m_ArmyUnits.Count; ++i)
        {
            Add(group.m_ArmyUnits[i]);
        }
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
