//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{

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
    protected StarSystem m_StarSystem;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
   
    ///Properties
    ///
    public StarSystem System
    {
        get { return m_StarSystem; }
    }

    #region Unity API  
    #endregion

    #region Public API
    public void Init()
    {
        float color = (1f + (float)m_StarSystem.m_StarType.TemperatureCode) / (float)(StarSystem.Temperature.COUNT);
        GetComponent<Renderer>().material.SetFloat("_Index", color);
        GetComponent<Renderer>().material.SetFloat("_Brightness", (float)m_StarSystem.m_StarType.Luminosity);
    }

    public void Load(StarSystem starSystem)
    {
        m_StarSystem = starSystem;
    }
    #endregion

    #region Protect
    #endregion

    #region Private
   

    #endregion
}
