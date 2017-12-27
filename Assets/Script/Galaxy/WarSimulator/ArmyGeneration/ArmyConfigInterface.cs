//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to modify/save/load army generation configurations from file.
/// </summary>
public class ArmyConfigInterface : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const Serializer_Deserializer<ArmyConfig>.SavedPath SAVED_PATH = Serializer_Deserializer<ArmyConfig>.SavedPath.Configuration;
    private const string FILENAME = "ArmyConfiguration";
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    public static ArmyConfigInterface instance;
    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [SerializeField][InEditorReadOnly]
    private ArmyConfig m_Configs;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////    

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private System.Type[] m_Types = { typeof(ArmyConfig) };
    ///Properties
    ///
    public ArmyConfig Config
    {
        get { return m_Configs; }
    }

    #region Unity API
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Init();
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }
    #endregion

    #region Public API
    public void Init()
    {
        Load();
    }

    public void Load()
    {
        Debug.Log("Loading Army Configuration.");
        //Army Congifs
        Serializer_Deserializer<ArmyConfig> sd = new Serializer_Deserializer<ArmyConfig>(m_Configs, SAVED_PATH, FILENAME, m_Types);
        m_Configs = sd.Load();
        if (m_Configs == null)
        {
            m_Configs = new ArmyConfig();
        }
    }

    public void Save()
    {
        Serializer_Deserializer<ArmyConfig> sd = new Serializer_Deserializer<ArmyConfig>(m_Configs, SAVED_PATH, FILENAME, m_Types);
        sd.Save();
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
