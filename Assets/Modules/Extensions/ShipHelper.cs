//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShipHelper {

    #region Static
    public static ShipComponent AddShipComponent(this GameObject g, eShipComponent component, Tile tile)
    {
        ShipComponent sc = null;
        switch (component)
        {
            case eShipComponent.WEAPON_STATION:
                {
                    sc = g.AddComponent<WeaponStation>();
                    break;
                }

            case eShipComponent.ENGINE_DEFAULT:
                {
                    sc = g.AddComponent<EngineStation>();
                    break;
                }

            case eShipComponent.PILOTING_STATION_DEFAULT:
                {
                    sc = g.AddComponent<PilotingStation>();
                    break;
                }

            case eShipComponent.EMPTY:
                return null;
        }

        if (sc != null)
        {
            sc.Tile = tile;
        }

        return sc;
    }
    #endregion
}
