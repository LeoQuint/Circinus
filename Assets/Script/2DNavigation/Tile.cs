//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, ISelectable {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const float CUBE_HALF_SIZE = 0.5f;
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    private static Vector3 BottomLeft = new Vector3(-1f,-1f, 0f);
    private static Vector3 BottomRight = new Vector3(1f, -1f, 0f);
    private static Vector3 TopLeft = new Vector3(-1f, 1f, 0f);
    private static Vector3 TopRight = new Vector3(1f, 1f, 0f);
    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    protected sTileInfo m_Info;
    protected MeshRenderer m_Renderer;
    protected MeshFilter m_MeshFilter;
    protected Mesh m_Mesh;
    protected Material m_Material;
    protected BoxCollider2D m_Collider;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private bool m_IsWalkable = true;

    public Vector2Int Position
    {
        get { return m_Info.Position; }
    }

    public bool CanControl
    {
        get { return false; }
    }

    public int SelectPriority
    {
        get { return 0; }
    }

    public bool IsWalkable
    {
        get { return m_IsWalkable; }
    }

    public eSelectableType SelectableType
    {
        get
        {
            return eSelectableType.TILE;
        }
    }
    #region Unity API
    #endregion

    #region Public API
    public void Init(sTileInfo info, Material mat)
    {
        m_Info = info;
        m_IsWalkable = TileUtilities.IsWalkable(m_Info.Type);
        Build(mat);
    }

    public void Select()
    {
        Debug.Log("Selecting tile: " + gameObject.name);
    }

    public void Deselect()
    {
        Debug.Log("Deselecting tile: " + gameObject.name);
    }
    #endregion

    #region Protect
    protected void Build(Material mat)
    {
        m_Renderer = gameObject.AddComponent<MeshRenderer>();
        m_MeshFilter = gameObject.AddComponent<MeshFilter>();
        m_MeshFilter.mesh = CreateMesh();
        m_Renderer.sharedMaterial = mat;
        AddCollider();
    }

    protected void AddCollider()
    {
        m_Collider = gameObject.AddComponent<BoxCollider2D>();
        m_Collider.size = Vector2.one * CUBE_HALF_SIZE * 2f;
    }
    #endregion

    #region Private
    private Mesh CreateMesh()
    {
        m_Mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        vertices[0] = BottomLeft * CUBE_HALF_SIZE;
        vertices[1] = TopLeft * CUBE_HALF_SIZE;
        vertices[2] = TopRight * CUBE_HALF_SIZE;
        vertices[3] = BottomRight * CUBE_HALF_SIZE;

        Vector2[] UVs = new Vector2[4];
        UVs[0] = Vector2.zero;
        UVs[1] = Vector2.up;
        UVs[2] = Vector2.one;
        UVs[3] = Vector2.right;

        int[] triangles = new int[] 
        {
            0,1,2,
            0,2,3
        };
               
        m_Mesh.vertices = vertices;
        m_Mesh.uv = UVs;
        m_Mesh.triangles = triangles;

        return m_Mesh;
    }
    #endregion
}
