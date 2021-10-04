#version 330 core
layout (location = 0) in vec3 aPos;   // λ�ñ���������λ��ֵΪ 0 

uniform mat4 m_MVP;

void main()
{
    gl_Position = m_MVP * vec4(aPos, 1.0);
}