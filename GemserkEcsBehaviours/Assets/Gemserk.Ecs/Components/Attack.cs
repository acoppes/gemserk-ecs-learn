using Unity.Entities;

namespace Gemserk.Ecs.Components
{
    public struct Attack : IComponentData
    {
        public bool attackPressed;
        public bool attacking;
    }
}

