//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeshData {

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

    private List<Vector3> verts = new List<Vector3>();
    private List<int> tris = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();

    //constructor
    public MeshData(List<Vector3> v, List<int> t, List<Vector2> u)
    {
        verts = v;
        tris = t;
        uvs = u;
    }

    public MeshData() { }

    #region Unity API
    #endregion

    #region Public API
    public void Merge(MeshData m)
    {
        if (m.verts.Count <= 0)
        {
            return;
        }
        if (verts.Count <= 0)
        {
            verts = m.verts;
            tris = m.tris;
            uvs = m.uvs;
            return;
        }

        int count = verts.Count;

        verts.AddRange(m.verts);

        for (int i = 0; i < m.tris.Count; ++i)
        {
            tris.Add(m.tris[i] + count);
        }

        uvs.AddRange(m.uvs);
    }

    public Mesh ToMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        MeshUtility.Optimize(mesh);
        return mesh;
    }

    public void AddPosition(Vector3 position)
    {
        for (int i = 0; i < verts.Count; ++i)
        {
            verts[i] = verts[i] + position;
        }
    }

    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
