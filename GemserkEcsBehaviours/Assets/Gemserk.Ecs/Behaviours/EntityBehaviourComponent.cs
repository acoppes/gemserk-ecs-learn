using System;
using System.Collections.Generic;
using Unity.Entities;

public interface EntityBehaviourContext
{
    // getBlackboard?
    
    // get local context

    T Get<T>(object o) where T : class, new();
    
    // object Get(string key);
}

public class EntityBehaviourContextDict : EntityBehaviourContext
{
    private readonly Dictionary<Type, object> localContext = new ();
    private readonly Dictionary<string, object> keyContext = new ();

    public T Get<T>(object o) where T : class, new()
    {
        var type = o.GetType();
        if (!localContext.ContainsKey(type))
        {
            localContext[type] = new T();
        }
        return localContext[type] as T;
    }

    // public object Get(string key)
    // {        
    //     if (!keyContext.ContainsKey(key))
    //     {
    //         return null;
    //     }
    //     return keyContext[key];
    // }
}

public class EntityBehaviourComponent : IComponentData
{
    public EntityBehaviour behaviour;
    public readonly EntityBehaviourContext context = new EntityBehaviourContextDict();

    // public bool contextInitialized;
    // public bool destroyPending;

    // TODO: clear context to lose references on destroy?
}