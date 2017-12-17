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
    private List<Army> m_Armies = new List<Army>();

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

    #region Unity API
    #endregion

    #region Public API
    public void CalculateNextMove()
    {

    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
