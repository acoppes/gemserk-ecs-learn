using Gemserk.Ecs.Components;
using Unity.Entities;
using UnityEngine;

namespace Gemserk.Ecs.Systems
{
    public class ModelCreateSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<Model>().WithNone<ModelInstance, ToDestroy>().ForEach((Entity e, Model model) =>
            {
                var instance = Object.Instantiate(model.prefab);
                var unitModel = instance.GetComponentInChildren<UnitModel>();

                unitModel.animator.runtimeAnimatorController = model.controller;
                
                PostUpdateCommands.AddComponent(e, new ModelInstance
                {
                    model = unitModel
                });
            });
        }
    }
}