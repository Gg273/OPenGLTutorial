#version 330 core
out vec4 FragColor; 

// 物体贴图
struct Material {
    sampler2D diffuse;
    sampler2D specular;
    float     shininess;
};
// 光线贴图
struct Light {
    vec3 position; 

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    // 定义点光源的随距离衰减参数 
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
    
    //设置环境光
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));

    //设置漫反射光
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords));
    
    //设置镜面反射光
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm); //反射光线
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * spec * vec3(texture(material.specular, TexCoords));

    //设置衰减值
    float distance = length(light.position - FragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));

    //衰减后的光线
    ambient  *= attenuation;
    diffuse  *= attenuation;
    specular *= attenuation;

    //最终的颜色
    FragColor = vec4((ambient + diffuse + specular), 1.0);
}