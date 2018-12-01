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
using System;

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

        public bool OnMouseMidDown;
        public bool MouseMidDown;
        public bool OnMouseMidUp;
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
    public Action<Tile, Vector2> OnLocationSelected;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private ISelectable m_CurrentSelection;
    private Vector2 m_InnerPosition;

    private Tile m_CurrentLocation;
    private sInputs m_Inputs;

    public sInputs Inputs
    {
        get { return m_Inputs; }
    }

    public ISelectable CurrentSelection
    {
        get { return m_CurrentSelection; }
    }

    public Tile CurrentLocation
    {
        get { return m_CurrentLocation; }
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

        if (m_Inputs.OnMouseDown)
        {
            GetSelection();
        }

        if (m_Inputs.OnMouseAltDown)
        {
            GetLocation();
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

        m_Inputs.OnMouseMidUp = Input.GetMouseButtonUp(2);
        m_Inputs.OnMouseMidDown = Input.GetMouseButtonDown(2);
        m_Inputs.MouseMidDown = Input.GetMouseButton(2);
    }

    private void GetSelection()
    {
        ISelectable highestPriority = GetHighestPrioritySelectable();

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

    private ISelectable GetHighestPrioritySelectable()
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
                if (highestPriority == null || selectable.SelectPriority > highestPriority.SelectPriority)
                {
                    highestPriority = selectable;
                    priority = highestPriority.SelectPriority;
                    m_InnerPosition = hits[i].collider.bounds.center.GetOffset(hits[i].point);
                }
            }
        }

        return highestPriority;
    }

    private void GetLocation()
    {
        ISelectable highestPriority = GetHighestPrioritySelectable();

        if (highestPriority == null)
        {
            m_CurrentSelection = null;
        }
        else if (highestPriority.SelectableType == eSelectableType.TILE)
        {
            m_CurrentLocation = highestPriority as Tile;

            if (OnLocationSelected != null)
            {
                OnLocationSelected(m_CurrentLocation, m_InnerPosition);
            }
        }
    }
    #endregion
}
