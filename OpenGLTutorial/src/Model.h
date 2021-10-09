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
	void processNode(aiNode* node, const aiScene* scene); // 加载场景中所有的物体
	Mesh processMesh(aiMesh* mesh, const aiScene* scene); // 加载物体中的顶点（包括顶点、法向量···）
	std::vector<Texture> loadMaterialTextures(aiMaterial* mat, aiTextureType type, std::string typeName);
};

