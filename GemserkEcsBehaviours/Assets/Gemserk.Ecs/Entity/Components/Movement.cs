using Unity.Entities;
using Unity.Mathematics;

public struct Movement : IComponentData
{
    public float3 velocity;
    public float speed;
    public float3 velocityDifference;
}

public struct MovementDestination : IComponentData
{
    public float3 value;
    public float distanceSq;
}

public class MovementDesignConversionSystem : GameObjectConversionSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((MovementDesign m) =>
        {
            var entity = GetPrimaryEntity(m);
            DstEntityManager.AddComponentData(entity, new Movement
            {
                speed = m.speed
            });
        });
    }
}