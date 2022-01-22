using Unity.Entities;
using Unity.Transforms;
using UnityEngine.Profiling;

public class ModelCreateSystem : ComponentSystem
{
    public IModelManager ModelManager { get; set; }
    
    protected override void OnUpdate()
    {
        if (ModelManager == null)
            return;
        
        Entities.WithAll<Model>().WithNone<ModelInstance, ToDestroy>().ForEach((Entity e, ref Model model) => {
            ModelManager.CreateModel(e, model.modelId);
            // var m = ModelManager.GetModelInstance(e);
            // m.SetHasShadow(EntityManager.HasComponent<ModelShadow>());
            // PostUpdateCommands.RemoveComponent<Model>(e);
            
            PostUpdateCommands.AddComponent(e, new ModelInstance());
            PostUpdateCommands.AddSharedComponent(e, new SharedModel
            {
                instance = ModelManager.GetModelInstance(e)
            });
        });
    }
}

[UpdateAfter(typeof(ModelCreateSystem))]
//[UpdateBefore(typeof(ModelDestroySystem)), UpdateAfter(typeof(ModelCreateSystem))]
public class ModelUpdateSystem : ComponentSystem
{
//    public IModelManager ModelManager { get; set; }
    
    protected override void OnUpdate()
    {
//        if (ModelManager == null)
//            return;
        
        Profiler.BeginSample("Model.Position");
        Entities.ForEach((Entity e, ref Translation t, ref ModelInstance m) =>
        {
            // convert to IM/KR world perspective
            //var p = t.Value;
            //p.y *= 0.75f;
            
            // same for looking direction?
            
//            var model = ModelManager.GetModelInstance(e);

            var model = EntityManager.GetSharedComponentData<SharedModel>(e).instance;
            
            model.SetPosition(t.Value);
            model.SetLookingDirection(m.lookingDirection);
        });
        Profiler.EndSample();
        
        Profiler.BeginSample("Model.AttachPoints");
        Entities.WithAllReadOnly<Alive, ModelInstance>().ForEach(
            (Entity e, ref Translation p, ref AttachPoints target, ref Model m) =>
        {
//            var model = ModelManager.GetModelInstance(e);
            var model = EntityManager.GetSharedComponentData<SharedModel>(e).instance;
            var position = p.Value;
            position.z = 0;
            
            target.bodyPosition = position + model.GetAttachPoint(m.bodyAttachPoint);
            target.headPosition = position + model.GetAttachPoint(m.headAttachPoint);
            target.attackPosition = position + model.GetAttachPoint(m.attackAttachPoint);          
        });
        Profiler.EndSample();
        
        Profiler.BeginSample("Model.AnimationFrame");
        Entities.WithAllReadOnly<ModelInstance>().ForEach((Entity e, ref Animation animation) =>
        {
//            var model = ModelManager.GetModelInstance(e);
            var model = EntityManager.GetSharedComponentData<SharedModel>(e).instance;
            model.SetAnimationFrame(animation.animationId, animation.currentFrame);
        });
        Profiler.EndSample();
    }
}

[UpdateAfter(typeof(ModelUpdateSystem)), UpdateBefore(typeof(UnitDestroySystem))]
public class ModelDestroySystem : ComponentSystem
{
    public IModelManager ModelManager { get; set; }
    
    protected override void OnUpdate()
    {
        Entities.WithAllReadOnly<ModelInstance, ToDestroy>().ForEach(e => {
            ModelManager.DestroyModel(e);
        });
    }
}