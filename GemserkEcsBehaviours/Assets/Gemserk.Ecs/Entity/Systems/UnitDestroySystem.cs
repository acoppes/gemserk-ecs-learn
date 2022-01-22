using Unity.Entities;

public class UnitDestroySystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.WithAllReadOnly<ToDestroy>().ForEach(e => {
            PostUpdateCommands.DestroyEntity(e);
        });
    }
}
