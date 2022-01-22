using Gemserk.Ecs.Components;
using Unity.Mathematics;

public class TestUnitBehaviour1 : EntityBehaviour
{
    public class MyCustomContext
    {
        public int state = 1;

        public float normalSpeed = 0.5f;
        public float superSpeed = 1.0f;

        public float cooldown = 2;
        public float currentTime = 0;
    }

    public override void OnEntityUpdated(float dt)
    {
        // Debug.Log("hola");
        
        // TODO: add idle...

        var myContext = GetContext<MyCustomContext>();
        
        myContext.currentTime -= dt;

        if (myContext.state == 1)
        {
            if (myContext.currentTime <= 0)
            {
                myContext.currentTime = myContext.cooldown;
                myContext.state = 0;
                
                var movement = GetComponentData<Movement>();
                movement.speed = myContext.normalSpeed;
                SetComponentData(movement);
            }
            
        } else if (myContext.state == 0)
        {
            if (myContext.currentTime <= 0)
            {
                myContext.currentTime = myContext.cooldown;
                myContext.state = 1;
                
                var movement = GetComponentData<Movement>();
                movement.speed = myContext.superSpeed;
                SetComponentData(movement);
            }
        }
        
        if (HasComponent<MovementDestination>())
        {
            return;
        }
        
        var p = UnityEngine.Random.insideUnitCircle * 2.0f;
        AddComponentData(new MovementDestination
        {
            value = new float3(p.x, p.y, 0)
        });

        AddComponentData(new AnimationCommand
        {
            animationId = 1,
            command = AnimationCommand.Command.Play,
            loopCount = -1
        });
    }
}