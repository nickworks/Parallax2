Shader "Experimental/ZoomInFade"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TintBack ("Background", Color) = (1,1,1,1)
		_TintFore ("Foreground", Color) = (0,0,0,1)
		_Distance ("Distance", Float) = 400
		_Separation ("Separation", Float) = 100
	}
    SubShader
    {
        Tags { "RenderType"="Opaque" }
       
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _TintBack;
			fixed4 _TintFore;
			float _Distance;
			float _Separation;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 screenuv : TEXCOORD1;
            };
           
            v2f vert (appdata_full v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenuv = ComputeScreenPos(o.pos);
                return o;
            }
           
            sampler2D _CameraDepthTexture;
 
            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.screenuv.xy / i.screenuv.w;
                float depth = 1 - Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv));
				fixed4 pixel = tex2D(_MainTex, uv);

				if(depth < _Distance){
					depth = (_Distance - depth)/_Distance; // 0-to-_Dis becomes 0-to-1
					pixel = lerp(pixel, _TintBack, depth);
				} else{
					depth = (depth-_Distance)/_Separation; //_Dis-to-1 becomes 0-to-1
					if(depth > 1) depth = 1;
					pixel = lerp(pixel, _TintFore, depth);
				}

				return pixel;
            }
            ENDCG
        }
    }
}