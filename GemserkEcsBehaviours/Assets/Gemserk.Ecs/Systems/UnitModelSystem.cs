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
            Entities.WithAllReadOnly<Translation>().ForEach((ref LookAt lookAt, ModelInstance m) =>
            {
                m.model.spriteRenderer.flipX = lookAt.direction.x < 0;
            });
        }
    }
}
