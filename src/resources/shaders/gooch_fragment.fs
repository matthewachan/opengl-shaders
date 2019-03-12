#version 330

in vec2 outTexCoord;
in vec3 mvVertexNormal;
in vec3 mvVertexPos;

out vec4 fragColor;

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
uniform vec3 ambientLight;
uniform float specularPower;
uniform Material material;
uniform DirectionalLight directionalLight;

vec4 calcLightColour(vec3 light_colour, float light_intensity, vec3 position, vec3 to_light_dir, vec3 normal)
{
	vec4 specColour = vec4(0, 0, 0, 0);
	
	// Specular Light
	vec3 camera_direction = normalize(-position);
	vec3 from_light_dir = -to_light_dir;
	vec3 reflected_light = normalize(reflect(from_light_dir , normal));
	float specularFactor = max( dot(camera_direction, reflected_light), 0.0);
	specularFactor = pow(specularFactor, specularPower);
	specColour = light_intensity  * specularFactor * material.reflectance * vec4(light_colour, 1.0);

	return specColour;
}


vec4 calcDirectionalLight(DirectionalLight light, vec3 position, vec3 normal)
{
	return calcLightColour(light.colour, light.intensity, position, normalize(light.direction), normal);
}

void main()
{
	vec4 baseColour = vec4(material.colour, 1);

	// Constant diffuse terms
	float coolDiffuse = 0.2;
	float warmDiffuse = 0.2;

	vec4 warmColor = vec4(0.9, 0.5, 0.1, 1);
	vec4 coolColor = vec4(0, 0.3, 0.9, 1);

	vec3 to_light_dir = normalize(directionalLight.direction);

	// Add in surface diffuse colors 
	coolColor = min(coolColor.rgba + coolDiffuse * baseColour.rgba, 1);
	warmColor = min(warmColor.rgba + coolDiffuse * baseColour.rgba, 1);

	// Linearly interpolate colors
	vec4 color = mix(coolColor, warmColor, dot(to_light_dir, mvVertexNormal));

	// Compute specular illumination and diffuse illumination
	vec4 totalLight = vec4(ambientLight, 1.0);
	totalLight += calcDirectionalLight(directionalLight, mvVertexPos, mvVertexNormal);

	// Add specular lighting to the color
	fragColor = color * totalLight;
}
