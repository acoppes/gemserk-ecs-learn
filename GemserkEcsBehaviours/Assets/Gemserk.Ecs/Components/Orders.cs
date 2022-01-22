using Unity.Entities;
using Unity.Mathematics;

namespace Gemserk.Ecs.Components
{
    public struct Orders : IComponentData
    {
        // TODO: test enqueue multiple orders
        // TODO: test showing orders queued for selected unit

        public enum Order
        {
            Move,
            Stop,
            Attack
        }

        public Order currentOrder;
        public float3 destination;
        public Target target;
    }
}
