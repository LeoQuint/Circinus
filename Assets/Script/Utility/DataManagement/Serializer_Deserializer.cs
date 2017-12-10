//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class Serializer_Deserializer<T> {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const string SAVED_PATH = "Assets/Save/";

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
    #region Unity API
    #endregion

    #region Public API
    public Serializer_Deserializer(T data, string filename = "", System.Type[] types = null)
    {
        if (!string.IsNullOrEmpty(filename))
        {
            m_Filename = filename;
        }
        m_Types = types;
        m_DataStore = data;
    }

    public void Save()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T), m_Types);
        FileStream fs = new FileStream(SAVED_PATH + m_Filename, FileMode.Create);
        serializer.Serialize(fs, m_DataStore);
        fs.Close();
        m_DataStore = default(T);
    }

    public T Load(string filename = "")
    {
        if (!string.IsNullOrEmpty(filename))
        {
            m_Filename = filename;
        }
        if (!File.Exists(SAVED_PATH + m_Filename))
        {
            Debug.LogError("FILE " + SAVED_PATH + m_Filename + " NOT FOUND!");
            return default(T);
        }
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        // To read the file, create a FileStream.
        FileStream fs = new FileStream(SAVED_PATH + m_Filename, FileMode.Open);
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
