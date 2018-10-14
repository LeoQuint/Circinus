//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using War;
using CoreUtility;

/// <summary>
/// Class used to modify/save/load army generation configurations from file.
/// </summary>
public class ArmyConfigInterface : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

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
            Serializer_Deserializer<ArmyGroup>.Save(testGroup, SavedPath.GameData, "TestGroup",
                new System.Type[] { typeof(ArmyUnit) });
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
        m_Configs = Serializer_Deserializer<ArmyConfig>.Load(SavedPath.Configuration, FILENAME, m_Types);
        if (m_Configs == null)
        {
            m_Configs = new ArmyConfig();
        }
    }

    public void Save()
    {
        //ArmyConfigs
        Serializer_Deserializer<ArmyConfig>.Save(m_Configs, SavedPath.Configuration, FILENAME, m_Types);
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
