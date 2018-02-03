//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public enum eScene
{
    NULL,
    Ship,
    Galaxy,
    World,
    Main_Menu,
    Count
}

public class FlowManager : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    public static FlowManager instance;
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

    #region Unity API
    protected void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    protected void Start()
    {

    }
    #endregion

    #region Public API

    public void LoadScene(eScene scene, LoadSceneMode loadMode)
    {
        SceneManager.LoadScene(scene.ToString(), loadMode);        
    }
    
    public void LoadSceneAsync(eScene scene, LoadSceneMode loadMode)
    {
        SceneManager.LoadSceneAsync(scene.ToString(), loadMode);
    }

    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
