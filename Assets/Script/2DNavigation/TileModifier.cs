//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eShipComponent
{
    EMPTY = 0,
    //100-199
    PILOTING_STATION_DEFAULT = 100,
    //200-299
    ENGINE_DEFAULT = 200,
    //300-399
    WEAPON_STATION = 300
}

public enum eTileModifier
{
    NONE = 0,

    DAMAGED = 100,

    BROKEN = 200
}

[System.Serializable]
public struct sTileInfo
{
    public TileType Type;
    public Vector2Int Position;
    public eShipComponent Component;
    public List<eTileModifier> Modifiers;

    public sTileInfo(TileType type, int x, int y, eShipComponent component, List<eTileModifier> modifier)
    {
        Type = type;
        Position = new Vector2Int(x,y);
        Component = component;
        Modifiers = new List<eTileModifier>(modifier);
    }

    public sTileInfo(int x, int y)
    {
        Type = TileType.EMPTY;
        Position = new Vector2Int(x, y);
        Component = eShipComponent.EMPTY;
        Modifiers = new List<eTileModifier>();
    }
}