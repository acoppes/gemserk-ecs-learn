using Gemserk.Ecs.Components;
using Unity.Entities;

namespace Gemserk.Ecs.Systems
{
    [UpdateBefore(typeof(MovementSystem))]
    public class DisableMovementWhileAttackingSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities
                .WithAllReadOnly<Attack>()
                .ForEach((ref Movement m, ref Attack attack) =>
                {
                    m.disable = attack.attacking;
                });
        }
    }
}