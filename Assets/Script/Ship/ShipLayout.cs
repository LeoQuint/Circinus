//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipLayout", menuName = "Ship/Layout", order = 2)]
public class ShipLayout : ScriptableObject {

    [System.Serializable]
    public struct sRowLayout
    {
        public sTileInfo[] Row;

        public sRowLayout(sTileInfo[] row)
        {
            Row = row;
        }

        public sTileInfo this[int index]
        {
            get { return Row[index]; }
            set { Row[index] = value; }
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
    public int _Width;
    public int _Height;
    public sRowLayout[] m_Layout;

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
    public sTileInfo[][] GetLayout()
    {
        sTileInfo[][] layout = new sTileInfo[m_Layout.Length][];

        for (int i = 0; i < m_Layout.Length; ++i)
        {
            layout[i] = m_Layout[i].Row;
        }
        return layout;
    }

    public void SetLayout(sTileInfo[][] layout)
    {
        m_Layout = new sRowLayout[layout.Length];

        for (int i = 0; i < m_Layout.Length; ++i)
        {
            m_Layout[i] = new sRowLayout(layout[i]);
        }
    }

    public sTileInfo this[int x, int y]
    {
        get
        {
            return m_Layout[x][y];
        }
        set
        {
            m_Layout[x][y] = value;
        }
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
