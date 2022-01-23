using Gemserk.Ecs.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Gemserk.Ecs.Systems
{
    public class LookAtSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach((ref LookAt lookAt, ref MovementDestination movement, ref Translation t) => {
                    lookAt.direction = movement.value - t.Value;
                });
        }
    }
}