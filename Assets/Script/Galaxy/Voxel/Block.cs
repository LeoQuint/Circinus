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
    private bool m_IsTransparent;

    public static Block Dirt = new Block(false);
    public static Block Air = new Block(true);

    //properties
    public bool IsTransparent
    {
        get { return m_IsTransparent;  }
    }

    //constructor
    public Block(bool isTransparent)
    {
        m_IsTransparent = isTransparent;
    }

    #region Public API


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
}
