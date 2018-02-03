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

    private bool m_NearStarsCalculated = false;
    private List<Star> m_NearStars = new List<Star>();
    ///Properties
    ///
    public List<Star> NearStars
    {
        get { return m_NearStars;  }
    }

    public StarSystem System
    {
        get { return m_StarSystem; }
    }

    public War.EFaction ControllingFaction
    {
        get { return m_StarSystem.m_ControllingFaction; }
    }
    #region Unity API
    private void OnDrawGizmosSelected()
    {
        if (m_NearStarsCalculated && m_NearStars.Count > 0)
        {
            for (int i = 0; i < m_NearStars.Count; ++i)
            {
                float ratio = Vector3.Distance(transform.position, m_NearStars[i].transform.position) / 
                                GameConstants.GALAXY_MAX_DISTANCE_TO_NEAR_STARS;

                Gizmos.color = Color.Lerp(Color.green, Color.red, ratio);
                Gizmos.DrawLine(transform.position, m_NearStars[i].transform.position);
            }
        }
        else if (!m_NearStarsCalculated)
        {
            CalculateNearStars();
        }
    }
    #endregion

    #region Public API
    /// <summary>
    /// Wait for all stars to be load prior to Init.
    /// </summary>
    public void Init()
    {
        float color = (1f + (float)m_StarSystem.m_StarType.TemperatureCode) / (float)(StarSystem.Temperature.COUNT);
        GetComponent<Renderer>().material.SetFloat("_Index", color);
        GetComponent<Renderer>().material.SetFloat("_Brightness", (float)m_StarSystem.m_StarType.Luminosity);
        CalculateNearStars();
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
    private void CalculateNearStars()
    {
        if (m_NearStarsCalculated)
        {
            return;
        }
        Star[][] starList = GalaxyGenerator.instance.StarList;

        for (int i = 0; i < starList.Length; ++i)
        {
            for (int j = 0; j < starList[i].Length; ++j)
            {
                float distance = Vector3.Distance(transform.position, starList[i][j].transform.position);
                if (distance > 0.1f && distance < GameConstants.GALAXY_MAX_DISTANCE_TO_NEAR_STARS)
                {
                    m_NearStars.Add(starList[i][j]);
                }
            }
        }

        m_NearStarsCalculated = true;
    }

    private void SetDisplay()
    {
        m_StarDisplay = m_StarSystem.m_Name + "\n";
        m_StarDisplay += m_StarSystem.m_ControllingFaction.ToString() + "\n";
        m_StarDisplay += "Forces " + m_StarSystem.LocalForcesStrength.ToLargeValues();

        m_Billboard.Text = m_StarDisplay;
    }
    #endregion
}
