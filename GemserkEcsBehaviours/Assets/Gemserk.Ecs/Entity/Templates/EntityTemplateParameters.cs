using System.Collections.Generic;

public class EntityTemplateParameters : IEntityTemplateParameters
{
    private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>();
    
    public int GetInt(string name, int defaultValue)
    {
        if (!_parameters.ContainsKey(name))
            return defaultValue;
        return (int) _parameters[name];
    }

    public float GetFloat(string name, float defaultValue)
    {
        if (!_parameters.ContainsKey(name))
            return defaultValue;
        return (float)_parameters[name];
    }

    public int GetInt(string name)
    {
        return (int) _parameters[name];
    }

    public T Get<T>(string name)
    {
        return (T) _parameters[name];
    }

    public void SetObject(string name, object o)
    {
        _parameters[name] = o;
    }

    public void SetInt(string name, int i)
    {
        _parameters[name] = i;
    }

    public void SetFloat(string name, float f)
    {
        _parameters[name] = f;
    }
}
