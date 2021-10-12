#version 330 core
out vec4 FragColor;

// ƽ�й��߹�Դ��ǿ�Ȳ���˥����ģ������̫���ķ�����
struct DirLight {
    vec3 direction;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};
// �۹⣬�������ֵ�Ͳ����Ĺ��ߣ�ֻ����һ����Χ�е�����������䵽��
struct SpotLight {
    vec3 position;
    vec3 direction;

    // �������䵽�ķ�Χ
    float cutOff;
    float outerCutOff;

    // ˥������
    float constant;
    float linear;
    float quadratic;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};


// ������ͼ
uniform sampler2D texture_diffuse1;
uniform sampler2D texture_specular1;

// ��Դ
uniform SpotLight spotLight;
// �۲�λ��
uniform vec3 viewPos;

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;

vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir);
vec3 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir);

void main()
{
    // ����
    vec3 norm = normalize(Normal);
    vec3 viewDir = normalize(viewPos - FragPos);

    // ��һ�׶Σ��������
    vec3 result = CalcSpotLight(spotLight, norm, FragPos, viewDir);    

    FragColor = vec4(result, 1.0);
}

vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir)
{
    // �����嵽��Դ�ķ�������
    vec3 lightDir = normalize(-light.direction);
    // ��������ɫ����
    float diff = max(dot(normal, lightDir), 0.0);
    // �������ɫ����
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32.0);
    // �ϲ����
    vec3 ambient  = light.ambient  * vec3(texture(texture_diffuse1, TexCoords));
    vec3 diffuse  = light.diffuse  * diff * vec3(texture(texture_diffuse1, TexCoords));
    vec3 specular = light.specular * spec * vec3(texture(texture_specular1, TexCoords));
    
    return (ambient + diffuse + specular);
}

// ����۹��Ӱ��
vec3 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
{
    vec3 lightDir = normalize(light.position - fragPos);

    // ��������ɫ
    float diff = max(dot(normal, lightDir), 0.0);
    // �������ɫ
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 64.0);
    
    // ˥��
    float distance    = length(light.position - fragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance + 
                 light.quadratic * (distance * distance));    
   
    // ���þ۹�����
    float theta     = dot(lightDir, normalize(-light.direction));
    float epsilon   = light.cutOff - light.outerCutOff;
    float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0);
    

    vec3 ambient  = light.ambient  * vec3(texture(texture_diffuse1, TexCoords));
    vec3 diffuse  = light.diffuse  * diff * vec3(texture(texture_diffuse1, TexCoords));
    vec3 specular = light.specular * spec * vec3(texture(texture_specular1, TexCoords));
    
    ambient  = ambient  * attenuation * intensity;
    diffuse  = diffuse  * attenuation * intensity;
    specular = specular * attenuation * intensity;

    return (ambient + diffuse + specular);
}