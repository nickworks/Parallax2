using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessEffect : MonoBehaviour {

    public Shader effect;
    Material mat;

    void Start()
    {
        mat = new Material(effect);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        source.wrapMode = TextureWrapMode.Repeat;
        Graphics.Blit(source, destination, mat);
    }
}
