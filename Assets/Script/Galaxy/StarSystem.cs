//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Xml.Serialization;


[System.Serializable]
[XmlRoot("StarSystem")]
public class StarSystem {

    [System.Serializable]
    [XmlRoot("StarType")]
    public struct StarType
    {
        //Hotness O,B,A,F,G,K,M : O (Hotest) M (Coolest), Also(D = White Dwarf, S = Sub Dwarf)
        public char TemperatureCode;
        //Sub Hotness Zero to Nine: 0 (Hotest) 9 (Coolest)
        public int SubTemperatureCode;
        //Luminosity Nulla, Ia, Ib, I, II, III, IV, V, VI, VII
        public string Luminosity;

        public StarType(char temperature, int subTemperature, string luminosity)
        {
            TemperatureCode = temperature;
            SubTemperatureCode = subTemperature;
            Luminosity = luminosity;
        }
        public StarType(bool isRandomed)
        {
            if (isRandomed)
            {
                TemperatureCode = TEMPERATURE_CODES[UnityEngine.Random.Range(0, TEMPERATURE_CODES.Count)];
                SubTemperatureCode = SUB_TEMPERATURE_CODES[UnityEngine.Random.Range(0, SUB_TEMPERATURE_CODES.Count)];
                Luminosity = LUMINOSITY_CODES[UnityEngine.Random.Range(0, LUMINOSITY_CODES.Count)];
            }
            else
            {
                TemperatureCode = 'O';
                SubTemperatureCode = 0;
                Luminosity = "Nulla";
            }
        }
    }

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    public static readonly List<char> TEMPERATURE_CODES = new List<char>()
    {
        'N'/*NULL*/,'O', 'B', 'A', 'F', 'G', 'K', 'M'
    };
    public static readonly List<int> SUB_TEMPERATURE_CODES = new List<int>()
    {
        -1/*NULL*/,0,1,2,3,4,5,6,7,8,9
    };
    public static readonly List<string> LUMINOSITY_CODES = new List<string>()
    {
        "Nulla"/*NULL*/, "Ia", "Ib", "I", "II", "III", "IV", "V", "VI", "VII"
    };
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    public int m_Index;
    public StarType m_StarType;
    public Vector3 m_Position;
    public List<Location> m_Locations;
    public ArmyGroup m_LocalForces;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    //Properties

    //Constructors
    public StarSystem(StarType type, Vector3 position, int index)
    {
        m_Index = index;
        m_Position = position;
        m_StarType = type;
        m_LocalForces = new ArmyGroup();
    }

    public StarSystem(StarType type, Vector3 position, int x, int y)
    {
        m_Index = (x*10000) + y;
        m_Position = position;
        m_StarType = type;
        m_LocalForces = new ArmyGroup();
    }

    public StarSystem(bool isEmpty)
    {
        if (isEmpty)
        {
            m_StarType = new StarType(TEMPERATURE_CODES[0], SUB_TEMPERATURE_CODES[0], LUMINOSITY_CODES[0]);
        }
        m_LocalForces = new ArmyGroup();
    }

    public StarSystem()
    {
    }

    #region Unity API
    #endregion

    #region Public API    
    public StarType GetStarType()
    {
        return m_StarType;
    }

    public void SetStarType(StarType type)
    {
        m_StarType = type;
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
