using Gemserk.Ecs.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gemserk.Ecs.Systems
{
    [UpdateBefore(typeof(MovementSystem))]
    public class ControlledByPlayerSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAllReadOnly<ControlledByPlayer>()
                .ForEach((ref Movement m, ControlledByPlayer c) =>
                {

                    if (c.playerInputInstance == null)
                    {
                        InputDevice device = Keyboard.current;

                        if (c.player == 1)
                        {
                            device = Gamepad.current;
                        }
                        
                        c.playerInputInstance = PlayerInput.Instantiate(c.playerInputPrefab, c.player);
                        c.playerInputInstance.SwitchCurrentActionMap($"Player_{c.player}");
                        c.playerInputInstance.SwitchCurrentControlScheme("Default", device);

                        // c.playerInputInstance.ActivateInput();

                    }
                    
                    var movingDirection = new float3();

                    var move = c.playerInputInstance.actions["Move"];
                    
                    var v = move.ReadValue<Vector2>();
                    movingDirection = new float3(v.x, v.y, 0);

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