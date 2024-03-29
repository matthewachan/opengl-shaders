#version 330

in vec2 outTexCoord;
in vec3 mvVertexNormal;
in vec3 mvVertexPos;
in float intensity;

out vec4 fragColor;

uniform vec3 ambientLight;
struct Material
{
	vec3 colour;
	int useColour;
	float reflectance;
};
uniform Material material;

void main()
{
	float u = clamp(outTexCoord.s, 0, 1);
	float v = clamp(outTexCoord.t, 0, 1);
	int nRows = 64;

	int row = int(v * float(nRows));
	int col = int(u * float(nRows));

	vec4 black = vec4(0.0, 0.0, 0.0, 1.0);
	vec4 white = vec4(1.0, 1.0, 1.0, 1.0);

	float dp = length(vec2(dFdx(u), dFdy(u)));
	float logdp = -log2(dp);
	float ilogdp = floor(logdp);
	float transition = logdp - ilogdp;
	float freq = exp2(ilogdp);

	float sawtooth = fract(u * freq * freq);
	float triangle = abs(2. * sawtooth - 1.);
	float square = step(0.5, triangle);

	fragColor = vec4(vec3(square), 1.0) * intensity;

	vec4 baseColour = vec4(material.colour, 1.0);
	vec4 ambientLight = vec4(ambientLight, 1.0);
}
