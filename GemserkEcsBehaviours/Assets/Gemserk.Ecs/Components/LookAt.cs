using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Gemserk.Ecs.Components
{
    [Serializable]
    public struct LookAt : IComponentData
    {
        public float3 direction;
    }
}