#pragma once

#include <glad/glad.h>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include <string>
#include <fstream>
#include <sstream>
#include <iostream>

class Shader 
{
public:
	unsigned int ID;

	Shader(const GLchar* vertexPath, const GLchar* fragmentPath);

	void use();

	void setBool(const std::string& name, bool value) const;
	void setInt(const std::string& name, int value) const;
	void setFloat(const std::string& name, float value) const;
	void setVec3(const std::string& name, float v0, float v1, float v2) const;
	void setVec3(const std::string& name, glm::vec3 vec) const;
	void setVec4(const std::string& name, float v0, float v1, float v2, float v3) const;
	void setVec4(const std::string& name, glm::vec4 vec) const;
	void setMat4(const std::string& name, glm::mat4 matrix) const;
};