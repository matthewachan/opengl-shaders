#version 330

in vec2 outTexCoord;
in vec3 mvVertexNormal;
in vec3 mvVertexPos;

out vec4 fragColor;

struct Material
{
	vec3 colour;
	int useColour;
	float reflectance;
};

uniform vec3 ambientLight;
uniform Material material;

void main()
{
	float u = clamp(outTexCoord.s, 0, 1);
	float v = clamp(outTexCoord.t, 0, 1);
	int nRows = 16;

	int row = int(v * float(nRows));
	int col = int(u * float(nRows));

	vec4 black = vec4(0.0, 0.0, 0.0, 1.0);
	vec4 white = vec4(1.0, 1.0, 1.0, 1.0);

	if (row % 2 == 0) {
		if (col % 2 == 0)
			fragColor = black;
		else
			fragColor = white;
	}
	else {
		if (col % 2 == 0)
			fragColor = white;
		else
			fragColor = black;
	}
	vec4 baseColour = vec4(material.colour, 1.0);
	vec4 totalLight = vec4(ambientLight, 1.0);
	fragColor *= totalLight;
}
