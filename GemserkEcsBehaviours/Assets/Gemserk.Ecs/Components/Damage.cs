using System;
using Unity.Entities;

namespace Gemserk.Ecs.Components
{
    [Serializable]
    public struct Damage : IComponentData
    {
        // public Entity source;
        public Entity target;
        public float value;

        public int particleId;
        public int attachPointId;

        // public fixed float2[] attachPoints[10];

        // TODO: support for area damage?
    }
}