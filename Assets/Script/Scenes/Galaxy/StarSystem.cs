//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class StarSystem {

    public struct StarType
    {
        //Hotness O,B,A,F,G,K,M : O (Hotest) M (Coolest), Also(D = White Dwarf, S = Sub Dwarf)
        public char TemperatureCode;
        //Sub Hotness Zero to Nine: 0 (Hotest) 9 (Coolest)
        public int SubTemperatureCode;
        //Luminosity Nulla, Ia, Ib, I, II, III, IV, V, VI, VII
        public string Luminosity;
    }

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
    protected StarType m_StarType;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    public StarType GetStartType
    {
        get { return m_StarType; }
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
