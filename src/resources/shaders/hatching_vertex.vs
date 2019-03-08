#version 330

layout (location=0) in vec3 position;
layout (location=1) in vec2 texCoord;
layout (location=2) in vec3 vertexNormal;

out vec2 outTexCoord;
out vec3 mvVertexNormal;
out vec3 mvVertexPos;
out float intensity;

uniform mat4 modelViewMatrix;
uniform mat4 projectionMatrix;

struct Attenuation
{
	float constant;
	float linear;
	float exponent;
};

struct PointLight
{
	vec3 colour;
	// Light position is assumed to be in view coordinates
	vec3 position;
	float intensity;
	Attenuation att;
};

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

uniform vec3 ambientLight;
uniform Material material;
uniform PointLight pointLight;
uniform DirectionalLight directionalLight;

vec4 calcLightColour(vec3 light_colour, float light_intensity, vec3 position, vec3 to_light_dir, vec3 normal)
{
	vec4 diffuseColour = vec4(0, 0, 0, 0);
	vec4 specColour = vec4(0, 0, 0, 0);

	// Diffuse Light
	float diffuseFactor = max(dot(normal, to_light_dir), 0.0);
	diffuseColour = vec4(light_colour, 1.0) * light_intensity * diffuseFactor;
	return diffuseColour;
}

vec4 calcPointLight(PointLight light, vec3 position, vec3 normal)
{
	vec3 light_direction = light.position - position;
	vec3 to_light_dir  = normalize(light_direction);
	vec4 light_colour = calcLightColour(light.colour, light.intensity, position, to_light_dir, normal);

	// Apply Attenuation
	float distance = length(light_direction);
	float attenuationInv = light.att.constant + light.att.linear * distance +
		light.att.exponent * distance * distance;
	return light_colour / attenuationInv;
}

vec4 calcDirectionalLight(DirectionalLight light, vec3 position, vec3 normal)
{
	return calcLightColour(light.colour, light.intensity, position, normalize(light.direction), normal);
}

void main()
{
	vec4 mvPos = modelViewMatrix * vec4(position, 1.0);
	gl_Position = projectionMatrix * mvPos;

	outTexCoord = texCoord;
	mvVertexNormal = normalize(modelViewMatrix * vec4(vertexNormal, 0.0)).xyz;
	mvVertexPos = mvPos.xyz;

	vec4 totalLight = calcDirectionalLight(directionalLight, mvVertexPos, mvVertexNormal);
	totalLight += calcPointLight(pointLight, mvVertexPos, mvVertexNormal); 

	intensity = dot(normalize(directionalLight.direction), mvVertexNormal);
	vec3 pointDir = normalize(pointLight.position - mvVertexPos);
	intensity += dot(pointDir, mvVertexNormal);
	intensity = max(intensity, 0);

}
