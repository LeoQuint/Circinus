//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevChunk : Chunk {

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

    public DevChunk(int posX, int posZ) : base (posX, posZ)
    {

    }

    #region Unity API
    #endregion

    #region Public API
    public override void OnUnityUpdate()
    {
        base.OnUnityUpdate();
        if (m_HasGenerated && !m_HasRendered && m_HasDrawned)
        {
            m_HasGenerated = false;
            m_HasDrawned = false;
            m_HasRendered = false;
            Start();
        }
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
