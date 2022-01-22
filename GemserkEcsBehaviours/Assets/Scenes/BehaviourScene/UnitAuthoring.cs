using Gemserk.Ecs.Components;
using Unity.Entities;
using UnityEngine;
using Animation = Gemserk.Ecs.Components.Animation;

public class UnitAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField]
    private EntityBehaviour behaviour;
    public float speed;
    
    public int modelId;
    public int bodyAttachPoint;
    public int headAttachPoint;
    
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
            modelId = modelId,
            bodyAttachPoint = bodyAttachPoint,
            headAttachPoint = headAttachPoint
        });
        
        dstManager.AddComponentData(entity, new Animation
        {
            fps = 30,
            state = Animation.State.Stopped
        });

        dstManager.AddComponentData(entity, new Unit
        {
            state = Unit.State.Idle,
            deathAnimationId = 0,
            destroyLogic = Unit.DestroyLogic.DestroyOnDeath
        });
        
        dstManager.AddComponentData(entity, new LookAt());
        dstManager.AddComponentData(entity, new AttachPoints());
        // dstManager.AddComponentData(entity, new Alive());
    }
}