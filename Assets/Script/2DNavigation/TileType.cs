//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType {

	EMPTY = 0,

    OUTER_WALL = 2,
    INNER_WALL = 3,

    DOOR = 7,


    STEEL = 10

}

public class TileUtilities
{
    private static readonly List<TileType> WALKABLE_TILES = new List<TileType>()
    {
        TileType.STEEL, TileType.DOOR
    };

    private static readonly List<TileType> UNWALKABLE_TILES = new List<TileType>()
    {
        TileType.EMPTY, TileType.INNER_WALL, TileType.OUTER_WALL
    };

    private static readonly List<TileType> FLAMMABLE_TILES = new List<TileType>()
    {
        TileType.STEEL, TileType.DOOR
    };

    private static readonly Dictionary<TileType, float> TileHealthMap = new Dictionary<TileType, float>()
    {
        { TileType.EMPTY , 0f},
        { TileType.DOOR , 5f},
        { TileType.INNER_WALL , 25f},
        { TileType.OUTER_WALL , 100f},
        { TileType.STEEL , 100f}
    };

    public static bool IsFlammable(TileType type)
    {
        return FLAMMABLE_TILES.Contains(type);
    }

    public static bool IsWalkable(TileType type)
    {
        return !UNWALKABLE_TILES.Contains(type);
    }

    public static float TileHealth(TileType type)
    {
        return TileHealthMap[type];
    }
}
