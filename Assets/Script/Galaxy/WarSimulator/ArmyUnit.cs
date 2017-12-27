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
/// Represents 1 unit type wittin an ArmyGroup.
/// </summary>
[System.Serializable]
[XmlRoot("ArmyUnit")]
public class ArmyUnit {

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
    [XmlElement("Number")]
    public int m_Number;
    [XmlElement("ShipClass")]
    public EShipClass m_ShipClass;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    ///Properties
    ///
    public int Strength
    {
        get { return (int)m_ShipClass * m_Number; }
    }

    ///Constructors
    ///
    public ArmyUnit() { }
    public ArmyUnit(EFaction faction, EShipClass shipClass, int number)
    {
        m_Faction = faction;
        m_ShipClass = shipClass;
        m_Number = number;
    }

    #region Unity API
    #endregion

    #region Public API
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
