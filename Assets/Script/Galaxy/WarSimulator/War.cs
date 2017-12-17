//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace War {

    public enum EFaction
    {
        Red,
        Blue,
        Neutral
    }
    /// <summary>
    /// ShipClass by size
    /// Their enum values are also their overall strengh.
    /// </summary>
    public enum EShipClass
    {
        Scout = 10,
        PatrolShip = 15,
        Corvette = 25,
        Frigate = 50,
        Destroyer = 150,
        LightCruiser = 200,
        HeavyCruiser = 250,
        Battlecruiser = 400,
        Battleship = 600,
        LightCarrier = 1200,
        HeavyCarrier = 2000
    }
}
