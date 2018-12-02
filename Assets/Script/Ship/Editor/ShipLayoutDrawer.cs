//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(ShipLayout))]
public class ShipLayoutDrawer : Editor {

    public enum eBrushType
    {
        GROUND,
        COMPONENT,
        MODIFIER
    }

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const string TEXTURE_FOLDER_PATH = "Assets/Resources/Editor/ShipLayout/";    

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
    private bool m_IsMouseScrolling = false;

    private eBrushType m_CurrentBrush = eBrushType.GROUND;

    private TileType m_GroundBrush = TileType.EMPTY;
    private eShipComponent m_ComponentBrush = eShipComponent.EMPTY;
    private eTileModifier m_ModifierBrush = eTileModifier.NONE;

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

        DrawEditoringTools(shipLayout);
        DrawLayout(shipLayout);
       
        EditorGUILayout.EndVertical();

        GetMouseInput(shipLayout);
    }
    #endregion

    #region Public API
    #endregion

    #region Protect
    #endregion

    #region Private
    int selGridInt = 0;

    private void DrawEditoringTools(ShipLayout shipLayout)
    {
        //Sliders
        EditorGUILayout.BeginVertical("window");
            EditorGUILayout.BeginVertical("box");
                shipLayout._LeftOffsetDrawerSaved = EditorGUILayout.Slider("Left", shipLayout._LeftOffsetDrawerSaved, -1000f, 1000f);
                shipLayout._TopOffsetDrawerSaved = EditorGUILayout.Slider("Top", shipLayout._TopOffsetDrawerSaved, -1000f, 1000f);
                shipLayout._SizeDrawerSaved = EditorGUILayout.Slider("Size", shipLayout._SizeDrawerSaved, 1f, 500f);           
            EditorGUILayout.EndVertical();

            List<string> nameList = new List<string>();
            foreach (eBrushType b in Enum.GetValues(typeof(eBrushType)))
            {
                nameList.Add(b.ToString());
            }
            string[] names = nameList.ToArray();
            EditorGUILayout.BeginHorizontal("box");
                selGridInt = GUILayout.SelectionGrid(selGridInt, names, 3);
            EditorGUILayout.EndHorizontal();

            m_CurrentBrush = (eBrushType)selGridInt;
            EditorGUILayout.BeginHorizontal("box");
                switch (m_CurrentBrush)
                {
                    case eBrushType.GROUND:
                        m_GroundBrush = (TileType)EditorGUILayout.EnumPopup("Brush", m_GroundBrush);
                        break;

                    case eBrushType.MODIFIER:
                        m_ModifierBrush = (eTileModifier)EditorGUILayout.EnumPopup("Brush", m_ModifierBrush);
                        break;

                    case eBrushType.COMPONENT:
                        m_ComponentBrush = (eShipComponent)EditorGUILayout.EnumPopup("Brush", m_ComponentBrush);
                        break;
                }
            EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    private void DrawLayout(ShipLayout shipLayout)
    {
        if (shipLayout.m_Layout != null && shipLayout.m_Layout.Length > 0)
        {
            Rect positions = new Rect(shipLayout._LeftOffsetDrawerSaved, shipLayout._TopOffsetDrawerSaved,
                shipLayout._SizeDrawerSaved, shipLayout._SizeDrawerSaved);
            
            for (int i = 0; i < shipLayout.m_Layout.Length; ++i)
            {
                Vector3 iVector = (Vector3.right * i * shipLayout._SizeDrawerSaved);
                for (int j = 0; j < shipLayout.m_Layout[i].Row.Length; ++j)
                {
                    Vector3 offset = iVector + (Vector3.up * j * shipLayout._SizeDrawerSaved);
                    Rect rect = new Rect( positions);
                    rect.x += offset.x;
                    rect.y += offset.y;

                    DrawGround(rect, shipLayout.m_Layout[i].Row[j].Type);
                    DrawComponents(rect, shipLayout.m_Layout[i].Row[j]);
                    DrawModifier(rect, shipLayout.m_Layout[i].Row[j]);
                }
            }
        }
    }

    private void DrawGround(Rect rect, TileType type)
    {
        EditorGUI.DrawRect(rect, GetGizmosColor(type));
    }

    private void DrawComponents(Rect rect, sTileInfo info)
    {
        if (info.Component != eShipComponent.EMPTY)
        {
            rect.size /= 4f;
            Texture2D t = (Texture2D)AssetDatabase.LoadAssetAtPath(TEXTURE_FOLDER_PATH + info.Component.ToString() + ".png", typeof(Texture2D));
            EditorGUI.DrawPreviewTexture(rect, t);
        }
    }

    private void DrawModifier(Rect rect, sTileInfo info)
    {
        if(info.Modifiers != null)
        {
            rect.x += rect.size.x - (rect.size.x / 4f);
            rect.size /= 4f;
            
            for (int i = 0; i < info.Modifiers.Count; ++i)
            {
                rect.y += (rect.size.x * i);
                if (info.Modifiers[i] != eTileModifier.NONE)
                {
                    Texture2D t = (Texture2D)AssetDatabase.LoadAssetAtPath(TEXTURE_FOLDER_PATH + info.Modifiers[i].ToString() + ".png", typeof(Texture2D));
                    EditorGUI.DrawPreviewTexture(rect, t);
                }
            }
        }
    }

    private void GetMouseInput(ShipLayout shipLayout)
    {
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
        if ((!m_IsClicking && Event.current.isMouse && Event.current.clickCount > 0 && Event.current.button == 0) || m_ClickHeld)
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

            Repaint();
        }

        if (Event.current.type == EventType.ScrollWheel)
        {
            shipLayout._SizeDrawerSaved += Event.current.delta.y;
            Event.current.Use();
            Repaint();
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

            case TileType.DOOR:
                return new Color(1f, 1f, 0f, 0.5f);

            case TileType.STEEL:
                return Color.grey;
        }

        return Color.magenta;
    }
    
    private void RefreshArray(ShipLayout layout)
    {
        sTileInfo[][] newLayout = new sTileInfo[layout._Width][];
        for (int i = 0; i < newLayout.Length; ++i)
        {
            newLayout[i] = new sTileInfo[layout._Height];
            if (layout.m_Layout != null && layout.m_Layout.Length > i)
            {
                for (int j = 0; j < newLayout[i].Length; ++j)
                {
                    if (layout.m_Layout[i].Row != null && layout.m_Layout[i].Row.Length > j)
                    {
                        newLayout[i][j] = layout.m_Layout[i].Row[j];
                    }
                    else
                    {
                        newLayout[i][j] = new sTileInfo();
                    }
                }
            }
        }
        layout.SetLayout(newLayout);
        SaveChanges(layout);
    }
    
    private void Empty(ShipLayout layout)
    {
        sTileInfo[][] newLayout = new sTileInfo[layout._Width][];
        for (int i = 0; i < newLayout.Length; ++i)
        {
            newLayout[i] = new sTileInfo[layout._Height];
        }
        layout.SetLayout(newLayout);
        SaveChanges(layout);
    }

    private void AssignTypeAt(Vector3 worldPosition, ShipLayout layout, bool erase = false)
    {
        int x = (int)((worldPosition.x - layout._LeftOffsetDrawerSaved) / layout._SizeDrawerSaved);
        int y = (int)((worldPosition.y - layout._TopOffsetDrawerSaved)/ layout._SizeDrawerSaved);

        if (x >= 0 && y >= 0 && layout.m_Layout.Length > x && layout.m_Layout[x].Row.Length > y)
        {
            sTileInfo tileInfo = layout[x, y];
            switch (m_CurrentBrush)
            {
                case eBrushType.GROUND:
                    tileInfo.Type = erase ? TileType.EMPTY : m_GroundBrush;
                    break;

                case eBrushType.MODIFIER:
                    if (tileInfo.Modifiers == null)
                    {
                        tileInfo.Modifiers = new List<eTileModifier>();
                    }

                    if (erase && tileInfo.Modifiers.Contains(m_ModifierBrush))
                    {
                        tileInfo.Modifiers.Remove(m_ModifierBrush);
                    }
                    else if (!erase && !tileInfo.Modifiers.Contains(m_ModifierBrush))
                    {
                        tileInfo.Modifiers.Add(m_ModifierBrush);
                    }
                   
                    break;

                case eBrushType.COMPONENT:
                    tileInfo.Component = erase ? eShipComponent.EMPTY : m_ComponentBrush;
                    break;
            }          
           
            layout[x, y] = tileInfo;
        }
       
        Repaint();
    }

    private void SaveChanges(ShipLayout layout)
    {
        EditorUtility.SetDirty(layout);
        AssetDatabase.SaveAssets();
    }
    #endregion
}