//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All on launch inits should be triggered here to keep the order
/// of execution in check.
/// </summary>
public partial class AppLauncher : MonoBehaviour {

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
    private void Awake()
    {
        Init();
    }
    #endregion

    #region Public API
    #endregion

    #region Private
    private void Init()
    {
        InitFlowManager();
        InitCameraManger();
        InitGalaxy();
        InitWarSimulator();
    }

    private void InitFlowManager()
    {

    }

    private void InitCameraManger()
    {
        
    }

    private void InitGalaxy()
    {
        GalaxyGenerator.instance.Init();
    }

    private void InitWarSimulator()
    {
        WarSimulator.instance.Init();
    }
    #endregion
}
