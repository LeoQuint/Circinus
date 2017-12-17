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
[XmlRoot("Army")]
public class Army {

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
    public List<ArmyUnit> m_Army = new List<ArmyUnit>();
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    public int Strength
    {
        get
        {
            int strength = 0;
            for (int i = 0; i < m_Army.Count; ++i)
            {
                strength += m_Army[i].Strength;
            }
            return strength;
        }
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
