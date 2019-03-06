#version 330
#extension GL_OES_standard_derivatives : enable

in vec2 outTexCoord;
in vec3 mvVertexNormal;
in vec3 mvVertexPos;
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
float edgeFactor(){
    vec3 d = fwidth(vBC);
    vec3 a3 = smoothstep(vec3(0.0), d*1.5, vBC);
    return min(min(a3.x, a3.y), a3.z);
}

void main() {
	/* if(any(lessThan(vBC, vec3(0.02)))){ */
	/* 	fragColor = vec4(0.0, 0.0, 0.0, 1.0); */
	/* } */
	/* else{ */
	/* 	fragColor = vec4(0.5, 0.5, 0.5, 1.0); */
	/* } */
	vec4 baseColour = vec4(material.colour, 1.0);
	vec4 totalLight = vec4(ambientLight, 1.0);
	/* fragColor = vec4(0.0, 0.0, 0.0, (1.0-edgeFactor())*0.95); */
	/* fragColor.rgb = mix(vec3(0.0), vec3(0.5), edgeFactor()); */
	/* fragColor.a = 1.0; */
	/* fragColor *= totalLight; */

	vec4 redTint = vec4(0.5, 0.0, 0.0, 1.0);
	fragColor = (baseColour + redTint) * totalLight;
	fragColor.a = (1.0 - edgeFactor()) * 0.95;
}
