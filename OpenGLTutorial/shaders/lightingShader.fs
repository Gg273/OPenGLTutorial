#version 330 core
out vec4 FragColor; 

// ������ͼ
struct Material {
    sampler2D diffuse;
    sampler2D specular;
    float     shininess;
};
// ������ͼ
struct Light {
    vec3 position;
    vec3 direction;  // ����һ���ֵ�ͲΪ��Դ���������Ϊ�ֵ�Ͳָ��ķ���
    float cutOff;    // Ϊ�й��

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    // ������Դ�������˥������ 
    float constant;
    float linear;
    float quadratic;
};

uniform Light light;
uniform Material material;
uniform vec3 viewPos;

in vec3 Normal;
in vec3 FragPos;
in vec2 TexCoords;

void main()
{
    
    // ���û�����
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));

    // �����������
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords));
    
    // ���þ��淴���
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm); // �������
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * spec * vec3(texture(material.specular, TexCoords));

    float theta = dot(lightDir, normalize(-light.direction));

    if(theta > light.cutOff) 
    {       
        // ִ�й��ռ���
        // ����˥��ֵ
        float distance = length(light.position - FragPos);
        float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));

        // ˥����Ĺ���
        ambient  *= attenuation;
        diffuse  *= attenuation;
        specular *= attenuation;

        // ���յ���ɫ
        FragColor = vec4((ambient + diffuse + specular), 1.0);
    }
    else  // ����ʹ�û����⣬�ó����ھ۹�֮��ʱ��������ȫ�ڰ�
        FragColor = vec4(light.ambient * vec3(texture(material.diffuse, TexCoords)), 1.0);

    
}