//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelManager : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////    

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    public static float _sDivision_X = 1f;
    public static float _sDivision_Z = 1f;
    public static float _sMultiply_Y = 1f;
    public static float _sCutoff = 1f;
    public static float _sMultiply = 1f;
    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    public float _Division_X = 1f;
    public float _Division_Z = 1f;
    public float _Multiply_Y = 1f;
    public float _Cutoff = 1f;
    public float _Multiply = 1f;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private MainLoopable m_MainLoopable;


    #region Unity API
    protected void Start()
    {       
        MainLoopable.Initialize();
        Block.Initialize();
        m_MainLoopable = MainLoopable.Instance;
        m_MainLoopable.Start();
    }

    protected void Update()
    {
       /* _sDivision_X    = _Division_X;
        _sDivision_Z    = _Division_Z;
        _sMultiply_Y    = _Multiply_Y;
        _sMultiply      = _Multiply;
        _sCutoff        = _Cutoff; */
        m_MainLoopable.Update();
    }

    protected void OnApplicationQuit()
    {
        m_MainLoopable.OnApplicationQuit();
    }
    #endregion

    #region Public API
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
