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
    public const int Width = 8;
    public const int Height = 8;
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
    protected GameObject m_Holder;
    protected MeshFilter m_MeshFilter;
    protected MeshRenderer m_MeshRenderer;
    protected MeshCollider m_MeshCollider;

    protected Block[,,] Blocks;

    protected bool m_HasGenerated = false;
    protected bool m_HasDrawned = false;
    protected bool m_HasRendered = false;
    protected bool m_DrawnLock = false;

    protected MeshData data;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    private int PosX;
    private int PosZ;

   
    //constructor
    public Chunk(int posX, int posZ)
    {
        PosX = posX;
        PosZ = posZ;
    }

    #region Unity API
    #endregion

    #region Public API
    public virtual void Start()
    {
        Blocks = new Block[Width, Height, Width];
        for (int x = 0; x < Width; ++x)
        {
            for (int z = 0; z < Width; ++z)
            {
                for (int y = 0; y < Height; ++y)
                {
                    float perlin = GetHeight(x,y,z);
                    if (perlin > VoxelManager._sCutoff)
                    {
                        Blocks[x, y, z] = Block.Air;
                    }
                    else
                    {                        
                        Blocks[x, y, z] = Block.Dirt;
                    }      
                }
            }
        }
        m_HasGenerated = true;
    }

    public virtual void Tick()
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

    public virtual void OnUnityUpdate()
    {
        if (m_HasGenerated && !m_HasRendered && m_HasDrawned)
        {
            m_HasRendered = true;

            Mesh mesh = data.ToMesh();
            if (m_Holder == null)
            {
                if (!Block.IsInitialized)
                {
                    Block.Initialize();
                }
                
                m_Holder = new GameObject("Chunk" + PosX.ToString() + "_" + PosZ.ToString());
                m_MeshFilter = m_Holder.gameObject.AddComponent<MeshFilter>();
                m_MeshRenderer= m_Holder.gameObject.AddComponent<MeshRenderer>();
                m_MeshCollider = m_Holder.gameObject.AddComponent<MeshCollider>();
                m_Holder.transform.position = new Vector3(PosX * Width, 0f, PosZ * Width);
                m_MeshFilter.sharedMesh = mesh;
                m_MeshCollider.sharedMesh = mesh;
                m_MeshRenderer.material = Block.MATERIALS[Block.BlockType.Dirt];
            }
           
        }
    }

    public float GetHeight(float px, float py, float pz)
    {
        px += (PosX * Width);
        pz += (PosZ * Width);

        float p1 = Mathf.PerlinNoise(px / VoxelManager._sDivision_X * VoxelManager._SeedX, pz / VoxelManager._sDivision_Z * VoxelManager._SeedZ) * VoxelManager._sMultiply;
        p1 *= (VoxelManager._sMultiply_Y * py);

        return p1;
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
