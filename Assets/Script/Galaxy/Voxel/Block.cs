//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : ITickable
{
    //const
    private const string MATERIAL_FOLDER_PATH = "Voxel/BlockMaterials/";   

    public static Block Dirt = new Block(false, BlockType.Dirt);
    public static Block Air = new Block(true, BlockType.Air);

    public static Dictionary<BlockType, Material> MATERIALS;
    public static bool IsInitialized = false;

    public BlockType m_BlockType;
    private bool m_IsTransparent;

    //enums
    public enum BlockType
    {
        Air,
        Dirt,
        Grass,
        COUNT
    }

    //properties
    public BlockType Type
    {
        get { return m_BlockType;  }
    }

    public bool IsTransparent
    {
        get { return m_IsTransparent;  }
    }

    //constructor
    public Block(bool isTransparent, BlockType type)
    {
        m_IsTransparent = isTransparent;
        m_BlockType = type; 
    }

    #region Public API
    public static void Initialize()
    {
        LoadMaterials();
        IsInitialized = true;
    }

    public void Start()
    {
        
    }

    public void Tick()
    {
        
    }

    public void Update()
    {
       
    }

    public void OnUnityUpdate()
    {

    }

    public virtual MeshData Draw(Chunk chunk, Block[,,] blocks, int x, int y, int z)
    {
        return MathHelper.DrawCube(chunk, blocks, this, x ,y ,z);
    }

    #endregion

    #region private 
    public static void LoadMaterials()
    {
        MATERIALS = new Dictionary<BlockType, Material>();
        MATERIALS.Clear();
        for (BlockType i = BlockType.Dirt; i != BlockType.COUNT; ++i)
        {
            MATERIALS.Add(i, Resources.Load<Material>(MATERIAL_FOLDER_PATH + i.ToString()));
        }        
    }
    #endregion
}
