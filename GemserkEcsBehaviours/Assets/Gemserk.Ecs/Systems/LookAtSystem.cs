using Gemserk.Ecs.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Gemserk.Ecs.Systems
{
    public class LookAtSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            // TODO: test two different for eachs, using AttackTarget set or MovementDEstination set.
            Entities.WithAll<Alive>().WithNone<MovementDestination>()
                .ForEach((ref LookAt lookAt, ref Attack attack, ref Translation t) => {
                
                    if (EntityManager.Exists(attack.target.entity))
                    {
                        var targetPosition = EntityManager.GetComponentData<Translation>(attack.target.entity);
                        lookAt.direction = targetPosition.Value - t.Value;
                    }
                });
        
            Entities
                // .WithAll<Alive>()
                .ForEach((ref LookAt lookAt, ref MovementDestination movement, ref Translation t) => {
                    lookAt.direction = movement.value - t.Value;
                });
        }
    }
}