Shader "Experimental/ZoomInFade"
{
	Properties
	{
		
		
	}
	SubShader
	{
		//The object has a render type opaque
		Tags
		{
			"RenderType" = "Opaque"
		}
		//We turn ZWritting on
		//Zwritting controls which pixals are added to the depth buffer 
		ZWrite On
		//We have our first shader pass here
		Pass
		{
			//We start writing CG code
			CGPROGRAM
			//WE have a vertex pragma called vert
			#pragma vertex vert
			//We have a fragment pragma called frag
			#pragma fragment frag
			//We include UnityCG.cginc which has unity's built in cg methods
			#include "UnityCG.cginc"
			//We have our appdata struct which passes raw geometry rendering info to our vertex shader
			struct appdata
			{
				//WE want our vertex positions in local space
				float4 vertex : POSITION;
			};
			//We have our v2f struct
			//This passes info from our vertex shader to our fragment shader
			struct v2f
			{
				//This is the position of the vertex after being transformed into projection space
				float4 vertex : SV_POSITION;
				//We get our vertex's depth value
				float depth : DEPTH;
			};

			//we have our vertex function it is a v2f struct and it recieve our models geometry
			v2f vert(appdata v)
			{
				//V2f o is created
				v2f o;
				//o.vertex is set equal to v.vertex after being transformed into clipping space
				o.vertex = UnityObjectToClipPos(v.vertex);
				//Depth is equal to the current model times view matrix plus the projection parameters of the 1/farplane of the camera
				o.depth = -mul(UNITY_MATRIX_MV, v.vertex).z * _ProjectionParams.w;
				//We return o
				return o;
			}
			
			//We have our fragment shader here
			fixed4 frag(v2f i) : SV_Target
			{
				//It takes the depth from our v2f and sets it equal to a float
				float depth = i.depth;
				//We return that value in a fixed 4
				return fixed4(depth, depth, depth, 1);
			}


			ENDCG
		}
	}


}