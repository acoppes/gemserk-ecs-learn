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
            Entities.WithAllReadOnly<ControlledByPlayer>()
                .ForEach((ref Movement m, ref ControlledByPlayer c) =>
                {
                    var movingDirection = new float3();
                    
                    if (c.player == 0)
                    {
                        if (Keyboard.current != null)
                        {
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
                        }
                    } else if (c.player == 1)
                    {
                        if (Gamepad.current != null)
                        {
                            var v = Gamepad.current.leftStick.ReadValue();
                            movingDirection = new float3(v.x, v.y, 0);
                        }
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