// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "WaterShader"
{
	Properties
	{
		_MainTex1("MainTex1", 2D) = "white" {}
		_MainTex2("MainTex2", 2D) = "white" {}
		_Speed("Speed", Vector) = (0.08,0,0,0)
		_Speed1("Speed", Vector) = (0,0.12,0,0)
		_Color0("Color 0", Color) = (0,0.6262352,1,0)
		[HDR]_Color1("Color 1", Color) = (0.8312753,3.334532,5.340313,0)
		_Noise("Noise", 2D) = "white" {}
		_Noise1("Noise", 2D) = "white" {}
		_NoiseStr("NoiseStr", Range( 0 , 1)) = 0.05
		_NoiseStr1("NoiseStr", Range( 0 , 1)) = 0.1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		AlphaToMask Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"

			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform float4 _Color0;
			uniform float4 _Color1;
			uniform sampler2D _MainTex1;
			uniform float2 _Speed;
			uniform sampler2D _Noise;
			uniform float4 _Noise_ST;
			uniform float _NoiseStr;
			uniform sampler2D _MainTex2;
			uniform float2 _Speed1;
			uniform sampler2D _Noise1;
			uniform float4 _Noise1_ST;
			uniform float _NoiseStr1;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float2 uv_Noise = i.ase_texcoord1.xy * _Noise_ST.xy + _Noise_ST.zw;
				float2 panner2 = ( 1.0 * _Time.y * _Speed + ( float4( i.ase_texcoord1.xy, 0.0 , 0.0 ) + ( tex2D( _Noise, uv_Noise ) * _NoiseStr ) ).rg);
				float2 uv_Noise1 = i.ase_texcoord1.xy * _Noise1_ST.xy + _Noise1_ST.zw;
				float2 panner7 = ( 1.0 * _Time.y * _Speed1 + ( float4( i.ase_texcoord1.xy, 0.0 , 0.0 ) + ( tex2D( _Noise1, uv_Noise1 ) * _NoiseStr1 ) ).rg);
				float4 lerpResult10 = lerp( _Color0 , _Color1 , ( tex2D( _MainTex1, panner2 ) * tex2D( _MainTex2, panner7 ) ));
				
				
				finalColor = lerpResult10;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19105
Node;AmplifyShaderEditor.PannerNode;2;-1169.738,-57.14644;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;7;-1157.844,453.2717;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;10;4.819458,56.73145;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;11;-467.1805,-407.2686;Inherit;False;Property;_Color0;Color 0;4;0;Create;True;0;0;0;False;0;False;0,0.6262352,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;12;-462.1805,-203.2686;Inherit;False;Property;_Color1;Color 1;5;1;[HDR];Create;True;0;0;0;False;0;False;0.8312753,3.334532,5.340313,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexCoordVertexDataNode;3;-1964.441,-282.9461;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-1644.405,-98.00269;Inherit;False;2;2;0;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;-1468.712,399.9458;Inherit;False;2;2;0;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;6;-1971.714,500.1056;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;4;-1419.344,100.1532;Inherit;False;Property;_Speed;Speed;2;0;Create;True;0;0;0;False;0;False;0.08,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;19;-1421.857,614.6874;Inherit;False;Property;_Speed1;Speed;3;0;Create;True;0;0;0;False;0;False;0,0.12;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SamplerNode;13;-2143.405,-8.002686;Inherit;True;Property;_Noise;Noise;6;0;Create;True;0;0;0;False;0;False;-1;f3ea51b4bc68b264c85f252f29504d86;f3ea51b4bc68b264c85f252f29504d86;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-1804.405,60.99731;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-2122.405,247.9973;Inherit;False;Property;_NoiseStr;NoiseStr;8;0;Create;True;0;0;0;False;0;False;0.05;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;20;-2193.784,653.8698;Inherit;True;Property;_Noise1;Noise;7;0;Create;True;0;0;0;False;0;False;-1;f3ea51b4bc68b264c85f252f29504d86;f3ea51b4bc68b264c85f252f29504d86;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1854.784,722.8698;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-2172.784,909.8698;Inherit;False;Property;_NoiseStr1;NoiseStr;9;0;Create;True;0;0;0;False;0;False;0.1;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-442.1946,72.3717;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-832,-80;Inherit;True;Property;_MainTex1;MainTex1;0;0;Create;True;0;0;0;False;0;False;-1;432ef94ab2357044d93c4663d13c6dec;432ef94ab2357044d93c4663d13c6dec;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-828.294,355.7717;Inherit;True;Property;_MainTex2;MainTex2;1;0;Create;True;0;0;0;False;0;False;-1;9c30b2d642850284984da3063f2ac1c5;9c30b2d642850284984da3063f2ac1c5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;23;357.1998,56.19999;Float;False;True;-1;2;ASEMaterialInspector;100;5;WaterShader;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;RenderType=Opaque=RenderType;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;1;True;False;;False;0
WireConnection;2;0;17;0
WireConnection;2;2;4;0
WireConnection;7;0;18;0
WireConnection;7;2;19;0
WireConnection;10;0;11;0
WireConnection;10;1;12;0
WireConnection;10;2;9;0
WireConnection;17;0;3;0
WireConnection;17;1;15;0
WireConnection;18;0;6;0
WireConnection;18;1;21;0
WireConnection;15;0;13;0
WireConnection;15;1;16;0
WireConnection;21;0;20;0
WireConnection;21;1;22;0
WireConnection;9;0;1;0
WireConnection;9;1;8;0
WireConnection;1;1;2;0
WireConnection;8;1;7;0
WireConnection;23;0;10;0
ASEEND*/
//CHKSM=8AD99E24AD5A730BFA7C28482F2DC4E15238ABB4