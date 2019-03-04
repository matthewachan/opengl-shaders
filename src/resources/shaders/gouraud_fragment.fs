#version 330

struct Material
{
    vec3 colour;
    int useColour;
    float reflectance;
};

in vec4 color; 

out vec4 fragColor;

uniform Material material;

void main()
{
	fragColor = color * vec4(material.colour, 1.0);
}
