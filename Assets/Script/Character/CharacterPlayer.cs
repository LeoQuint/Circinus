//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : Character {

    public struct sPlayerInput
    {
        public float VerticalAxis;
        public float HorizontalAxis;

        public bool Jump;

        private float m_VerticalLookAxis;
        private float m_PreviousVerticalLookAxis;
        private float m_HorizontalLookAxis;
        private float m_PreviousHorizontalLookAxis;

        public float HorizontalLookAxis
        {
            get { return m_HorizontalLookAxis;  }
            set
            {
                m_PreviousHorizontalLookAxis = m_HorizontalLookAxis;
                m_HorizontalLookAxis = value;
            }
        }

        public float VerticalLookAxis
        {
            get { return m_VerticalLookAxis; }
            set
            {
                m_PreviousVerticalLookAxis = m_VerticalLookAxis;
                m_VerticalLookAxis = value;
            }
        }

        public float DeltaVerticalLookAxis
        {
            get { return m_VerticalLookAxis - m_PreviousHorizontalLookAxis; }
        }

        public float DeltaHorizontalLookAxis
        {
            get { return m_HorizontalLookAxis - m_PreviousHorizontalLookAxis; }
        }

        public Vector3 Direction()
        {
            return new Vector3(HorizontalAxis, 0f, VerticalAxis).normalized;
        }      
    }
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
    private sPlayerInput m_Input;

    #region Unity API
    protected void Update()
    {
        InputUpdate();
    }
    #endregion

    #region Public API
    #endregion

    #region Protect
    protected void InputUpdate()
    {
       
    }
    #endregion

    #region Private
    #endregion
}
