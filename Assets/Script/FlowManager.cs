//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum eScene
{
    NULL,
    Ship,
    Galaxy,
    World,
    Maine_Menu,
    Count
}

public class FlowManager : Singleton {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

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

    #region Unity API
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
