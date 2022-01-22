using Unity.Entities;

namespace Gemserk.Ecs.Components
{
    public struct Health : IComponentData
    {
        public float total;
        public float current;
    }
}
