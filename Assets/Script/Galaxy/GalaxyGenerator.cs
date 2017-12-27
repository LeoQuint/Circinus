//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyGenerator : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const string STAR_PREFAB_PATH = "Galaxy/Prefabs/Stars/Star";
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    [Range(0, 100)]
    public float _FillPercent;
    public int _Width;
    public int _Height;
    public float _Angle;

    public bool _DrawGizmos = false;

    public string _Seed;
    public bool _UseRandomSeed = false;
    [InEditorReadOnly]
    public int m_NumberOfStars;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private int[,] map;
    private Galaxy m_Galaxy;
    System.Type[] m_SystemTypes = { typeof(StarSystem), typeof(StarSystem.StarType) , typeof(Location)};

    private List<GameObject> mapList = new List<GameObject>();
    private Transform mapHolder;

    private float m_SquaredRadiusWidth;
    private float m_SquaredRadiusHeight;

    private int m_CachedWidth;
    private int m_CachedHeight;

    #region Unity API
    private void Start()
    {
        mapHolder = new GameObject("Map_Holder").transform;
        mapHolder.position = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Generate();
            SpawnGameObjects();
            m_Galaxy.GenerateRandomArmies();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGalaxy();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGalaxy();
        }
    }
    #endregion

    #region Public API
    public void SaveGalaxy()
    {        
        Serializer_Deserializer<Galaxy> sd = new Serializer_Deserializer<Galaxy>(m_Galaxy, Serializer_Deserializer<Galaxy>.SavedPath.GameData , "Galaxy", m_SystemTypes);
        sd.Save();
    }

    public void LoadGalaxy()
    {
        Debug.Log("Loading Galaxy");
        Serializer_Deserializer<Galaxy> sd = new Serializer_Deserializer<Galaxy>(m_Galaxy, Serializer_Deserializer<Galaxy>.SavedPath.GameData, "Galaxy", m_SystemTypes);
        m_Galaxy = sd.Load();
        
        foreach (GameObject g in mapList)
        {
            Destroy(g);
        }
        mapList.Clear();

        for (int x = 0; x < m_Galaxy.m_GalacticMap.Count; x++)
        {
            for (int y = 0; y < m_Galaxy.m_GalacticMap[x].Count; y++)
            {
                GameObject g = Instantiate(Resources.Load<GameObject>(STAR_PREFAB_PATH)) as GameObject;
                g.GetComponent<Renderer>().material.SetFloat("_Index", Random.Range(0f, 1f));
                g.GetComponent<Renderer>().material.SetFloat("_Brightness", Random.Range(2f, 5f));
                g.transform.position = m_Galaxy.m_GalacticMap[x][y].m_Position;
                g.transform.SetParent(mapHolder);
                mapList.Add(g);
            }
        }
    }

    public void Generate()
    {
        m_SquaredRadiusHeight = (_Height/2) * (_Height/2);
        m_SquaredRadiusWidth = (_Width/2) * (_Width/2);
        m_CachedHeight = _Height;
        m_CachedWidth = _Width;
        map = new int[_Width, _Height];
        m_NumberOfStars = 0;
        RandomFillMap();      
        SmoothMap();    
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private void RandomFillMap()
    {
        if (_UseRandomSeed)
        {
            _Seed = Time.time.ToString();
        }
        System.Random sudoRandom = new System.Random(_Seed.GetHashCode());

        for (int x = 0; x < _Width; ++x)
        {
            for (int y = 0; y < _Height; ++y)
            {
                if (x == 0 || x == _Width - 1 || y == 0 || y == _Height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (sudoRandom.Next(0, 100) < _FillPercent) ? 1 : 0;
                }

            }
        }
    }

    private void SmoothMap()
    {
        for (int x = 0; x < _Width; x++)
        {
            for (int y = 0; y < _Height; y++)
            {
                if (map[x, y] == 1)
                {
                    if (IsInEllipse(x, y))//Check is the location fits in the ellipse.
                    {
                        //Check if there is any other neighbour. Remove if any.
                        int neighbourWallTiles = GetSurroundingWallCount(x, y);

                        if (neighbourWallTiles > 0)
                        {
                            map[x, y] = 0;
                        }
                        else
                        {
                            ++m_NumberOfStars;
                        }
                    }
                    else
                    {
                        map[x, y] = 0;
                    }                    
                }               
            }
        }
    }

    private int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < _Width && neighbourY >= 0 && neighbourY < _Height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }

    private void OnDrawGizmos()
    {
        if (map == null)
        {
            return;
        }
        if (_DrawGizmos)
        {
            for (int x = 0; x < m_CachedWidth; ++x)
            {
                for (int y = 0; y < m_CachedHeight; ++y)
                {
                    if (map[x, y] == 1)
                    {
                        Gizmos.color = Color.red;
                        Vector3 pos = new Vector3(-m_CachedWidth / 2 + x + 0.5f, -m_CachedHeight / 2 + y, -2f);
                        pos = new Vector3(pos.x * Mathf.Cos(_Angle) - pos.y * Mathf.Sin(_Angle), pos.y * Mathf.Cos(_Angle) - pos.x * Mathf.Sin(_Angle), pos.z);
                        Gizmos.DrawCube(pos, Vector3.one);
                    }                    
                }
            }
        }
    }

    private void SpawnGameObjects()
    {
        foreach (GameObject g in mapList)
        {
            Destroy(g);
        }
        mapList.Clear();

        m_Galaxy = new Galaxy(map, RepositioningFunction);

        for (int x = 0; x < _Width; x++)
        {
            for (int y = 0; y < _Height; y++)
            {
                if (map[x, y] == 0)
                {
                    continue;
                }
                GameObject g = Instantiate(Resources.Load<GameObject>(STAR_PREFAB_PATH))as GameObject;
                g.GetComponent<Renderer>().material.SetFloat("_Index", Random.Range(0f, 1f)); 
                g.GetComponent<Renderer>().material.SetFloat("_Brightness", Random.Range(2f, 5f));
                g.transform.position = RepositioningFunction(new Vector3(x,y,0f));
                g.transform.SetParent(mapHolder);
                mapList.Add(g);
            }
        }
       
    }

    private bool IsInEllipse(int x, int y)
    {
        x -= _Width / 2;
        y -= _Height / 2;
        return (float)(x*x)/m_SquaredRadiusWidth + (float)(y* y)/ m_SquaredRadiusHeight <= 1f;
    }

    private Vector3 RepositioningFunction(Vector3 startPos)
    {
        Vector3 pos = new Vector3(-_Width / 2 + startPos.x + 0.5f, -_Height / 2 + startPos.y, -2f);
        return new Vector3(pos.x * Mathf.Cos(_Angle) - pos.y * Mathf.Sin(_Angle), pos.y * Mathf.Cos(_Angle) - pos.x * Mathf.Sin(_Angle), pos.z);
    }
    #endregion
}
