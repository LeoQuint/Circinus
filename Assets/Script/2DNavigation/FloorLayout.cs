//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class FloorLayout : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    public const float SQUARE_SIZE = 1f;
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    public static SpriteTileData SpriteData;
    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////     

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    public string LayoutName;
    public SpriteTileData data;
    public PathFinder m_PathFinder;
    ////////////////////////////////
    ///			Protected		 ///
    //////////////////////////////// 
    protected LayoutData m_Data;   
    protected TileInfo[][] m_Layout;
    protected Tile[][] m_Tiles;    
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private Ship m_Ship;

    public int Width
    {
        get
        {
            if (m_Tiles != null)
            {
                return m_Tiles.Length;
            }
            return 0;
        }
    }

    public int Height
    {
        get
        {
            if (m_Tiles != null && m_Tiles.Length > 0)
            {
                return m_Layout[0].Length;
            }
            return 0;
        }
    }

    public Tile[] this[int x]
    {
        get
        {
            return m_Tiles[x];
        }
    }
    #region Unity API
    #endregion

    #region Public API
    public void Init(ShipLayout layout, Ship ship)
    {
        m_Ship = ship;
        SpriteData = data;
        SpriteData.Init();
        LoadLayoutData();
        if(m_Data != null)
        {
            m_Layout = m_Data.GetLayout();
            console.logStatus("Loading layout from data");
        }
        else
        {
            m_Layout = layout.GetLayout();
            console.logWarning("Loading layout from defaults");
        }       
       
        m_PathFinder.SetLayout(m_Layout);
        BuildLayout();
    }

    public List<Tile> GetNeighbours(Vector2Int position, bool includeDiagonal = false)
    {
        List<Tile> neighbours = new List<Tile>();

        if (includeDiagonal)
        {
            Debug.LogError("Diagonal neighbours not yet implemented.");
        }

        if (m_Tiles[0].Length > position.y + 1)//up
        {
            neighbours.Add(m_Tiles[position.x][position.y + 1]);
        }

        if (position.y > 0)//down
        {
            neighbours.Add(m_Tiles[position.x][position.y - 1]);
        }

        if (position.x > 0)//left
        {
            neighbours.Add(m_Tiles[position.x - 1][position.y]);
        }

        if (m_Tiles.Length > position.x + 1)//right
        {
            neighbours.Add(m_Tiles[position.x + 1][position.y]);
        }

        return neighbours;
    }

  
    #endregion

    #region Protect
    protected void BuildLayout()
    {
        if (m_Layout != null && m_Layout.Length > 0)
        {
            transform.localPosition = new Vector3((SQUARE_SIZE * m_Layout.Length) / -2f, (SQUARE_SIZE * m_Layout[0].Length) / -2f, 0f);
           
            m_Tiles = new Tile[m_Layout.Length][];
            for (int x = 0; x < m_Layout.Length; ++x)
            {
                Vector3 iVector = (Vector3.right * x * SQUARE_SIZE);
                m_Tiles[x] = new Tile[m_Layout[x].Length];
                for (int y = 0; y < m_Layout[x].Length; ++y)
                {
                    GameObject go = new GameObject(string.Format("Tile{0}{1}", x, y));
                    go.transform.parent = transform;
                    go.transform.localPosition = iVector + (Vector3.up * y * SQUARE_SIZE);
                    Tile tile = go.AddComponent<Tile>();
                    tile.Init(m_Layout[x][y], m_Ship);
                    m_Tiles[x][y] = tile;
                }
            }
        }
    }
    #endregion

    #region Private
    private void LoadLayoutData()
    {
        if (m_Ship.Data != null)
        {
            m_Data = LayoutData.Load(m_Ship.Data.Name);
        }
        else
        {
            m_Data = null;
        }
    }

    [ContextMenu("Save Layout")]
    private void SaveLayout()
    {
        if (m_Data == null)
        {
            m_Data = new LayoutData();
            m_Data.Name = m_Ship.Data.Name;
            m_Data.Layout = new List<List<TileInfo>>();
        }

        m_Data.Layout.Clear();

        for (int x = 0; x < m_Layout.Length; ++x)
        {
            List<TileInfo> infoList = new List<TileInfo>();           
            for (int y = 0; y < m_Layout[0].Length; ++y)
            {
                infoList.Add(m_Layout[x][y]);
            }
            m_Data.Layout.Add(infoList);
        }

        LayoutData.Save(m_Data);
    }
    #endregion
}