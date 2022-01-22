using Unity.Entities;
using UnityEngine;

public abstract class EntityTemplateBase : ScriptableObject, IEntityTemplate
{
    // maybe use injector instead?

    private EntityManager? _entityManager;

    private EntityCommandBuffer _entityCommandBuffer;
    
    protected IEntityTemplateParameters _parameters;

    public EntityManager? EntityManager { set => _entityManager = value; }

    public EntityCommandBuffer EntityCommandBuffer
    {
        set => _entityCommandBuffer = value;
    }

    public IEntityTemplateParameters Parameters { set => _parameters = value; }

    public abstract void Apply(Entity e);

    protected void AddComponent<T>(Entity e, T t) where T : struct, IComponentData
    {
        if (_entityManager.HasValue)
            _entityManager.Value.AddComponentData(e, t);
        else 
            _entityCommandBuffer.AddComponent(e, t);
    }
}
