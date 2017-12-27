//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;

[System.Serializable]
[XmlRoot("Galaxy")]
public class Galaxy {

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
    public List<List<StarSystem>> m_GalacticMap;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    //Properties

    //Constructor
    public Galaxy(int[,] locations, Func<Vector3, Vector3> RepositionFunction = null)
    {
        m_GalacticMap = new List<List<StarSystem>>();

        for (int i = 0; i < locations.GetLength(0); i++)
        {
            m_GalacticMap.Add(new List<StarSystem>());

            for (int j = 0; j < locations.GetLength(1); j++)
            {
                if (locations[i, j] == 1)
                {
                    if (RepositionFunction != null)
                    {
                        m_GalacticMap[i].Add(new StarSystem(new StarSystem.StarType(true), RepositionFunction(new Vector3(i, j, 0f)), i, j));
                    }
                    else
                    {
                        m_GalacticMap[i].Add(new StarSystem(new StarSystem.StarType(true), new Vector3(i, j, 0f), i, j));
                    }
                }                
            }                
        }
    }

    public Galaxy()
    {

    }

    #region Unity API
    #endregion

    #region Public API
    public StarSystem GetStarSystemAtIndex(int x, int y)
    {
        return m_GalacticMap[x][y];
    }

    public void SetStarSystem(StarSystem system, int x, int y)
    {
        m_GalacticMap[x][y] = system;
    }

    public void SetStarType(StarSystem.StarType type, int x, int y)
    {
        m_GalacticMap[x][y].SetStarType(type);
    }

    public void RenderLocation(int x, int y)
    {

    }

    public void GenerateRandomArmies()
    {
        for (int i = 0; i < m_GalacticMap.Count; ++i)
        {
            for (int j = 0; j < m_GalacticMap[i].Count; ++j)
            {
                AssignArmyTo(ArmyConfigInterface.instance.GenerateArmyGroup(War.EFaction.Red, War.EArmyGroupType.MainCorp, 1000000), i, j);
            }
        }
    }

    public void AssignArmyTo(ArmyGroup armyGroup, int x, int y)
    {
        m_GalacticMap[x][y].m_LocalForces.Add(armyGroup);
    }
    #endregion

    #region Protect
    #endregion

    #region Private

    #endregion
}
