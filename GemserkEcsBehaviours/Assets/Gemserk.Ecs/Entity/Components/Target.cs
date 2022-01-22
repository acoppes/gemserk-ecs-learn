using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct Target
{
    public Entity entity;
    public float3 position;
}

public struct AttachPoints : IComponentData
{
    public float3 bodyPosition;
    public float3 headPosition;
    public float3 attackPosition;
}