//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using War;
using CoreUtility;

public class ArmyGenerator : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const Serializer_Deserializer<ArmyGenerator>.SavedPath SAVED_PATH = Serializer_Deserializer<ArmyGenerator>.SavedPath.Configuration;
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [SerializeField]
    private int m_BaseStrength = 1000000;
    [SerializeField]
    private PercentageList<EShipClass> m_UnitOccurenceSettings = new PercentageList<EShipClass>();
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GenerateArmy();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SaveConfiguration();
        }
    }
    #endregion

    #region Public API
    public void GenerateArmy()
    {
        m_UnitOccurenceSettings.Add(EShipClass.Battlecruiser, 0.5f);
    }

    public void SaveConfiguration()
    {

    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
