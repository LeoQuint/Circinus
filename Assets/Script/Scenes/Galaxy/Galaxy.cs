//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

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
    public List<List<StarSystem>> m_Galaxy;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    //Properties

    //Constructor
    public Galaxy(int[,] locations)
    {
        int locX = locations.GetLength(0);
        int locY = locations.GetLength(1);
        m_Galaxy = new List<List<StarSystem>>();

        for (int i = 0; i < locations.GetLength(0); i++)
        {
            m_Galaxy.Add(new List<StarSystem>());

            for (int j = 0; j < locations.GetLength(1); j++)
            {
                m_Galaxy[i].Add(locations[i, j] == 1? new StarSystem(): new StarSystem(true));
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
        return m_Galaxy[x][y];
    }

    public void SetStarSystem(StarSystem system, int x, int y)
    {
        m_Galaxy[x][y] = system;
    }

    public void SetStarType(StarSystem.StarType type, int x, int y)
    {
        m_Galaxy[x][y].SetStarType(type);
    }

    public void RenderLocation(int x, int y)
    {

    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
