//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using War;
using CoreUtility;
using ArmyConfigSavedPath = Serializer_Deserializer<ArmyConfig>.SavedPath;
using ArmyGoupSavedPath = Serializer_Deserializer<ArmyGroup>.SavedPath;

/// <summary>
/// Class used to modify/save/load army generation configurations from file.
/// </summary>
public class ArmyConfigInterface : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    ArmyConfigSavedPath ARMY_CONFIG_SAVED_PATH = ArmyConfigSavedPath.Configuration;
    ArmyGoupSavedPath ARMY_GROUP_SAVED_PATH = ArmyGoupSavedPath.GameData;
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
            instance = this;
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
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ArmyGroup testGroup = GenerateArmyGroup(EFaction.Blue, EArmyGroupType.MainCorp, 100000);
            Serializer_Deserializer<ArmyGroup> sd = new Serializer_Deserializer<ArmyGroup>(testGroup, ARMY_GROUP_SAVED_PATH, "TestGroup", 
                new System.Type[] { typeof(ArmyUnit) });
            sd.Save();
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
        //ArmyConfigs
        Serializer_Deserializer<ArmyConfig> sd = new Serializer_Deserializer<ArmyConfig>(m_Configs, ARMY_CONFIG_SAVED_PATH, FILENAME, m_Types);
        m_Configs = sd.Load();
        if (m_Configs == null)
        {
            m_Configs = new ArmyConfig();
        }
    }

    public void Save()
    {
        //ArmyConfigs
        Serializer_Deserializer<ArmyConfig> sd = new Serializer_Deserializer<ArmyConfig>(m_Configs, ARMY_CONFIG_SAVED_PATH, FILENAME, m_Types);
        sd.Save();
    }

    public ArmyGroup GenerateArmyGroup(EFaction faction, EArmyGroupType type, int strengh)
    {
        ArmyGroup group = new ArmyGroup(type);

        PercentageList<EShipClass> shipList = new PercentageList<EShipClass>();

        for (int i = 0; i < m_Configs.ShipOccurence.Count; ++i)
        {
            shipList.Add(m_Configs.ShipOccurence[i].item, m_Configs.ShipOccurence[i].percentage);
        }

        while (strengh > 0)
        {
            EShipClass ship = shipList.Random();
            strengh -= (int)ship;
            group.Add(ship);
        }

        return group;
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
