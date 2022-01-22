using Gemserk.Ecs.Components;
using Gemserk.Ecs.Models;
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
                var animator = instance.GetComponentInChildren<Animator>();

                animator.runtimeAnimatorController = model.controller;
                
                PostUpdateCommands.AddComponent(e, new ModelInstance
                {
                    instance = instance,
                    animator = animator
                });
            });
        }
    }

    [UpdateAfter(typeof(ModelCreateSystem))]
//[UpdateBefore(typeof(ModelDestroySystem)), UpdateAfter(typeof(ModelCreateSystem))]
    public class ModelUpdateSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Profiler.BeginSample("Model.Position");
            Entities.ForEach((Entity e, ref Translation t, ModelInstance m) =>
            {
                m.instance.transform.localPosition = t.Value;
                // TODO: looking direction
                
                // var model = EntityManager.GetSharedComponentData<SharedModel>(e).instance;
                // model.SetPosition(t.Value);
                // model.SetLookingDirection(m.lookingDirection);
            });
            Profiler.EndSample();

//             Profiler.BeginSample("Model.AnimationFrame");
//             Entities.WithAllReadOnly<ModelInstance>().ForEach((Entity e, ref Animation animation) =>
//             {
// //            var model = ModelManager.GetModelInstance(e);
//                 var model = EntityManager.GetSharedComponentData<SharedModel>(e).instance;
//                 model.SetAnimationFrame(animation.animationId, animation.currentFrame);
//             });
//             Profiler.EndSample();
        }
    }

    [UpdateAfter(typeof(ModelUpdateSystem)), UpdateBefore(typeof(UnitDestroySystem))]
    public class ModelDestroySystem : ComponentSystem
    {
        public IModelManager ModelManager { get; set; }
    
        protected override void OnUpdate()
        {
            Entities.WithAllReadOnly<ModelInstance, ToDestroy>().ForEach(e => {
                ModelManager.DestroyModel(e);
            });
        }
    }
}