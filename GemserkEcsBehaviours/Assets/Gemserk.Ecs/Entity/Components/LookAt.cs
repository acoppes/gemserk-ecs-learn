using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct LookAt : IComponentData
{
    public float3 direction;
}