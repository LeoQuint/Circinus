//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShipHelper {

    #region Static
    public static ShipComponent AddShipComponent(this GameObject g, eShipComponent component)
    {        
        switch (component)
        {
            case eShipComponent.WEAPON_STATION:
                {
                    ShipComponent sc = g.AddComponent<WeaponStation>();
                    return sc;
                }

            case eShipComponent.ENGINE_DEFAULT:
                {
                    ShipComponent sc = g.AddComponent<EngineStation>();
                    return sc;
                }

            case eShipComponent.PILOTING_STATION_DEFAULT:
                {
                    ShipComponent sc = g.AddComponent<PilotingStation>();
                    return sc;
                }

            case eShipComponent.EMPTY:
                return null;
        }
        return null;
    }
    #endregion
}
