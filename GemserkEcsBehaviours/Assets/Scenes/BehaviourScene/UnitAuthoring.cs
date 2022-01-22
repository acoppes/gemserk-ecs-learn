using Gemserk.Ecs.Components;
using Unity.Entities;
using UnityEngine;
using Animation = Gemserk.Ecs.Components.Animation;

public class UnitAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField]
    private EntityBehaviour behaviour;

    [SerializeField]
    private GameObject modelPrefab;

    [SerializeField]
    private RuntimeAnimatorController modelController;
    
    public float speed;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new EntityBehaviourComponent
        {
            behaviour = behaviour
        });
        dstManager.AddComponentData(entity, new Movement
        {
            speed = speed
        });
        dstManager.AddComponentData(entity, new Model
        {
            prefab = modelPrefab,
            controller = modelController
        });
        dstManager.AddComponentData(entity, new LookAt());
    }
}