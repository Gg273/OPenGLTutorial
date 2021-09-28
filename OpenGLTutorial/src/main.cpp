#include <iostream>
#include <glad\glad.h>
#include <GLFW\glfw3.h>

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
	
	// ��Ⱦѭ��
	while (!glfwWindowShouldClose(window))
	{
		// �����ɫ����
		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT);
		
		// ����
		processInput(window);




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