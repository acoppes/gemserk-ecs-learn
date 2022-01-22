using Gemserk.Ecs.Components;
using Unity.Entities;
using UnityEngine;

namespace Gemserk.Ecs.Systems
{
    public class AnimationSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities
                .WithAllReadOnly<Movement>()
                .WithAll<ModelInstance>().ForEach((ModelInstance model, ref Movement movement) => 
                {
                    var moving = Vector3.SqrMagnitude(movement.velocity) > 0f;
                    model.model.animator.SetBool("walking", moving);
                });
        }
    }
}