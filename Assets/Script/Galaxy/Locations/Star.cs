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
    protected OnHoverOver m_OnHover;
    protected OverheadBillboard m_Billboard;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private string m_StarDisplay;
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
        m_OnHover = gameObject.AddComponent<OnHoverOver>();
        m_Billboard = GetComponentInChildren<OverheadBillboard>();
        m_Billboard.Init(m_OnHover);
        SetDisplay();
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private void SetDisplay()
    {
        m_StarDisplay = m_StarSystem.m_Name + "\n";
        m_StarDisplay += m_StarSystem.m_ControllingFaction.ToString() + "\n";
        m_StarDisplay += "Forces " + m_StarSystem.LocalForcesStrength.ToLargeValues();

        m_Billboard.Text = m_StarDisplay;
    }
    #endregion
}
