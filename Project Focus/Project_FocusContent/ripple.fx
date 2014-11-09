float4 DestColor;
float Time;

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
	float4 srcRGBA = tex2D(ColorMapSampler, input.TexCoord + float2(sin(Time * 2.0f + input.TexCoord.y * 3.0f) * 0.02f, 0.0));

	return srcRGBA;
}

technique Technique1
{
	pass ColorTransform
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}