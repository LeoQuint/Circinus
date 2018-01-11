using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAfterEffect : MonoBehaviour {


    public Material m_Material;
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, m_Material);
    }
}
