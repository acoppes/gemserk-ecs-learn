using Unity.Entities;

namespace Gemserk.Ecs.Components
{
    public struct ControlledByPlayer : IComponentData
    {
        public int player;
    }
}