#pragma once

#include <glad/glad.h>

#include <iostream>

class Texture {
public:
	unsigned int ID;
	Texture(const char* path);
};