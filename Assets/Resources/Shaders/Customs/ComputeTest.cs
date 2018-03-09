//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeTest : MonoBehaviour {

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
    public RenderTexture _RenderTexture;
    public ComputeShader _CS;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    public void Start()
    {
        int kernel = _CS.FindKernel("CSMain");
        _RenderTexture = new RenderTexture(512,512,24);
        _RenderTexture.enableRandomWrite = true;
        _RenderTexture.Create();
        _CS.SetTexture(kernel, "Result", _RenderTexture);
        _CS.Dispatch(kernel, 512/8, 512/8 ,1);
    }
    #endregion

    #region Public API
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
