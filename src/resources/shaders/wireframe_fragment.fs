#version 330

in vec3 vBC;

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
	vec4 baseColour = vec4(material.colour, 1.0);
	
	vec4 black = vec4(0, 0, 0, 0);
	vec4 white = vec4(1, 1, 1, 1);

	if(any(lessThan(vBC, vec3(0.02)))) {
		fragColor = white;
	}
	else {
		fragColor = black;
	}

	vec4 totalLight = vec4(ambientLight, 1.0);
}
