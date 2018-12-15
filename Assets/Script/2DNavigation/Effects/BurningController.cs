//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningController : ShipComponent {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const int FIRE_ANIMATION_POOL_SIZE = 5;
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
    private FloorLayout m_Layout;
    private int[][] m_FireRank;//How hard is the fire burning
    private int[][] m_IgnitionRank;//how close is this from catching fire.
    private List<Animation> m_ActiveFireAnimations = new List<Animation>();
    private List<Animation> m_PooledFireAnimations = new List<Animation>();

    #region Unity API
    #endregion

    #region Public API
    public void Init()
    {


        m_FireRank = new int[m_Layout.Width][];
        m_IgnitionRank = new int[m_Layout.Width][];
        for (int i = 0; i < m_Layout.Width; ++i)
        {
            m_FireRank[i] = new int[m_Layout.Height];
            m_IgnitionRank[i] = new int[m_Layout.Height];
        }
    }

    public void AddFireRank(Vector2Int location, int amount)
    {
        if (location.x < 0 || location.x >= m_FireRank.Length ||
            location.y < 0 || location.y >= m_FireRank[0].Length)
        {
            Debug.LogError("Trying to add Fire to a Location outside this Layout.");
            return;
        }        
    }

    public void AddIgnitionRank(Vector2Int location, int amount)
    {
        if (location.x < 0 || location.x >= m_FireRank.Length ||
           location.y < 0 || location.y >= m_FireRank[0].Length)
        {
            Debug.LogError("Trying to Ignite a Location outside this Layout.");
            return;
        }
    }

    public bool IsBurning(Vector2Int location)
    {
        return m_FireRank[location.x][location.y] > 0;
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
