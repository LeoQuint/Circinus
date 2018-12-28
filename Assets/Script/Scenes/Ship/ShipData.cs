//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;

[System.Serializable]
[XmlRoot("ShipData")]
public class ShipData {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const string FILENAME = "ShipData";
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    private static Type[] m_Types = new Type[1] { typeof(ShipData) };
    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    public string Name;
    //propulsion
    public float BaseAcceleration;
    public float BaseMaxSpeed;
    //steering
    public float BaseSteeringAccelerationSpeed;
    public float BaseSteeringMaxSpeed;
    //stats
    public float HullWeight;
    public float BaseHullHealth;

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    //properties
    public float Weight
    {
        get { return 0f; }
    }

    public float Acceleration
    {
        get { return 0f; }
    }

    public float SteeringSpeed
    {
        get { return 0f; }
    }

    #region Unity API
    #endregion

    #region Public API
    public static void Save(ShipData data)
    {
        console.logStatus("Saving " + FILENAME + "_" + data.Name);
        Serializer_Deserializer<ShipData>.Save(data, SavedPath.GameData, FILENAME + "_" + data.Name, m_Types);
    }

    public static ShipData Load(string shipName)
    {
        console.logStatus("Loading " + shipName);
        return Serializer_Deserializer<ShipData>.Load(SavedPath.GameData, FILENAME + "_" + shipName, m_Types);
    }
#if UNITY_EDITOR
    [UnityEditor.MenuItem("CreateConfigs/Create New ShipData")]
    static void CreateShipData()
    {        
        ShipData sd = new ShipData();
        sd.Name = "NEW_SHIPDATA";
        ShipData.Save(sd);
        console.log("Ship data template created");
    }
#endif

#endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
