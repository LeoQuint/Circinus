//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseManager {

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
    public static bool IsPaused = false;
    public static float TimeScaleAtPause = 0f;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    #endregion

    #region Public API
    public static void Pause()
    {
        if (!IsPaused)
        {
            TimeScaleAtPause = Time.timeScale;
            Time.timeScale = 0f;
            IsPaused = true;
        }
    }

    public static void UnPause()
    {
        if (IsPaused)
        {
            Time.timeScale = TimeScaleAtPause;
            IsPaused = false;
        }
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
