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

    STEEL = 10

}

public class TileUtilities
{
    private static readonly List<TileType> WALKABLE_TILES = new List<TileType>()
    {
        TileType.STEEL
    };

    private static readonly List<TileType> UNWALKABLE_TILES = new List<TileType>()
    {
        TileType.EMPTY, TileType.INNER_WALL, TileType.OUTER_WALL
    };

    public static bool IsWalkable(TileType type)
    {
        return !UNWALKABLE_TILES.Contains(type);
    }
}
