//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelper {

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

    #region Unity API
    #endregion

    #region Public API
    public static MeshData DrawCube(Chunk chunk, Block[,,] blocks, Block block, int x, int y, int z)
    {

        MeshData d = new MeshData();

        if (block.Equals(Block.Air))
        {
            return new MeshData();
        }
        //bottom
        if (y - 1 < 0 || blocks[x, y-1, z].IsTransparent)
        {
            d.Merge(new MeshData(
                new List<Vector3>()
                {
                    Vector3.zero,
                    Vector3.forward,
                    Vector3.right,
                    new Vector3(1f,0f,1f)
                },
                new List<int>()
                {
                    0,2,1,3,1,2
                },
                new List<Vector2>()
                {
                    Vector2.one,
                    Vector2.up,
                    Vector2.right,
                    Vector2.one
                }
                ));
        }
        //top
        if(y + 1 >= Chunk.Width || blocks[x, y + 1, z].IsTransparent)
        {
            d.Merge(new MeshData(
                new List<Vector3>()
                {
                    Vector3.up,
                    new Vector3(0f,1f,1f),
                    new Vector3(1f,1f,0f),
                    Vector3.one
                },
                new List<int>()
                {
                    0,1,2,3,2,1
                },
                new List<Vector2>()
                {
                    Vector2.zero,
                    Vector2.right,
                    Vector2.up,
                    Vector2.one
                }
                ));
        }
        //back
        if (x + 1 >= Chunk.Width || blocks[x + 1, y, z].IsTransparent)
        {
            d.Merge(new MeshData(
                new List<Vector3>()
                {
                    Vector3.right,
                    new Vector3(1f,0f,1f),
                    new Vector3(1f,1f,0f),
                    Vector3.one
                },
                new List<int>()
                {
                    0,2,1,3,1,2
                },
                new List<Vector2>()
                {
                    Vector2.zero,
                    Vector2.right,
                    Vector2.up,
                    Vector2.one
                }
                ));
        }
        //front
        if (x - 1 < 0 || blocks[x - 1, y, z].IsTransparent)
        {
            d.Merge(new MeshData(
                new List<Vector3>()
                {
                    Vector3.zero,
                    new Vector3(0f,0f,1f),
                    Vector3.up,
                    new Vector3(0f,1f,1f)
                },
                new List<int>()
                {
                    0,1,2,3,2,1
                },
                new List<Vector2>()
                {
                    Vector2.zero,
                    Vector2.up,
                    Vector2.right,
                    Vector2.one
                }
                ));
        }
        //right side
        if (z + 1 >= Chunk.Width || blocks[x , y, z + 1].IsTransparent)
        {
            d.Merge(new MeshData(
                new List<Vector3>()
                {
                    Vector3.forward,
                    new Vector3(1f,0f,1f),                   
                    new Vector3(0f,1f,1f),
                     Vector3.one
                },
                new List<int>()
                {
                    0,1,2,3,2,1
                },
                new List<Vector2>()
                {
                    Vector2.zero,
                    Vector2.up,
                    Vector2.right,
                    Vector2.one
                }
                ));
        }
        //left side
        if (z - 1 < 0 || blocks[x, y, z - 1].IsTransparent)
        {
            d.Merge(new MeshData(
                new List<Vector3>()
                {
                    Vector3.zero,
                    Vector3.right, 
                    Vector3.up,
                    new Vector3(1f,1f,0f)
                },
                new List<int>()
                {
                    0,2,1,3,1,2
                },
                new List<Vector2>()
                {
                    Vector2.zero,
                    Vector2.up,
                    Vector2.right,
                    Vector2.one
                }
                ));
        }

        d.AddPosition(new Vector3(x - 0.5f,y -0.5f,z - 0.5f));

        return d;
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
