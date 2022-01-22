using Gemserk.Ecs.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Gemserk.Ecs.Systems
{
    [UpdateAfter(typeof(ModelCreateSystem))]
    public class UnitModelSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAllReadOnly<Translation>().ForEach((Unity.Entities.Entity e, ref LookAt lookAt, ref ModelInstance m) => {
                m.lookingDirection = lookAt.direction;
            });
        }
    }
}
