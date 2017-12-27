//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using War;
using CoreUtility;

[System.Serializable]
[XmlRoot("ArmyConfiguration")]
public class ArmyConfig {

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
    public List<ItemPercentage<EShipClass>> ShipOccurence = new List<CoreUtility.ItemPercentage<EShipClass>>()
    {
        new ItemPercentage<EShipClass>
        (EShipClass.Scout,          1.5f),
        new ItemPercentage<EShipClass>
        (EShipClass.PatrolShip,     2.5f),
        new ItemPercentage<EShipClass>
        (EShipClass.Corvette,       2f),
        new ItemPercentage<EShipClass>
        (EShipClass.Destroyer,      1f),
        new ItemPercentage<EShipClass>
        (EShipClass.Frigate,        2f),
        new ItemPercentage<EShipClass>
        (EShipClass.LightCruiser,   0.5f),
        new ItemPercentage<EShipClass>
        (EShipClass.HeavyCruiser,   0.25f),
        new ItemPercentage<EShipClass>
        (EShipClass.Battlecruiser,  0.1f),
        new ItemPercentage<EShipClass>
        (EShipClass.Battleship,     0.05f),        
        new ItemPercentage<EShipClass>
        (EShipClass.LightCarrier,   0.02f),
        new ItemPercentage<EShipClass>
        (EShipClass.HeavyCarrier,   0.01f),
};
	////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

	////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    #endregion

    #region Public API
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
