//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public class Serializer_Deserializer<T> {

   
    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private static readonly Dictionary<SavedPath, string> SAVED_PATH = new Dictionary<SavedPath, string>()
    {
        { SavedPath.GameData, "Assets/Save/"},
        { SavedPath.Configuration, "Assets/XMLConfigs/"}
    };

    private const string EXTENTION = ".xml";
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    /// <summary>
    /// Constructor
    /// </summary>
    #region Unity API
    #endregion

    #region Public API
    public static void Save(T data,SavedPath path, string filename, System.Type[] Types = null)
    {
        string formatedPath = string.Format("{0}{1}{2}", SAVED_PATH[path], filename, EXTENTION);
        XmlSerializer serializer = new XmlSerializer(typeof(T), Types);
        FileStream fs = new FileStream(formatedPath, FileMode.Create);
        serializer.Serialize(fs, data);
        fs.Close();
    }

    public static T Load(SavedPath path, string filename, System.Type[] Types = null)
    {
        string formatedPath = string.Format("{0}{1}{2}", SAVED_PATH[path] , filename , EXTENTION);
        if (!File.Exists(formatedPath))
        {
            console.logError(formatedPath + " NOT FOUND! Returning default values.");
            return default(T);
        }
        XmlSerializer serializer = new XmlSerializer(typeof(T), Types);
        // To read the file, create a FileStream.
        FileStream fs = new FileStream(formatedPath, FileMode.Open);
        // Call the Deserialize method and cast to the object type.
        T loadedDataStore = (T)serializer.Deserialize(fs);

        fs.Close();
        return loadedDataStore;
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}

public enum SavedPath
{
    GameData,
    Configuration
}