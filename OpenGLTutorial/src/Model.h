#pragma once

#include <iostream>
#include <vector>
#include <string>

#include <assimp/Importer.hpp>
#include <assimp/scene.h>
#include <assimp/postprocess.h>


#include "Mesh.h"

class Model
{
public:
	Model(std::string path);
	~Model();

	std::vector<Texture> textures_loaded;
	std::vector<Mesh> meshes;
	std::string directory;
	void Draw(Shader& shader);

private:
	void loadModel(std::string path);
	void processNode(aiNode* node, const aiScene* scene); // ���س��������е�����
	Mesh processMesh(aiMesh* mesh, const aiScene* scene); // ���������еĶ��㣨�������㡢��������������
	std::vector<Texture> loadMaterialTextures(aiMaterial* mat, aiTextureType type, std::string typeName);
};

