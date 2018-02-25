//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : ITickable{

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    public const int Width = 10;
    public const int Height = 10;
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

    private int PosX;
    private int PosZ;
    
    private Block[,,] Blocks;

    private bool m_HasGenerated = false;
    private bool m_HasDrawned = false;
    private bool m_HasRendered = false;
    private bool m_DrawnLock = false;

    private MeshData data;

    public Chunk(int posX, int posZ)
    {
        PosX = posX;
        PosZ = posZ;
    }

    #region Unity API
    #endregion

    #region Public API
    public void Start()
    {
        
        Blocks = new Block[Width, Height, Width];
        for (int x = 0; x < Width; ++x)
        {
            for (int y = 0; y < Height; ++y)
            {
                for (int z = 0; z < Width; ++z)
                {
                    Blocks[x, y, z] = Block.Dirt;
                }
            }
        }
        m_HasGenerated = true;
    }

    public void Tick()
    {
        
    }

    public void Update()
    {
        if (!m_HasDrawned && m_HasGenerated && !m_DrawnLock)
        {
            m_DrawnLock = true;
            data = new MeshData();
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    for (int z = 0; z < Width; ++z)
                    {
                        data.Merge(Blocks[x,y,z].Draw(this, Blocks, x,y,z));
                    }
                }
            }            
        }
        m_DrawnLock = false;
        m_HasDrawned = true;
    }

    public void OnUnityUpdate()
    {
        if (m_HasGenerated && !m_HasRendered && m_HasDrawned)
        {
            m_HasRendered = true;

            Mesh mesh = data.ToMesh();
            GameObject go = new GameObject();
            Transform t = go.transform;
            MeshFilter mf = t.gameObject.AddComponent<MeshFilter>();
            MeshRenderer mr = t.gameObject.AddComponent<MeshRenderer>();
            MeshCollider mc = t.gameObject.AddComponent<MeshCollider>();

            mf.sharedMesh = mesh;
            mc.sharedMesh = mesh;
        }
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
