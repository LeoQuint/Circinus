//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

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
    protected TileType m_Type;
    protected MeshRenderer m_Renderer;
    protected MeshFilter m_MeshFilter;
    protected Mesh m_Mesh;
    protected Material m_Material;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private bool m_IsWalkable = true;
    private int m_Cost = 100;

    //Sorted list by priority
    private List<TileModifier> m_Modifiers = new List<TileModifier>();

    public bool IsWalkable
    {
        get { return m_IsWalkable; }
    }

    public int Cost
    {
        get { return m_Cost; }
    }
    #region Unity API
    #endregion

    #region Public API
    public void Init(TileType type, Material mat)
    {
        m_Type = type;
        m_IsWalkable = TileUtilities.IsWalkable(m_Type);
        Build(mat);
    }
    #endregion

    #region Protect
    protected void Build(Material mat)
    {
        m_Renderer = gameObject.AddComponent<MeshRenderer>();
        m_MeshFilter = gameObject.AddComponent<MeshFilter>();
        m_MeshFilter.mesh = CreateMesh();
        m_Renderer.sharedMaterial = mat;
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
