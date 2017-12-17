//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[System.Serializable]
[XmlRoot("Location")]
public class Location {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const string PREFAB_EMPTY_LOCATION = "Galaxy/Prefabs/Locations/Empty";
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    public Vector3 m_Position;
    public ArmyUnit m_Units;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    protected GameObject m_Target;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    #endregion

    #region Public API
    public virtual void Initialize()
    {
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private void SetPosition()
    {
    
    }
    #endregion
}
