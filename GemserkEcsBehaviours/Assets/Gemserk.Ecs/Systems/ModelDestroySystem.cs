using Gemserk.Ecs.Components;
using Unity.Entities;
using UnityEngine;

namespace Gemserk.Ecs.Systems
{
    [UpdateAfter(typeof(ModelUpdateSystem)), UpdateBefore(typeof(UnitDestroySystem))]
    public class ModelDestroySystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAllReadOnly<ModelInstance, ToDestroy>().ForEach((ModelInstance m) => {
                GameObject.Destroy(m.model.gameObject);
            });
        }
    }
}