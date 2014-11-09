float4 DestColor;
float Time;

#define SAMPLES 8

texture2D ColorMap;
sampler2D ColorMapSampler = sampler_state
{
	Texture = <ColorMap>;
};

struct PixelShaderInput
{
	float2 TexCoord : TEXCOORD0;
};

float4 PixelShaderFunction(PixelShaderInput input) : COLOR0
{
	float4 sum = float4(0,0,0,0);

	for (int i = -SAMPLES / 2; i < SAMPLES / 2; ++i) {
		sum += tex2D(ColorMapSampler, input.TexCoord + float2(i, 0));
	}

	return sum / SAMPLES;
}

technique Technique1
{
	pass ColorTransform
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}