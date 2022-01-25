using Unity.Entities;
using Unity.Mathematics;

namespace Gemserk.Ecs.Components
{
    public struct Movement : IComponentData
    {
        public float3 velocity;
        public float speed;
        public float3 velocityDifference;

        public bool disable;
    }

    public struct MovementDestination : IComponentData
    {
        public float3 value;
        public float distanceSq;
    }
}