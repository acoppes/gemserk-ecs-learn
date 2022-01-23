using Gemserk.Ecs.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

// [UpdateInGroup(typeof(SimulationSystemGroup))]
namespace Gemserk.Ecs.Systems
{
    public class MovementSystem : JobComponentSystem
    {       
        const float destinationDistance = 0.001f;
    
        // TODO: max speed (could be in movement system maybe?)

        // TODO: acceleration/desacceleration 

        [BurstCompile]
        private struct MovementDirectionJob : IJobForEach<Movement, Translation, MovementDestination>
        {
            public float dt;
        
            public void Execute(ref Movement movement, [ReadOnly] ref Translation translation, ref MovementDestination movementDestination)
            {
                var difference = movementDestination.value - translation.Value;
                difference.z = 0;
                movement.velocityDifference = math.normalize(difference) * dt;
                movementDestination.distanceSq = math.lengthsq(difference);
            }
        }
    
        [BurstCompile]
        private struct MovementPositionJob : IJobForEach<Movement, Translation>
        {
            public void Execute(ref Movement movement, ref Translation translation)
            {
                movement.velocity = movement.velocityDifference * movement.speed;
                translation.Value += movement.velocity;
                movement.velocityDifference = float3.zero;
            }
        }
    
        //[BurstCompile]
        private struct MovementDestinationReachedJob : IJobForEachWithEntity<MovementDestination>
        {
            // [ReadOnly]
//        [NativeDisableParallelForRestriction]
            public EntityCommandBuffer.ParallelWriter buffer;

            public void Execute(Entity entity, int index, [ReadOnly] ref MovementDestination destination)
            {
                if (destination.distanceSq > destinationDistance)
                    return;
                // movement.velocity = float3.zero;
                buffer.RemoveComponent<MovementDestination>(index, entity);
            }
        }
    
        private BeginInitializationEntityCommandBufferSystem _commandBufferSystem;

        protected override void OnCreate()
        {
            base.OnCreate();
            _commandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var movementDirectionJob = new MovementDirectionJob
            {
                dt = Time.DeltaTime
            };

            var movementTranslationJob = new MovementPositionJob();
        
            var movementDestinationReachedJob = new MovementDestinationReachedJob()
            {
                buffer = _commandBufferSystem.CreateCommandBuffer().AsParallelWriter()
            };

            var job1Handle = movementDirectionJob.Schedule(this, inputDependencies);
            var job2Handle = movementTranslationJob.Schedule(this, job1Handle);
            var job3Handle = movementDestinationReachedJob.Schedule(this, job2Handle);
        
            _commandBufferSystem.AddJobHandleForProducer(job3Handle);

            return job3Handle;
        }
    }
}

