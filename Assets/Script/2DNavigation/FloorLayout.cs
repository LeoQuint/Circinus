//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FloorLayout : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    public const float SQUARE_SIZE = 1f;
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////     
    [SerializeField] protected int _Width;
    [SerializeField] protected int _Height;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    public Material _TEMP_EMPTY;
    public Material _TEMP_STEEL;
    public Material _TEMP_INNER_WALL;
    public Material _TEMP_OUTER_WALL;

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////    
    protected sTileInfo[][] m_Layout;
    //test
    public PathFinder finder;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    #endregion

    #region Public API
    public void Init(ShipLayout layout)
    {
        m_Layout = layout.GetLayout();
        finder.SetLayout(m_Layout);
        BuildLayout();
    }   
    #endregion

    #region Protect
    protected void BuildLayout()
    {
        if (m_Layout != null && m_Layout.Length > 0)
        {
            Vector3 position = transform.position;
            for (int x = 0; x < m_Layout.Length; ++x)
            {
                Vector3 iVector = (Vector3.right * x * SQUARE_SIZE);
                for (int y = 0; y < m_Layout[x].Length; ++y)
                {
                    GameObject go = new GameObject(string.Format("Tile{0}{1}", x, y));
                    go.transform.parent = transform;
                    go.transform.localPosition = position + iVector + (Vector3.up * y * SQUARE_SIZE);
                    Tile tile = go.AddComponent<Tile>();
                    tile.Init(m_Layout[x][y], GetTileMaterial(m_Layout[x][y].Type));
                }
            }
        }
    }
    #endregion

    #region Private
    private Material GetTileMaterial(TileType type)
    {
        switch (type)
        {
            case TileType.EMPTY:
                return _TEMP_EMPTY;

            case TileType.INNER_WALL:
                return _TEMP_INNER_WALL;

            case TileType.OUTER_WALL:
                return _TEMP_OUTER_WALL;

            case TileType.STEEL:
                return _TEMP_STEEL;
        }

        return _TEMP_EMPTY;
    }
    #endregion
}
