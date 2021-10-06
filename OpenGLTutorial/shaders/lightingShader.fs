#version 330 core
struct Material {
    sampler2D diffuse;
    sampler2D specular;
    float shininess;
};
struct Light {
    vec3 position;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform Light light;
uniform Material material;

uniform vec3 viewPos;

in vec3 Normal;
in vec3 FragPos;
in vec2 TexCoords;

out vec4 FragColor;  

void main()
{
    //设置环境光
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));

    //设置漫反射光
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords));
    
    //设置镜面反射
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm); //反射光线
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * spec * vec3(texture(material.specular, TexCoords));

    //最终的颜色
    FragColor = vec4(ambient + diffuse + specular, 1.0);
}