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
[XmlRoot("ShipWeaponData")]
public class ShipWeaponData {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const string FOLDER = "Ship/ShipWeapon";
    private const string FILENAME = "ShipWeaponData";
    private const char SEPERATOR = '_';
    private const string PATH_FORMAT = "{0}/{1}{2}{3}";

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    private static Type[] m_Types = new Type[1] { typeof(ShipWeaponData) };
    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    public string Name;

    public float Acceleration;
    public float MaxSpeed;
    //break
    public float Deceleration;
    //steering
    public float SteeringAccelerationSpeed;
    public float SteeringMaxSpeed;
    //stats
    public float Weight;
    public float Health;

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
    public static void Save(ShipWeaponData data)
    {
        console.logStatus("Saving " + FILENAME + "_" + data.Name);
        Serializer_Deserializer<ShipWeaponData>.Save(data, SavedPath.GameData, GetPath(data.Name), m_Types);
    }

    public static ShipWeaponData Load(string name)
    {
        console.logStatus("Loading " + name);
        return Serializer_Deserializer<ShipWeaponData>.Load(SavedPath.GameData, GetPath(name), m_Types);
    }
#if UNITY_EDITOR
    [UnityEditor.MenuItem("CreateConfigs/Data/ShipWeaponData")]
    static void CreateData()
    {
        ShipWeaponData swd = new ShipWeaponData();
        swd.Name = "NEW_ShipWeaponData";
        Save(swd);
        console.log("Engine Data template created");
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

public enum eShipWeaponType
{
    KINETIC,
    LASER,
    PLASMA,
    MISSILE
}