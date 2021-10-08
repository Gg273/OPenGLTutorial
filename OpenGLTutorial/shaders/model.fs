#version 330 core
out vec4 FragColor;

uniform sampler2D texture_diffuse1;
uniform sampler2D texture_diffuse2;
uniform sampler2D texture_diffuse3;
uniform sampler2D texture_specular1;
uniform sampler2D texture_specular2;

in vec2 TexCoords;

void main()
{

    FragColor = vec4(vec3(texture(texture_diffuse1, TexCoords)), 1.0);
}