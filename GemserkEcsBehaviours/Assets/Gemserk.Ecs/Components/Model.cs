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
        public UnitModel model;
    }
}