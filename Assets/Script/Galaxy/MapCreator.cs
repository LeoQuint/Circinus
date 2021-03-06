﻿//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [Range(0, 100)]
    [SerializeField] int fillPercent;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] bool _DrawGizmos = false;
    [SerializeField] bool _UseRandomSeed = false;
    [SerializeField] string _seed;
    [SerializeField] int _smoothFactor = 5;
    [SerializeField] int _AccretionFactor = 5;
    [SerializeField] int _borderSize = 5;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private int[,] map;
    private int[,] accretionMap;

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
    }
    #endregion

    #region Public API
    public void Generate()
    {
        map = new int[width, height];
        RandomFillMap();
        for (int i = 0; i < _smoothFactor; ++i)
        {
            SmoothMap();
        }

        for (int i = 0; i < _AccretionFactor; ++i)
        {
            GenerateAccretion();
        }

        SpawnGameObjects();

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

    private void GenerateAccretion()
    {
        accretionMap  = new int[width, height];
        int[] mainCenter = new int[9];
        //Generate the map of centers.
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);
                accretionMap[x, y] = neighbourWallTiles;
                ++mainCenter[neighbourWallTiles];
            }
        }

        for (int i = mainCenter.Length-1; i > 0; --i)
        {
            Debug.Log(i + ": " + mainCenter[i]);
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
                else
                {
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

                    Gizmos.color = map[x, y] == 1 ? Color.black : Color.red;
                    Vector3 pos = new Vector3(-width / 2 + x + 0.5f, -height / 2 + y, -2f);
                    Gizmos.DrawCube(pos, Vector3.one);
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
                //Debug.Log("Making cube: " + pos);
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

