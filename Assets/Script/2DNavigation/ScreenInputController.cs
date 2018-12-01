//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
//  Description
//
//  Get Mouse input and translate to world positions.
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInputController : MonoBehaviour {

    public struct sInputs
    {

        public Vector3 MouseScreenPosition
        {
            get { return currentMousePosition; }
            set
            {
                MouseDelta = currentMousePosition - value;
                currentMousePosition = value;
            }
        }
        public Vector3 MouseDelta;

        private Vector3 currentMousePosition;

        public bool OnMouseDown;
        public bool MouseDown;
        public bool OnMouseUp;

        public bool OnMouseAltDown;
        public bool MouseAltDown;
        public bool OnMouseAltUp;
    }
    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    public static ScreenInputController instance;
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
    private ISelectable m_CurrentSelection;
    private sInputs m_Inputs;

    public ISelectable CurrentSelection
    {
        get { return m_CurrentSelection; }
    }

    #region Unity API
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        GetInput();

        if (m_Inputs.OnMouseAltDown)
        {
            GetSelected();
        }

        if (m_Inputs.OnMouseDown)
        {
            GetSelected();
        }
    }
    #endregion  

    #region Public API
    #endregion

    #region Protect
    #endregion

    #region Private
    private void GetInput()
    {
        m_Inputs.MouseScreenPosition = Input.mousePosition;

        m_Inputs.OnMouseDown = Input.GetMouseButtonDown(0);
        m_Inputs.OnMouseUp = Input.GetMouseButtonUp(0);
        m_Inputs.MouseDown = Input.GetMouseButton(0);

        m_Inputs.OnMouseAltUp = Input.GetMouseButtonUp(1);
        m_Inputs.OnMouseAltDown = Input.GetMouseButtonDown(1);
        m_Inputs.MouseAltDown = Input.GetMouseButton(1);
    }

    private void GetSelected()
    {
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(m_Inputs.MouseScreenPosition), Vector2.zero);

        List<ISelectable> selected = new List<ISelectable>();
        ISelectable highestPriority = null;
        int priority = 0;
        for (int i = 0; i < hits.Length; i++)
        {
            ISelectable selectable = hits[i].collider.gameObject.GetComponent<ISelectable>();
            if (selectable != null)
            {
                if (highestPriority == null)
                {
                    highestPriority = selectable;
                    priority = highestPriority.SelectPriority;
                }
                else if (selectable.SelectPriority > highestPriority.SelectPriority)
                {
                    highestPriority = selectable;
                    priority = highestPriority.SelectPriority;
                }
            }
        }

        if (m_CurrentSelection != highestPriority)
        {
            if (m_CurrentSelection != null)
            {
                m_CurrentSelection.Deselect();
            }

            if (highestPriority != null)
            {
                highestPriority.Select();
            }
        }

        m_CurrentSelection = highestPriority;
    }
    #endregion
}
