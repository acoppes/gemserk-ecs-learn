using Unity.Entities;

namespace Gemserk.Ecs.Behaviours
{
    public class EntityBehavioursSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            // other events like init, config, state change, etc?
        
            // if initializing, then create context (ask behaviour for that)
        
            Entities.ForEach((Entity e, EntityBehaviourComponent t) =>
            {
                var behaviour = t.behaviour;
                behaviour.entityManager = EntityManager;
                behaviour.entity = e;
                behaviour.context = t.context;
                behaviour.OnEntityUpdated(Time.DeltaTime);
                // t.OnEntityUpdated(EntityManager, e);    
            });
        
            // if being destroyed, then destroy context as well (lose references)
        }
    }
}