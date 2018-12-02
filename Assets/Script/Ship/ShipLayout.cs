//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipLayout", menuName = "Ship/Layout", order = 2)]
public class ShipLayout : ScriptableObject {

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
    public int _Width;
    public int _Height;
    public FloorLayout.sLayout[] m_Layout;
    //Editor saved data
    public float _SizeDrawerSaved = 25f;
    public float _LeftOffsetDrawerSaved = 20f;
    public float _TopOffsetDrawerSaved = 200f;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    #endregion

    #region Public API
    public FloorLayout.sLayout[] GetLayout()
    {
        return m_Layout;
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
