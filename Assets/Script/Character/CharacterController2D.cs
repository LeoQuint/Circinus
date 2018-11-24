//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    public struct s2DInput
    {
        public float Horizontal;
        public float Vertical;

        public eButtonState X;
        public eButtonState Y;
        public eButtonState A;
        public eButtonState B;

        public eButtonState Select;
        public eButtonState Start;
    }

    public enum eButtonState
    {        
        UnPressed,  //True while not pressed.
        Released,   //The first frame the button is released
        Pressed,    //The first frame the button is pressed
        HeldDown    //True while held down
    }

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const string HORIZONTAL_INPUT_KEY = "Horizontal";
    private const string VERTICAL_INPUT_KEY = "Vertical";
    private const string X_INPUT_KEY = "XButton";
    private const string Y_INPUT_KEY = "YButton";
    private const string A_INPUT_KEY = "AButton";
    private const string B_INPUT_KEY = "BButton";

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
    protected s2DInput m_Inputs;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    protected void Update()
    {
        GetInput();
    }
    #endregion

    #region Public API
    public void Init()
    {

    }
    #endregion

    #region Protect
    protected void GetInput()
    {
        m_Inputs.Vertical       = Input.GetAxis(VERTICAL_INPUT_KEY);
        m_Inputs.Horizontal     = Input.GetAxis(HORIZONTAL_INPUT_KEY);
        m_Inputs.X              = GetButtonInput(X_INPUT_KEY);
    }

    #endregion

    #region Private
    private eButtonState GetButtonInput(string button)
    {
        // Input.GetButton(X_INPUT_KEY);
        return eButtonState.UnPressed;
    }
    #endregion
}
