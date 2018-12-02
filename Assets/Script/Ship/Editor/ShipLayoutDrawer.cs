//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using sLayout = FloorLayout.sLayout;

[CustomEditor(typeof(ShipLayout))]
public class ShipLayoutDrawer : Editor {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const float TILE_SIZE = 25f;
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
    private bool m_IsClicking = false;
    private bool m_IsRightClicking = false;
    private bool m_ClickHeld = false;
    private bool m_RightClickHeld = false;
    private bool m_MiddleClickHeld = false;

    private TileType m_CurrentBrush = TileType.STEEL;
    private float m_TileSize = TILE_SIZE;

    #region Unity API
    public override void OnInspectorGUI()
    {
        ShipLayout shipLayout = target as ShipLayout;

        EditorGUILayout.BeginVertical("window");

            EditorGUILayout.BeginHorizontal("box");
            shipLayout._Width = EditorGUILayout.IntField("Width", shipLayout._Width);
            shipLayout._Height = EditorGUILayout.IntField("Height", shipLayout._Height);
            EditorGUILayout.EndHorizontal();
            //Buttons
            EditorGUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Save"))
                {
                    SaveChanges(shipLayout);
                }

                if (GUILayout.Button("Generate"))
                {
                    RefreshArray(shipLayout);
                }

                if (GUILayout.Button("Clear"))
                {
                    Empty(shipLayout);
                }
            EditorGUILayout.EndHorizontal();
            //Sliders
            EditorGUILayout.BeginVertical("box");
            shipLayout._LeftOffsetDrawerSaved = EditorGUILayout.Slider("Left", shipLayout._LeftOffsetDrawerSaved, 0f, 1000f);
            shipLayout._TopOffsetDrawerSaved = EditorGUILayout.Slider("Top", shipLayout._TopOffsetDrawerSaved, 0f, 1000f);
            m_TileSize = EditorGUILayout.Slider("Size", m_TileSize, 1f, 200f);
            m_CurrentBrush = (TileType)EditorGUILayout.EnumPopup("Tile Brush", m_CurrentBrush);
            EditorGUILayout.EndVertical();

        DrawLayout(shipLayout);

        EditorGUILayout.EndVertical();


        //DrawDefaultInspector();
    }
    #endregion

    #region Public API
    #endregion

    #region Protect
    #endregion

    #region Private
    private void DrawLayout(ShipLayout shipLayout)
    {
        if (shipLayout.m_Layout != null && shipLayout.m_Layout.Length > 0)
        {
            Rect positions = new Rect(shipLayout._LeftOffsetDrawerSaved, shipLayout._TopOffsetDrawerSaved, m_TileSize, m_TileSize);
            
            for (int i = 0; i < shipLayout.m_Layout.Length; ++i)
            {
                Vector3 iVector = (Vector3.right * i * m_TileSize);
                for (int j = 0; j < shipLayout.m_Layout[i].Row.Length; ++j)
                {
                    Vector3 offset = iVector + (Vector3.up * j * m_TileSize);
                    Rect rect = new Rect( positions);
                    rect.x += offset.x;
                    rect.y += offset.y;
                    EditorGUI.DrawRect(rect , GetGizmosColor(shipLayout.m_Layout[i][j]));
                }
            }
        }

        if (Event.current.type == EventType.MouseDown)
        {
            if (Event.current.button == 0)
            {
                m_ClickHeld = true;
            }
            else if (Event.current.button == 1)
            {
                m_RightClickHeld = true;
            }
            else if (Event.current.button == 2)
            {
                m_MiddleClickHeld = true;
            }
        }
        else if (Event.current.type == EventType.MouseUp)
        {
            m_ClickHeld = false;
            m_RightClickHeld = false;
            m_MiddleClickHeld = false;
        }
        //Drawing
        if ( (!m_IsClicking && Event.current.isMouse && Event.current.clickCount > 0 && Event.current.button == 0 ) || m_ClickHeld)
        {
            m_IsClicking = true;
            AssignTypeAt(Event.current.mousePosition, shipLayout);
        }
        else
        {
            m_IsClicking = false;
        }
        //Erasing
        if (!m_IsRightClicking && Event.current.isMouse && Event.current.clickCount > 0 && Event.current.button == 1 || m_RightClickHeld)
        {
            m_IsRightClicking = true;
            AssignTypeAt(Event.current.mousePosition, shipLayout, true);
        }
        else
        {
            m_IsRightClicking = false;
        }
        //Moving
        if (m_MiddleClickHeld)
        {           
            Vector2 offset = Event.current.delta;          

            shipLayout._LeftOffsetDrawerSaved += offset.x;
            shipLayout._TopOffsetDrawerSaved += offset.y;
        }
    }

    private Color GetGizmosColor(TileType type)
    {
        switch (type)
        {
            case TileType.EMPTY:
                return new Color(0f, 0f, 0f, 0.5f);

            case TileType.OUTER_WALL:
                return new Color(1f, 0f, 0f, 0.5f);

            case TileType.INNER_WALL:
                return new Color(0f, 1f, 0f, 0.5f);

            case TileType.STEEL:
                return Color.grey;
        }

        return Color.magenta;
    }
    
    private void RefreshArray(ShipLayout layout)
    {
        sLayout[] newLayout = new sLayout[layout._Width];
        for (int i = 0; i < newLayout.Length; ++i)
        {
            newLayout[i].Row = new TileType[layout._Height];
            if (layout.m_Layout != null && layout.m_Layout.Length > i)
            {
                for (int j = 0; j < newLayout[i].Row.Length; ++j)
                {
                    if (layout.m_Layout[i].Row != null && layout.m_Layout[i].Row.Length > j)
                    {
                        newLayout[i][j] = layout.m_Layout[i].Row[j];
                    }
                    else
                    {
                        newLayout[i][j] = TileType.EMPTY;
                    }
                }
            }
        }
        layout.m_Layout = newLayout;

        SaveChanges(layout);
    }
    
    private void Empty(ShipLayout layout)
    {
        sLayout[] newLayout = new sLayout[layout._Width];
        for (int i = 0; i < newLayout.Length; ++i)
        {
            newLayout[i].Row = new TileType[layout._Height];
        }
        layout.m_Layout = newLayout;

        SaveChanges(layout);
    }

    private void AssignTypeAt(Vector3 worldPosition, ShipLayout layout, bool erase = false)
    {
        int x = (int)((worldPosition.x - layout._LeftOffsetDrawerSaved) /m_TileSize);
        int y = (int)((worldPosition.y - layout._TopOffsetDrawerSaved)/m_TileSize);

        if (x >= 0 && y >= 0 && layout.m_Layout.Length > x && layout.m_Layout[x].Row.Length > y)
        {
            layout.m_Layout[x][y] = erase? TileType.EMPTY : m_CurrentBrush;
        }
    }

    private void SaveChanges(ShipLayout layout)
    {
        EditorUtility.SetDirty(layout);
        AssetDatabase.SaveAssets();
    }
    #endregion
}
