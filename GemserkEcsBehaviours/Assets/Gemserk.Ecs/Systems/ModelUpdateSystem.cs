using Gemserk.Ecs.Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine.Profiling;

namespace Gemserk.Ecs.Systems
{
    [UpdateAfter(typeof(ModelCreateSystem))]
    public class ModelUpdateSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Profiler.BeginSample("Model.Position");
            Entities.ForEach((Entity e, ref Translation t, ModelInstance m) =>
            {
                var v = t.Value;
                v.y *= 0.75f;
                m.model.transform.localPosition = v;
            });
            Profiler.EndSample();
        }
    }
}