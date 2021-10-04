#version 330 core
layout (location = 0) in vec3 aPos;   // 位置变量的属性位置值为 0 

uniform mat4 m_MVP;

void main()
{
    gl_Position = m_MVP * vec4(aPos, 1.0);
}