//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    public class Node
    {
        public TileType Type;
        public int gCost;
        public int fCost;
        public Vector2Int LayoutCoordinates;
        public Node fromPrevious;

        public int x
        {
            get { return LayoutCoordinates.x; }
        }

        public int y
        {
            get { return LayoutCoordinates.y; }
        }

        public bool IsWalkable
        {
            get { return TileUtilities.IsWalkable(Type); }
        }

        public Node(TileType type, int x, int y)
        {
            gCost = int.MaxValue;
            fCost = int.MaxValue;
            Type = type;
            LayoutCoordinates = new Vector2Int(x,y);
        }
    }
    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    public static PathFinder instance;
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
    private Node[][] m_NodeLayout;
    private Vector2Int m_Start;
    private Vector2Int m_End;

    #region Unity API
#if UNITY_EDITOR
    private List<Vector2Int> m_TestPath;
    protected void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            if (m_TestPath != null && m_TestPath.Count > 0)
            {
                Vector3 position = Vector3.zero;
                for (int i = 0; i < m_TestPath.Count; ++i)
                {                   
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(new Vector3( m_TestPath[i].x, m_TestPath[i].y, -0.5f), 0.5f);
                }
            }
        }
    }
#endif

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Init();
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    #region Public API
    public void Init()
    {

    }

    public void SetLayout(FloorLayout.sLayout[] layout)
    {
        m_NodeLayout = new Node[layout.Length][];
        for (int x =0; x < layout.Length; ++x)
        {
            m_NodeLayout[x] = new Node[layout[x].Row.Length];
            for (int y = 0; y < layout[x].Row.Length; ++y)
            {
                m_NodeLayout[x][y] = new Node(layout[x][y], x,y);
            }
        }        
    }

    public List<Vector2Int> GetPath(Vector2Int start, Vector2Int finish)
    {
        return A_Star(start, finish);
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private List<Vector2Int> A_Star(Vector2Int start, Vector2Int end)
    {
        m_Start = start;
        m_End = end;

        // The set of nodes already evaluated
        List<Node> closedSet = new List<Node>();

        // The set of currently discovered nodes that are not evaluated yet.
        // Initially, only the start node is known.
        List<Node> openSet = new List<Node>() { GetNode(start) };

        // For each node, which node it can most efficiently be reached from.
        // If a node can be reached from many nodes, cameFrom will eventually contain the
        // most efficient previous step.
        Node currentNode = openSet[0];
        currentNode.gCost = 0;

        // For the first node, that value is completely heuristic.
        SetfCost(currentNode);

        while (openSet.Count > 0)
        {
            currentNode = openSet[0];
            if (currentNode.LayoutCoordinates == end)
            {
                return BuildPath(currentNode);
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            List<Node> neighbours = GetNeighbours(currentNode);

            for (int i = 0; i < neighbours.Count; ++i)
            {
                if (closedSet.Contains(neighbours[i]))
                {
                    continue;
                }

                int currentGCost = currentNode.gCost + 1;

                if (!openSet.Contains(neighbours[i]))
                {
                    openSet.Add(neighbours[i]);
                }
                else if (currentGCost >= neighbours[i].gCost)
                {
                    continue;
                }

                neighbours[i].fromPrevious = currentNode;
                neighbours[i].gCost = currentGCost;
                SetfCost(neighbours[i]);
            }
        }
        return null;
    }


    private Node GetNode(Vector2Int position)
    {
        return m_NodeLayout[position.x][position.y];
    }
    //crows fly heuristic
    private void SetfCost(Node node)
    {
        node.fCost = Mathf.Abs(node.LayoutCoordinates.x - m_End.x) + Mathf.Abs(node.LayoutCoordinates.y - m_End.y);
    }

    private List<Vector2Int> BuildPath(Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node node = endNode;

        path.Insert(0, node.LayoutCoordinates);
        while (node.LayoutCoordinates != m_Start)
        {
            node = node.fromPrevious;
            path.Insert(0, node.LayoutCoordinates);
        }
        return path;
    }

    private List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        //up
        if (node.y > 0 && m_NodeLayout[node.x][node.y - 1].IsWalkable)
        {
            neighbours.Add(m_NodeLayout[node.x][node.y - 1]);
        }
        //down
        if (node.y < m_NodeLayout[node.x].Length - 1 && m_NodeLayout[node.x][node.y + 1].IsWalkable)
        {
            neighbours.Add(m_NodeLayout[node.x][node.y + 1]);
        }
        //right
        if (node.x < m_NodeLayout.Length - 1 && m_NodeLayout[node.x + 1][node.y].IsWalkable)
        {
            neighbours.Add(m_NodeLayout[node.x + 1][node.y]);
        }
        //left
        if (node.x > 0 && m_NodeLayout[node.x - 1][node.y].IsWalkable)
        {
            neighbours.Add(m_NodeLayout[node.x -1][node.y]);
        }

        return neighbours;
    }
    #endregion
}