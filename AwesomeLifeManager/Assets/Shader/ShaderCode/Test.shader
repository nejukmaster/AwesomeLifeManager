Shader "Custom/Test"
{
	Properties
	{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_CellSize("Cell Size", Range(0, 2000)) = 2000
	}

		SubShader
	{
		Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		ZWrite Off Lighting Off Cull Off Fog { Mode Off } Blend SrcAlpha OneMinusSrcAlpha
		LOD 110

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_vct
			#pragma fragment frag_mult 
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			#include "Assets/Shader/ShaderCode/ShaderCginc/Random.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _CellSize;

			struct vin_vct
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f_vct
			{
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			float voronoiNoise(float2 value) {
				float2 baseCell = floor(value);

				float minDistToCell = 10;
				[unroll]
				for (int x = -1; x <= 1; x++) {
					[unroll]
					for (int y = -1; y <= 1; y++) {
						float2 cell = baseCell + float2(x, y);
						float2 cellPosition = cell + rand2dTo2d(cell);
						float2 toCell = cellPosition - value;
						float distToCell = length(toCell);
						if (distToCell < minDistToCell) {
							minDistToCell = distToCell;
						}
					}
				}
				return minDistToCell;
			}

			v2f_vct vert_vct(vin_vct v)
			{
				v2f_vct o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);;
				return o;
			}

			fixed4 frag_mult(v2f_vct i) : COLOR
			{
				float2 value = i.vertex.xy / _CellSize;
				fixed4 col = tex2D(_MainTex, i.texcoord) * i.color;
				float a = col.a * voronoiNoise(value);
				return col;
			}

			ENDCG
		}
	}
}
