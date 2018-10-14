//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigLoader : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    private static ConfigLoader instance;
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
    private AIConfig m_AIConfig;
    public AIConfig AIConfig
    {
        get { return m_AIConfig;  }
    } 

    public static ConfigLoader Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject holder = new GameObject("ConfigLoader");
                ConfigLoader atm = holder.AddComponent<ConfigLoader>();
                instance = atm;
            }
            return instance;
        }
    }
    #region Unity API
    protected void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    #region Public API
    [ContextMenu("Load")]
    public void Load()
    {
       m_AIConfig = Serializer_Deserializer<AIConfig>.Load(m_AIConfig.SavedPath, m_AIConfig.Filename);
        console.logStatus(m_AIConfig.TaskPriorities.Count);
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    [ContextMenu("Create Configs")]
    private void CreateConfigs()
    {
        AIConfig aic = new AIConfig();
        aic.TaskPriorities = new List<List<AITask.TaskType>>();
        for (int i = 0; i < AIConfig.NUMBER_OF_PRIORITY_LEVELS; ++i)
        {
            aic.TaskPriorities.Add(new List<AITask.TaskType>() { AITask.TaskType.None });
        }
        Serializer_Deserializer<AIConfig>.Save(aic, aic.SavedPath, aic.Filename);
    }
    #endregion
}
