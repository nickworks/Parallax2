using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraScript : MonoBehaviour {
    /// <summary>
    /// A reference to the shader we want to use
    /// </summary>
    public Shader shader;
    public Color foreground;
    public Color background;
    Material mat;


    void Start()
    {
        Camera cam = GetComponent<Camera>();
        float depthRange = cam.farClipPlane - cam.nearClipPlane;

        mat = new Material(shader);
        mat.SetColor("_TintBack", background);
        mat.SetColor("_TintFore", foreground);
        mat.SetFloat("_Distance", 400 / depthRange);
        mat.SetFloat("_Separation", 100 / depthRange);
        //GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        source.wrapMode = TextureWrapMode.Repeat;
        Graphics.Blit(source, destination, mat);
    }
}
