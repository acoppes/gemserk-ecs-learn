using Unity.Entities;
using Unity.Mathematics;

namespace Gemserk.Ecs.Components
{
    public struct LookAt : IComponentData
    {
        public float3 direction;
    }
}