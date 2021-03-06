float4x4 World;
float4x4 View;
float4x4 Projection;
float4x4 WorldInverseTranspose;
 
float4 AmbientColor = float4(0.1, 0.1, 0.1, 1);
 
float3 DiffuseLightDirection = float3(0, 0, 0);
float4 DiffuseColor = float4(1, 1, 1, 1);
float DiffuseIntensity = 0.2;
 
float Shininess = 100;
float4 SpecularColor = float4(1, 1, 1, 1);
float SpecularIntensity = 0.05;
float3 ViewVector = float3(1, 0, 0);

float Saturation = 1;
 
texture ModelTexture;
sampler2D textureSampler = sampler_state {
    Texture = (ModelTexture);
    MinFilter = Linear;
    MagFilter = Linear;
	MipFilter = None;
    AddressU = Clamp;
    AddressV = Clamp;
};
 
struct VertexShaderInput
{
    float4 Position : POSITION0;
    float4 Normal : NORMAL0;
    float2 TextureCoordinate : TEXCOORD0;
};
 
struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float3 Normal : TEXCOORD0;
    float2 TextureCoordinate : TEXCOORD1;
};
 
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
 
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
 
    float4 normal = normalize(mul(input.Normal, WorldInverseTranspose));
    float lightIntensity = dot(normal, DiffuseLightDirection);
    output.Color = saturate(DiffuseColor * DiffuseIntensity * lightIntensity);
 
    output.Normal = normal;
 
    output.TextureCoordinate = input.TextureCoordinate;
    return output;
}
 
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    //float3 light = normalize(DiffuseLightDirection);
    //float3 normal = normalize(input.Normal);
    //float3 r = normalize(2 * dot(light, normal) * normal - light);
    //float3 v = normalize(mul(normalize(ViewVector), World));
    //float dotProduct = dot(r, v);
 
    //float4 specular = SpecularIntensity * SpecularColor * max(pow(dotProduct, Shininess), 0) * length(input.Color);
 
    float4 color = tex2D(textureSampler, input.TextureCoordinate);
    color.a = 1;
    
    float value = 0.3 * color.r + 0.5 * color.g + 0.2 * color.b;
    color.r = Saturation * (color.r - value) + value;
    color.g = Saturation * (color.g - value) + value;
    color.b = Saturation * (color.b - value) + value;
    
    return saturate(color * (input.Color) + AmbientColor); //+ specular);
}
 
technique Mood
{
    pass Pass1
    {
		CullMode = CCW;
        ZEnable = TRUE;
        ZWriteEnable = TRUE;
        AlphaBlendEnable = FALSE;

        VertexShader = compile vs_1_1 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

