using Gemserk.Ecs.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.InputSystem;

namespace Gemserk.Ecs.Systems
{
    [UpdateBefore(typeof(MovementSystem))]
    public class ControlledByPlayerSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<ControlledByPlayer>()
                .ForEach((ref Movement m) =>
                {
                    var movingDirection = new float3();

                    if (Keyboard.current.wKey.isPressed)
                    {
                        movingDirection.y += 1.0f;
                    }
                    
                    if (Keyboard.current.sKey.isPressed)
                    {
                        movingDirection.y -= 1.0f;
                    }
                    
                    if (Keyboard.current.aKey.isPressed)
                    {
                        movingDirection.x -= 1.0f;
                    }
                    
                    if (Keyboard.current.dKey.isPressed)
                    {
                        movingDirection.x += 1.0f;
                    }
                    
                    m.velocityDifference = math.normalizesafe(movingDirection) * Time.DeltaTime;
                });
            
            Entities
                .WithAll<ControlledByPlayer>()
                .WithNone<MovementDestination>()
                .ForEach((ref LookAt lookAt, ref Movement m) =>
                {
                    if (math.abs(m.velocityDifference.x) > 0)
                        lookAt.direction.x = m.velocityDifference.x;
                    
                    if (math.abs(m.velocityDifference.y) > 0)
                        lookAt.direction.y = m.velocityDifference.y;
                });
        }
    }
}