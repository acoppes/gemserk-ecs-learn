using Gemserk.Ecs.Components;
using Unity.Entities;
using Unity.Mathematics;

namespace Gemserk.Ecs.Systems
{
    [UpdateBefore(typeof(UnitSystem)), UpdateAfter(typeof(AnimationSystem)), UpdateBefore(typeof(AnimationEventsSystem))]
    public class UnitDeathFrameSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((Entity e, ref AnimationCompletedEvent cEvent) => {
                // post in another system?

                if (!EntityManager.HasComponent<Unit>(cEvent.entity))
                    return;

                var unit = EntityManager.GetComponentData<Unit>(cEvent.entity);

                if (cEvent.animationId == unit.deathAnimationId)
                {
                    unit.state = Unit.State.Dead;
                    if (unit.destroyLogic == Unit.DestroyLogic.DestroyOnDeath)
                        PostUpdateCommands.AddComponent(cEvent.entity, new ToDestroy());
                    PostUpdateCommands.RemoveComponent<Unit>(cEvent.entity);
                }
            });
        }
    }

    [UpdateAfter(typeof(ModelCreateSystem)), UpdateBefore(typeof(ModelDestroySystem)), UpdateBefore(typeof(UnitDestroySystem))]
    public class UnitSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAllReadOnly<Alive, ModelInstance>().ForEach((Entity e, ref Attack attack, ref Health health, ref Unit state, ref Movement movement) => {

                if (health.current <= 0)
                {
                    state.state = Unit.State.Dying;
      
                    PostUpdateCommands.AddComponent(e, new AnimationCommand()
                    {
                        animationId = 3,
                        command = AnimationCommand.Command.Play,
                        loopCount = 1
                    });

                    PostUpdateCommands.RemoveComponent<Alive>(e);
                }
                else if (attack.target.entity != Entity.Null)
                {
                    if (state.state != Unit.State.Attacking)
                    {
                        // add component attack event wait stuff?
                        PostUpdateCommands.AddComponent(e, new AnimationCommand()
                        {
                            animationId = 2,
                            command = AnimationCommand.Command.Play,
                            loopCount = -1
                        });
                    }
                    state.state = Unit.State.Attacking;
                }
                else if (math.lengthsq(movement.velocity) > 0.00001f)
                {
                    if (state.state != Unit.State.Walking)
                    {
                        PostUpdateCommands.AddComponent(e, new AnimationCommand()
                        {
                            animationId = 1,
                            command = AnimationCommand.Command.Play,
                            loopCount = -1
                        });
                    }
                    state.state = Unit.State.Walking;
                }
                else
                {
                    if (state.state != Unit.State.Idle)
                    {
                        PostUpdateCommands.AddComponent(e, new AnimationCommand()
                        {
                            animationId = 0,
                            command = AnimationCommand.Command.Play,
                            loopCount = -1
                        });
                    }
                    state.state = Unit.State.Idle;                
                }
            });
        
            // TODO: use on animation complete event instead

        }
    }
}