
public interface IEntityTemplateParameters
{
    int GetInt(string name, int defaultValue);

    float GetFloat(string name, float defaultValue);

    int GetInt(string name);

    T Get<T>(string name);
}
