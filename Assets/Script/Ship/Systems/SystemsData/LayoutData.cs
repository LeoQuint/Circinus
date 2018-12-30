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
[XmlRoot("LayoutData")]
public class LayoutData {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const string FOLDER = "Ship/Layout";
    private const string FILENAME = "LayoutData";
    private const char SEPERATOR = '_';
    private const string PATH_FORMAT = "{0}/{1}{2}{3}";
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    private static Type[] m_Types = new Type[2] { typeof(LayoutData), typeof(TileInfo) };
    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    public string Name;
    public List<List<TileInfo>> Layout;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    #endregion

    #region Public API
    public TileInfo[][] GetLayout()
    {
        TileInfo[][] toReturn = new TileInfo[Layout.Count][];
        for (int x = 0; x < Layout.Count; ++x)
        {
            toReturn[x] = new TileInfo[Layout[x].Count];
            for (int y = 0; y < Layout[0].Count; ++y)
            {
                toReturn[x][y] = Layout[x][y];
            }
        }

        return toReturn;
    }

    public static void Save(LayoutData data)
    {
        console.logStatus("Saving " + FILENAME + "_" + data.Name);
        Serializer_Deserializer<LayoutData>.Save(data, SavedPath.GameData, GetPath(data.Name), m_Types);
    }

    public static LayoutData Load(string name)
    {
        console.logStatus("Loading " + name);
        return Serializer_Deserializer<LayoutData>.Load(SavedPath.GameData, GetPath(name), m_Types);
    }
#if UNITY_EDITOR
    [UnityEditor.MenuItem("CreateConfigs/Create New LayoutData")]
    static void CreateData()
    {
        LayoutData sd = new LayoutData();
        sd.Name = "NEW_LayoutData";
        sd.Layout = new List<List<TileInfo>>();
        LayoutData.Save(sd);
        console.log("LayoutData template created");
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
