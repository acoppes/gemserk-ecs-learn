using System;
using Gemserk.Ecs.Models;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Gemserk.Ecs.Components
{
    // model creation!
    public class Model : IComponentData
    {
        public GameObject prefab;
        public RuntimeAnimatorController controller;
    }

    public class ModelInstance : IComponentData
    {
        public GameObject instance;
        public Animator animator;
        
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
}