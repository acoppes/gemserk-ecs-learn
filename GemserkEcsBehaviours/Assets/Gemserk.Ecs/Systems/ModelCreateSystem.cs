using Gemserk.Ecs.Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Profiling;

namespace Gemserk.Ecs.Systems
{
    public class ModelCreateSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<Model>().WithNone<ModelInstance, ToDestroy>().ForEach((Entity e, Model model) =>
            {
                var instance = GameObject.Instantiate(model.prefab);
                var unitModel = instance.GetComponentInChildren<UnitModel>();

                unitModel.animator.runtimeAnimatorController = model.controller;
                
                PostUpdateCommands.AddComponent(e, new ModelInstance
                {
                    model = unitModel
                });
            });
        }
    }

    [UpdateAfter(typeof(ModelCreateSystem))]
    public class ModelUpdateSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Profiler.BeginSample("Model.Position");
            Entities.ForEach((Entity e, ref Translation t, ModelInstance m) =>
            {
                m.model.transform.localPosition = t.Value;
            });
            Profiler.EndSample();
        }
    }
}