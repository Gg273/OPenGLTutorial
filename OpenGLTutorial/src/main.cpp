#include <iostream>
#include <glad\glad.h>
#include <GLFW\glfw3.h>

#include "Shader.h"

void framebuffer_size_callback(GLFWwindow* window, int width, int height);
void processInput(GLFWwindow* window);

int main()
{
	std::cout << "Hello OPenGL" << std::endl;
	
	glfwInit();

	// glfw����
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
#ifdef	__APPLE__
	glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE)
#endif

	GLFWwindow* window = glfwCreateWindow(800, 600, "OpenGL again", NULL, NULL);
	if (window == NULL)
	{
		std::cout << "�򿪴���ʧ�ܣ�����" << std::endl;
		glfwTerminate();
		return -1;
	}
	// ����glfw������
	glfwMakeContextCurrent(window);
	
	if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress))
	{
		std::cout << "��ʼ��glad��ʧ�ܣ�����" << std::endl;
		glfwTerminate();
		return -1;
	}
	
	glfwSetFramebufferSizeCallback(window, framebuffer_size_callback);
	
	float vertices[] = {
		// һ�����εĶ���
		 0.5f,  0.5f, 0.0f,
		 0.5f, -0.5f, 0.0f,
		-0.5f, -0.5f, 0.0f,
		-0.5f,  0.5f, 0.0f 

	};
	unsigned int indices[] = {
		0, 1, 3,
		1, 2, 3
	};

	// ���� VAO��VBO��IBO
	unsigned int VAO, VBO, IBO;
	
	glGenVertexArrays(1, &VAO);
	glBindVertexArray(VAO);
	
	glGenBuffers(1, &VBO);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

	glGenBuffers(1, &IBO);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, IBO);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);

	glEnableVertexAttribArray(0);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (const void*)0);
	
	Shader ourShader("shaders/shader.vs", "shaders/shader.fs");


	// ��Ⱦѭ��
	while (!glfwWindowShouldClose(window))
	{
		// �����ɫ����
		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT);
		
		// ����
		processInput(window);

		// ����ͼ��
		ourShader.use();
		// 
		float green = sin(glfwGetTime()) / 2.0f + 0.5f;
		ourShader.setVec4("ourColor", 0.0f, green, 0.0f, 1.0f);

		glBindVertexArray(VAO);
		glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);


		// �������壬��鲢�����¼�
		glfwSwapBuffers(window);
		glfwPollEvents();
	}

	glfwTerminate();
	return 0;
}

void framebuffer_size_callback(GLFWwindow* window, int width, int height)
{
	glViewport(0, 0, width, height);
}

void processInput(GLFWwindow* window)
{
	if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
	{
		glfwSetWindowShouldClose(window, GL_TRUE);
	}
}