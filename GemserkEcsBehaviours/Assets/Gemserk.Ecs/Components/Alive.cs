using Unity.Entities;

namespace Gemserk.Ecs.Components
{
    public struct Alive : IComponentData
    {
        // just because foreach fails otherwise
        // public int garbage;
    }
}