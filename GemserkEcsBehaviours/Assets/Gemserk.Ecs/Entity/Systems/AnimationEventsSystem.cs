using Unity.Entities;
// update at the end of frame?
[UpdateAfter(typeof(AnimationSystem))]
public class AnimationEventsSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.WithAll<DestroyAtFrameEnd>().ForEach((Entity e) => {
            // post in another system?
            PostUpdateCommands.DestroyEntity(e);
        });
    }
}
