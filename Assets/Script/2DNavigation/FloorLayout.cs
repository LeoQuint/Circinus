//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FloorLayout : MonoBehaviour {

    [System.Serializable]
    public struct sLayout
    {
        public TileType[] Row;       

        public TileType this[int i]
        {
            get { return Row[i]; }
            set { Row[i] = value; }
        }
    }
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
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    [SerializeField]
    protected sLayout[] m_Layout;
    //test
    public PathFinder finder;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    private void Awake()
    {
        Init();
        finder.SetLayout(m_Layout);
    }

#if UNITY_EDITOR
    private bool m_IsClicking = false;
    protected void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            if (m_Layout != null && m_Layout.Length > 0)
            {
                Vector3 position = transform.position;
                for (int i = 0; i < m_Layout.Length; ++i)
                {
                    Vector3 iVector = (Vector3.right * i * SQUARE_SIZE);
                    for (int j = 0; j < m_Layout[i].Row.Length; ++j)
                    {
                        Gizmos.color = GetGizmosColor(m_Layout[i][j]);
                        Gizmos.DrawCube(position + iVector + (Vector3.up * j * SQUARE_SIZE), Vector3.one * SQUARE_SIZE);
                    }
                }
            }

            if (!m_IsClicking && Event.current.isMouse && Event.current.clickCount > 0)
            {
                m_IsClicking = true;
                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                AssignTypeAt(ray.GetPoint(0f));
            }
            else
            {
                m_IsClicking = false;
            }
        }
    }
#endif

    #endregion

    #region Public API
    public void Init()
    {
        BuildLayout();
    }

    public TileType[][] GetLayout()
    {
        TileType[][] layout = new TileType[m_Layout.Length][];
        for (int i =0; i < m_Layout.Length; ++i)
        {
            layout[i] = m_Layout[i].Row;
        }
        return layout;
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
                for (int y = 0; y < m_Layout[x].Row.Length; ++y)
                {
                    GameObject go = new GameObject(string.Format("Tile{0}{1}", x, y));
                    go.transform.parent = transform;
                    go.transform.localPosition = position + iVector + (Vector3.up * y * SQUARE_SIZE);
                    Tile tile = go.AddComponent<Tile>();
                    tile.Init(x, y, m_Layout[x][y], GetTileMaterial(m_Layout[x][y]));
                }
            }
        }
    }
    #endregion

    #region Private

#if UNITY_EDITOR
    private Color GetGizmosColor(TileType type)
    {
        switch (type)
        {
            case TileType.EMPTY:
                return new Color(1f,1f,1f,0.1f);

            case TileType.STEEL:
                return Color.grey;
        }

        return Color.magenta;
    }

    private Material GetTileMaterial(TileType type)
    {
        switch (type)
        {
            case TileType.EMPTY:
                return _TEMP_EMPTY;

            case TileType.STEEL:
                return _TEMP_STEEL;
        }

        return _TEMP_EMPTY;
    }

    [ContextMenu("RefreshArray")]
    private void RefreshArray()
    {
        sLayout[] newLayout = new sLayout[_Width];
        for (int i = 0; i < newLayout.Length; ++i)
        {
            newLayout[i].Row = new TileType[_Height];
            if (m_Layout != null && m_Layout.Length > i)
            {
                for (int j = 0; j < newLayout[i].Row.Length; ++j)
                {
                    if (m_Layout[i].Row != null && m_Layout[i].Row.Length > j)
                    {
                        newLayout[i][j] = m_Layout[i].Row[j];
                    }
                    else
                    {
                        newLayout[i][j] = TileType.EMPTY;
                    }
                }
            }
        }
        m_Layout = newLayout;
    }

    [ContextMenu("Set Array to Empty")]
    private void Empty()
    {
        sLayout[] newLayout = new sLayout[_Width];
        for (int i = 0; i < newLayout.Length; ++i)
        {
            newLayout[i].Row = new TileType[_Height];
        }
        m_Layout = newLayout;
    }

    private void AssignTypeAt(Vector3 worldPosition)
    {
        worldPosition -= transform.position;
        int x = (int)(worldPosition.x + (SQUARE_SIZE / 2f) );
        int y = (int)(worldPosition.y + (SQUARE_SIZE / 2f));

        if (x >= 0 && y >= 0 && m_Layout.Length > x && m_Layout[x].Row.Length > y)
        {
            m_Layout[x][y] = TileType.STEEL;
        }
    }
#endif


    #endregion
}
