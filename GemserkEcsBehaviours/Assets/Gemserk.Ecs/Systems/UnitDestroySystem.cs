using Gemserk.Ecs.Components;
using Unity.Entities;

namespace Gemserk.Ecs.Systems
{
    public class UnitDestroySystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAllReadOnly<ToDestroy>().ForEach(e => {
                PostUpdateCommands.DestroyEntity(e);
            });
        }
    }
}
