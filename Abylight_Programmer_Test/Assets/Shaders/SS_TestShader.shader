Shader "Custom/SS_TestShader"

{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_MainColor("Main Color", Color) = (1,1,1,1)
		_MaskTex("Mask Texture", 2D) = "white" {}
		_MaskColor("Mask Color", Color) = (1,1,1,1)
	}

		SubShader
		{

			Pass
			{

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

			// Properties
			sampler2D _MainTex;
			sampler2D _MaskTex;
			float4 _MaskColor;
			float4 _MainColor;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float3 texCoord : TEXCOORD0;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float3 normal : NORMAL;
				float3 texCoord : TEXCOORD0;
			};
			
			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;

				output.pos = UnityObjectToClipPos(input.vertex);
				output.normal = UnityObjectToWorldNormal(input.normal);

				output.texCoord = input.texCoord;

				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{

				// albedo
				float4 albedo = tex2D(_MainTex, input.texCoord.xy);

				// mask
				float isMask = tex2D(_MaskTex, input.texCoord.xy);
				


				if (isMask == 1)
					albedo = albedo;
				else
					albedo = abs(1 - albedo);

				// final
				float3 rgb = albedo.rgb;
				return float4(rgb, 1.0);
			}

			ENDCG
		}
		}
			Fallback "Diffuse"
}