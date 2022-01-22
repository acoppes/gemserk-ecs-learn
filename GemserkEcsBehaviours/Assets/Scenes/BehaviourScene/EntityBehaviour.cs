using Unity.Entities;
using UnityEngine;

public abstract class EntityBehaviour : MonoBehaviour
{
    // public abstract void OnCreateContext(EntityBehaviourContext context);
    
    // public abstract void OnDestroyContext(EntityBehaviourContext context);
    public Entity entity;
    public EntityManager entityManager;
    public EntityBehaviourContext context;
    
    public abstract void OnEntityUpdated(float dt);

    protected T GetContext<T>() where T : class, new()
    {
        return context.Get<T>(this);
    }

    protected T GetComponentData<T>() where T : struct, IComponentData
    {
        return entityManager.GetComponentData<T>(entity);
    }

    protected bool HasComponent<T>()
    {
        return entityManager.HasComponent<T>(entity);
    }

    protected void SetComponentData<T>(T t) where T : struct, IComponentData
    {
        entityManager.SetComponentData(entity, t);
    }

    protected void AddComponentData<T>(T t) where T : struct, IComponentData
    {
        entityManager.AddComponentData(entity, t);
    }
}