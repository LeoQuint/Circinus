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
using War;

[System.Serializable]
[XmlRoot("StarSystem")]
public class StarSystem {

    [System.Serializable]
    [XmlRoot("StarType")]
    public struct StarType
    {
        //Hotness O,B,A,F,G,K,M : O (Hotest) M (Coolest), Also(D = White Dwarf, S = Sub Dwarf)
        public Temperature TemperatureCode;
        public Luminosity Luminosity;

        public StarType(Temperature temperature, Luminosity luminosity)
        {
            TemperatureCode = temperature;
            Luminosity = luminosity;
        }
        public StarType(bool isRandomed)
        {
            if (isRandomed)
            {
                TemperatureCode = (Temperature)UnityEngine.Random.Range(0, (int)Temperature.COUNT);
                Luminosity = (Luminosity)UnityEngine.Random.Range(0, (int)Luminosity.COUNT);
            }
            else
            {
                TemperatureCode = Temperature.O;
                Luminosity = Luminosity.Nulla;
            }
        }
    }

    public enum Luminosity
    {
        Nulla = 0,
        Ia = 1,
        Ib = 2,
        I = 3,
        II = 4,
        III = 5,
        IV = 6,
        V = 7,
        VI = 8,
        VII = 9,
        COUNT = 10
    }

    public enum Temperature
    {
        O,
        B,
        A,
        F,
        G,
        K,
        M,
        COUNT
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
    [XmlElement("Index")]
    public int m_Index;
    [XmlElement("StarType")]
    public StarType m_StarType;
    [XmlElement("Position")]
    public Vector3 m_Position;
    [XmlElement("Locations")]
    public List<Location> m_Locations;
    [XmlElement("LocalForces")]
    public List<ArmyGroup> m_LocalForces;
    [XmlElement("ControllingFaction")]
    public EFaction m_ControllingFaction;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private EArmyGroupType m_CurrentArmyGroupProduced;
    private int m_CurrentProductionCompleted;
    private int m_CurrentProductionCost;
    //Properties

    public bool HasProductionInProgress
    {
        get { return m_CurrentArmyGroupProduced != EArmyGroupType.None; }
    }

    public int TurnsRemainingForProduction
    {
        get { return Mathf.CeilToInt((m_CurrentProductionCompleted - m_CurrentProductionCost) / StrengthProduction()); }
    }

    public int LocalForcesStrength
    {
        get
        {
            int strength = 0;
            for (int i = 0; i < m_LocalForces.Count; ++i)
            {
                strength += m_LocalForces[i].Strength;
            }
            return strength;
        }
    }

    //Constructors
    public StarSystem(StarType type, Vector3 position, int index)
    {
        m_Index = index;
        m_Position = position;
        m_StarType = type;
        m_LocalForces = new List<ArmyGroup>();
    }

    public StarSystem(StarType type, Vector3 position, int x, int y)
    {
        m_Index = (x*10000) + y;
        m_Position = position;
        m_StarType = type;
        m_LocalForces = new List<ArmyGroup>();
    }

    public StarSystem(bool isEmpty)
    {
        if (isEmpty)
        {
            m_StarType = new StarType(Temperature.O, Luminosity.Nulla);
        }
        m_LocalForces = new List<ArmyGroup>();
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

    public int StrengthProduction()
    {
        int production = 0;
        for (int i = 0; i < m_Locations.Count; ++i)
        {
            production += m_Locations[i].StrengthProduction;
        }
        return production;
    }

    public void AssignProduction(EArmyGroupType type, int strength)
    {
        m_CurrentArmyGroupProduced = type;
        m_CurrentProductionCost = strength;
        m_CurrentProductionCompleted = 0;
    }

    public void BeginTurn()
    {
        ExecuteProduction();
    }
    /// <summary>
    /// Used for initialization or if a system is flipped peacefully.
    /// </summary>
    public void SetControllingFaction(EFaction faction)
    {
        m_ControllingFaction = faction;
        for (int i = 0; i < m_LocalForces.Count; ++i)
        {
            m_LocalForces[i].ChangeFaction(faction);
        }
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private void ExecuteProduction()
    {
        if (m_CurrentArmyGroupProduced != EArmyGroupType.None)
        {
            m_CurrentProductionCompleted += StrengthProduction();
            if (m_CurrentProductionCompleted >= m_CurrentProductionCost)
            {
                ArmyGroup ag = ArmyConfigInterface.instance.GenerateArmyGroup(m_ControllingFaction, m_CurrentArmyGroupProduced, m_CurrentProductionCost);
                m_LocalForces.Add(ag);
                m_CurrentProductionCompleted = 0;
                m_CurrentArmyGroupProduced = EArmyGroupType.None;
                m_CurrentProductionCost = 0;
            }
        }
    }
    #endregion
}
