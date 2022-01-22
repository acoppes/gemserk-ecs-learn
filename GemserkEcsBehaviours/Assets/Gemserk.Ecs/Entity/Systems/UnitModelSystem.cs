using Unity.Entities;
using Unity.Transforms;

[UpdateAfter(typeof(ModelCreateSystem))]
public class UnitModelSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.WithAllReadOnly<Translation>().ForEach((Entity e, ref LookAt lookAt, ref ModelInstance m) => {
            m.lookingDirection = lookAt.direction;
        });
    }
}
