#version 330

in vec2 outTexCoord;
in vec3 mvVertexNormal;
in vec3 mvVertexPos;

out vec4 fragColor;

/* struct Attenuation */
/* { */
/* 	float constant; */
/* 	float linear; */
/* 	float exponent; */
/* }; */

/* struct PointLight */
/* { */
/* 	vec3 colour; */
/* 	// Light position is assumed to be in view coordinates */
/* 	vec3 position; */
/* 	float intensity; */
/* 	Attenuation att; */
/* }; */

struct DirectionalLight
{
	vec3 colour;
	vec3 direction;
	float intensity;
};

struct Material
{
	vec3 colour;
	int useColour;
	float reflectance;
};

/* uniform sampler2D texture_sampler; */
/* uniform vec3 ambientLight; */
/* uniform float specularPower; */
uniform Material material;
/* uniform PointLight pointLight; */
uniform DirectionalLight directionalLight;

void main() {
	
	vec4 baseColour = vec4(material.colour, 1);
	vec4 color;

	float intensity = dot(normalize(directionalLight.direction), normalize(mvVertexNormal));

	if (intensity > 0.95)
		color = vec4(1.0,0.5,0.5,1.0);
	else if (intensity > 0.5)
		color = vec4(0.6,0.3,0.3,1.0);
	else if (intensity > 0.25)
		color = vec4(0.4,0.2,0.2,1.0);
	else
		color = vec4(0.2,0.1,0.1,1.0);

	fragColor = color;

}
