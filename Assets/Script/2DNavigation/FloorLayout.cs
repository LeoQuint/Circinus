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
    public static SpriteTileData SpriteData;
    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////     
    [SerializeField] protected int _Width;
    [SerializeField] protected int _Height;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    public SpriteTileData data;

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////    
    protected sTileInfo[][] m_Layout;
    protected Tile[][] m_Tiles;
    //test
    public PathFinder finder;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////


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
    #region Unity API
    #endregion

    #region Public API
    public void Init(ShipLayout layout)
    {
        SpriteData = data;
        SpriteData.Init();
        m_Layout = layout.GetLayout();
        finder.SetLayout(m_Layout);
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
            Vector3 position = transform.position;
            m_Tiles = new Tile[m_Layout.Length][];
            for (int x = 0; x < m_Layout.Length; ++x)
            {
                Vector3 iVector = (Vector3.right * x * SQUARE_SIZE);
                m_Tiles[x] = new Tile[m_Layout[x].Length];
                for (int y = 0; y < m_Layout[x].Length; ++y)
                {
                    GameObject go = new GameObject(string.Format("Tile{0}{1}", x, y));
                    go.transform.parent = transform;
                    go.transform.localPosition = position + iVector + (Vector3.up * y * SQUARE_SIZE);
                    Tile tile = go.AddComponent<Tile>();
                    tile.Init(m_Layout[x][y]);
                    m_Tiles[x][y] = tile;
                }
            }
        }
    }
    #endregion

    #region Private
    #endregion
}
