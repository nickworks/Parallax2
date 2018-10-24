using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraScript : MonoBehaviour {
    /// <summary>
    /// We get a reference to the shader we want to use
    /// </summary>
    public Shader shader;
  
    
     void OnEnable()
    {
        if (shader != null)
            //We get our camera component and use the set replacement shader function to make the camera render with the shader
            //It takes a shader  reference and a replacement tag
            //We can leave the tag empty and all objects in the scene are rendered with the replacement shader. If we wanted to 
            //have some objects rendered and some not we could do some fancy stuff by specifying a tag on an objects shader and in here but since we want everything to
            //be rendered with this shader we will not do that
            GetComponent<Camera>().SetReplacementShader(shader, "");
    }
     void OnDisable()
    {
        //If we disable this component then we reset the replacement shader which removes the replacement shader from the camera
        GetComponent<Camera>().ResetReplacementShader();
    }
}
