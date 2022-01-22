using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

// model creation!
public struct Model : IComponentData
{
    public int modelId;
    public int bodyAttachPoint;
    public int headAttachPoint;
    public int attackAttachPoint;
}

public struct ModelInstance : IComponentData
{
    public float3 lookingDirection;
}

public struct SharedModel : ISharedComponentData, IEquatable<SharedModel>
{
    public IUnitModel instance;

    public bool Equals(SharedModel other)
    {
        return Equals(instance, other.instance);
    }

    public override bool Equals(object obj)
    {
        return obj is SharedModel other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (instance != null ? instance.GetHashCode() : 0);
    }
}

