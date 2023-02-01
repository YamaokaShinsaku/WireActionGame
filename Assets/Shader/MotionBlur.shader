Shader "Hidden/MotionBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 0
        _EdgeCoeff ("Edge Coefficient", Float) = 1
        _SpeedCoeff ("Speed Coefficient", Float) = 0
        _BlurCenterPoint ("Blur Center Point", Vector) = (0.5, 0.5, 0.0)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            half _BlurSize;
            half _EdgeCoeff;            // �[�̕������Ɍ��ʂ������邽�߂Ɏ�������W���i�傫���قǒ[�݂̂Ɍ��ʂ��\���j
            half _SpeedCoeff;           // �X�s�[�h�ɉ����đ���������W�� (0 �` 1)�@�i�傫������قǌ��ʂ������j
            half2 _BlurCenterPoint;     // �u���[�̒��S�ƂȂ�_

            static const int BLUR_COUNT = 8;

            // �߂��_���牓���_�Ɍ������ăT���v�����񂮂���ۂ̏d�݂Â��W����ݒ�
            // ���a���P�ɂȂ�悤��
            static const float BLUR_WEIGHTS[BLUR_COUNT] =
            {
                1.0 / BLUR_COUNT,
                1.0 / BLUR_COUNT,
                1.0 / BLUR_COUNT,
                1.0 / BLUR_COUNT,
                1.0 / BLUR_COUNT,
                1.0 / BLUR_COUNT,
                1.0 / BLUR_COUNT,
                1.0 / BLUR_COUNT,
            };

            float magnitude(float2 vec)
            {
                return max(abs(vec.x), abs(vec.y));
            }

            // ��ʏ�ɂ�����u���[�̒��S�_�܂ł̍ő勗�����v�Z����
            float calcMaxDistance()
            {
                // �ǂ̓_�����S�_�������Ƃ��Ă��A�ő勗�������͎̂l�p�̂ǂꂩ
                float distance1 = magnitude(float2(0, 0) - _BlurCenterPoint);
                float distance2 = magnitude(float2(1, 0) - _BlurCenterPoint);
                float distance3 = magnitude(float2(0, 1) - _BlurCenterPoint);
                float distance4 = magnitude(float2(1, 1) - _BlurCenterPoint);

                float maxDistance = max(distance1, max(distance2, max(distance3, distance4)));

                return maxDistance;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = 0;
                
                // �u���[�̒��S����Y���s�N�Z���܂ł̕����x�N�g��
                float2 dir = i.uv - _BlurCenterPoint;

                // �u���[�̒��S����̋����B�����قǋ����u���[��������悤��
                float distance = magnitude(dir);

                // �����x�N�g���𐳋K��
                dir /= sqrt(dir.x * dir.x + dir.y * dir.y);

                // ��ʂ̒��S����ł������_�܂ł̋�����1�ɂȂ�悤�ɋ����𐳋K��
                distance /= calcMaxDistance();

                // distance��0�`1�͈̔͂����̂ŁA�ݏ悷�邱�Ƃł��[�̕����������ʂ̑Ώۂɂ��邱�Ƃ��ł���
                distance = pow(distance, _EdgeCoeff);

                for (int j = 0; j < BLUR_COUNT; j++)
                {
                    float2 Point = i.uv - dir / BLUR_COUNT * j * distance * _SpeedCoeff * _BlurSize;
                    col += tex2D(_MainTex, Point) * BLUR_WEIGHTS[j];
                }

                return col;
            }
            ENDCG
        }
    }
}
