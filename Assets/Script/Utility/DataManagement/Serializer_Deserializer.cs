//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public class Serializer_Deserializer<T> {

    public enum SavedPath
    {
        GameData,
        Configuration
    }
    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private readonly Dictionary<SavedPath, string> SAVED_PATH = new Dictionary<SavedPath, string>()
    {
        { SavedPath.GameData, "Assets/Save/"},
        { SavedPath.Configuration, "Assets/XMLConfigs/"}
    };
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
    private string m_Filename = "save.xml";
    private T m_DataStore;
    private System.Type[] m_Types = null;
    private SavedPath m_Path;

    #region Unity API
    #endregion

    #region Public API
    public Serializer_Deserializer(T data, SavedPath path = SavedPath.GameData, string filename = "", System.Type[] types = null)
    {
        m_Path = path;
        if (!string.IsNullOrEmpty(filename))
        {
            m_Filename = filename;
        }
        else
        {
            Debug.LogWarning("Filename missing, default '" + m_Filename + "' will be used.");
        }
        m_Types = types;
        m_DataStore = data;
    }

    public void Save()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T), m_Types);
        FileStream fs = new FileStream(SAVED_PATH[m_Path] + m_Filename, FileMode.Create);
        serializer.Serialize(fs, m_DataStore);
        fs.Close();
        m_DataStore = default(T);
    }

    public T Load(SavedPath path = SavedPath.GameData, string filename = "")
    {
        m_Path = path;
        if (!string.IsNullOrEmpty(filename))
        {
            m_Filename = filename;
        }
        else
        {
            Debug.LogWarning("Filename missing, default '" + m_Filename + "' will be used.");
        }
        if (!File.Exists(SAVED_PATH[m_Path] + m_Filename))
        {
            Debug.LogError("FILE " + SAVED_PATH[m_Path] + m_Filename + " NOT FOUND!");
            return default(T);
        }
        XmlSerializer serializer = new XmlSerializer(typeof(T), m_Types);
        // To read the file, create a FileStream.
        FileStream fs = new FileStream(SAVED_PATH[m_Path] + m_Filename, FileMode.Open);
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
