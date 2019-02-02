//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshMaker : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const string path = "Assets/Scenes/Junk/Meshes/";
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    public MeshGenerator m_Generator;
    public string m_AssetName;
    public List<Vector3> m_Points = new List<Vector3>();
    public List<int> m_Triangle = new List<int>();
    public List<Vector2> m_Uvs = new List<Vector2>();
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 0, 0, 0.75F);
        for (int i =0; i < m_Points.Count; ++i)
        {
            Gizmos.DrawCube(transform.position + m_Points[i],Vector3.one);
        }       
    }
    #endregion

    #region Public API
    [ContextMenu("Create Mesh")]
    public void CreateMesh()
    {
        //Mesh m = m_Generator.GenerateMesh(Vector3.zero, m_Points);
        Mesh m = new Mesh();
        m.vertices = m_Points.ToArray();
        m.triangles = m_Triangle.ToArray();
        m.uv = m_Uvs.ToArray();


        UnityEditor.AssetDatabase.CreateAsset(m, string.Format("{0}{1}{2}", path, m_AssetName, ".mesh") );
        UnityEditor.AssetDatabase.SaveAssets();
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
