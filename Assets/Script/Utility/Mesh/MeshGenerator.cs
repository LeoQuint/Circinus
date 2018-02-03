//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    //Struct
    public struct Triangle
    {
        public int vertexIndexA, vertexIndexB, vertexIndexC;

        int[] vertices;

        public Triangle(int _vertexA, int _vertexB, int _vertexC)
        {
            vertexIndexA = _vertexA;
            vertexIndexB = _vertexB;
            vertexIndexC = _vertexC;
            vertices = new int[3] { vertexIndexA, vertexIndexB, vertexIndexC };
        }

        public int this[int i]
        {
            get
            {
                return vertices[i];
            }
        }

        public bool Contains(int vertexIndex)
        {
            return vertexIndex == vertexIndexA || vertexIndex == vertexIndexB || vertexIndex == vertexIndexC;
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

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    private Dictionary<int, List<Triangle>> triangleDictionary = new Dictionary<int, List<Triangle>>();
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private HashSet<int> checkedVertices = new HashSet<int>();
   
    #region Unity API
    private void Start()
    {
       
        
       
        
    }
    #endregion

    #region Public API
    public Mesh GenerateMesh(Vector3 origin, List<Vector3> points)
    {
        vertices.Clear();
        triangles.Clear();

        vertices.Add(origin);
        for (int i = 0; i < points.Count; ++i)
        {
            vertices.Add(points[i]);
        }

        for (int i = 0; i < points.Count; ++i)
        {
            triangles.Add(0);
            int index = i + 1;
            index = (index > vertices.Count - 1) ? 1 : index;
            triangles.Add(index);
            ++index;
            index = (index > vertices.Count - 1) ? 1 : index;
            triangles.Add(index);
        }

        Mesh mesh = new Mesh();        
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        return mesh;
    }

    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}

public class Node
{
    public Vector3 position;
    public int vertexIndex = -1;

    public Node(Vector3 _Pos)
    {
        position = _Pos;
    }
}

public class ControlNode : Node
{
    public bool isActive;
    public Node aboveNode;
    public Node rightNode;

    public ControlNode(Vector3 _Pos, bool _active, float squareSize) : base(_Pos)
    {
        isActive = _active;
        aboveNode = new Node(position + (Vector3.forward * squareSize / 2f));
        rightNode = new Node(position + (Vector3.right * squareSize / 2f));
    }
}