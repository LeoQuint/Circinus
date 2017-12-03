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

    [Range(0, 100)]
    public int fillPercent;
    public int width;
    public int height;

    public bool _DrawGizmos = false;

    public string _seed;
    public bool _UseRandomSeed = false;

    public int _smoothFactor = 5;
    public int _borderSize = 5;


    int[,] map;

    private List<GameObject> mapList = new List<GameObject>();
    private Transform mapHolder;

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
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGalaxy();
        }
    }
    #endregion

    #region Public API
    public void SaveGalaxy()
    {
    }

    public void Generate()
    {
        map = new int[width, height];
        RandomFillMap();
        for (int i = 0; i < _smoothFactor; ++i)
        {
            SmoothMap();
        }

        //SpawnGameObjects();

        int[,] borderedMap = new int[width + _borderSize * 2, height + _borderSize * 2];


        for (int x = 0; x < borderedMap.GetLength(0); ++x)
        {
            for (int y = 0; y < borderedMap.GetLength(1); ++y)
            {
                if (x >= _borderSize && x < width + _borderSize && y >= _borderSize && y < height + _borderSize)
                {
                    borderedMap[x, y] = map[x - _borderSize, y - _borderSize];
                }
                else
                {
                    borderedMap[x, y] = 1;
                }
            }
        }
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private void RandomFillMap()
    {
        if (_UseRandomSeed)
        {
            _seed = Time.time.ToString();
        }
        System.Random sudoRandom = new System.Random(_seed.GetHashCode());

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (sudoRandom.Next(0, 100) < fillPercent) ? 1 : 0;
                }

            }
        }
    }

    private void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                {
                    map[x, y] = 1;
                }
                else if (neighbourWallTiles < 4)
                {
                    map[x, y] = 0;
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
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
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
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    if (map[x, y] == 1)
                    {
                        Gizmos.color = Color.red;
                        Vector3 pos = new Vector3(-width / 2 + x + 0.5f, -height / 2 + y, -2f);
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
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 0)
                {
                    continue;
                }

                Vector3 pos = new Vector3(-width / 2 + x + 0.5f, -height / 2 + y, -2f);
                GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                g.GetComponent<Renderer>().material.color = Color.red;
                g.transform.position = pos;
                g.transform.SetParent(mapHolder);
                mapList.Add(g);
            }
        }
    }
    #endregion
}
