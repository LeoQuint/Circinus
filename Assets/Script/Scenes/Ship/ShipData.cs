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
    private const string FOLDER = "Ship";
    private const string FILENAME = "ShipData";
    private const char SEPERATOR = '_';
    private const string PATH_FORMAT = "{0}/{1}{2}{3}";
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
    //break
    public float BaseDeceleration;
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
    #region Unity API
    #endregion

    #region Public API
    public static void Save(ShipData data)
    {
        console.logStatus("Saving " + FILENAME + "_" + data.Name);
        Serializer_Deserializer<ShipData>.Save(data, SavedPath.GameData, GetPath(data.Name), m_Types);
    }

    public static ShipData Load(string shipName)
    {
        console.logStatus("Loading " + shipName);
        return Serializer_Deserializer<ShipData>.Load(SavedPath.GameData, GetPath(shipName), m_Types);
    }
#if UNITY_EDITOR
    [UnityEditor.MenuItem("CreateConfigs/Data/ShipData")]
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
    private static string GetPath(string name)
    {
        return string.Format(PATH_FORMAT, FOLDER, FILENAME, SEPERATOR, name);
    }
    #endregion
}
