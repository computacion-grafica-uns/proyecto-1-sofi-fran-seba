Shader "ShaderVentana"
{
    SubShader
    {
        // 1. Definimos que el objeto es transparente y se debe dibujar después de los opacos
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Pass
        {
            // 2. Activamos el Blending (mezclado)
            // Esto permite que el canal Alpha del color determine qué tanto se ve el fondo
            Blend SrcAlpha OneMinusSrcAlpha

            // 3. Desactivamos ZWrite para que los objetos que estan atras no sean descartados
            ZWrite Off

            // Esto es para ver la ventana desde adentro y afuera
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform float4x4 _ModelMatrix;
            uniform float4x4 _ViewMatrix;
            uniform float4x4 _ProjectionMatrix;

            struct appdata {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = mul(_ProjectionMatrix, mul(_ViewMatrix, mul(_ModelMatrix, v.vertex)));
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_target
            {
                // Retornamos el color con su Alpha original
                return i.color;
            }
            ENDCG
        }
    }
}
