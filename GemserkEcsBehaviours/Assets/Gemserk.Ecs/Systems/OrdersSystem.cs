using Gemserk.Ecs.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Gemserk.Ecs.Systems
{
    public class OrdersSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<Alive>().ForEach((Entity e, ref Orders order, ref Movement movement, ref Translation t, ref Attack attack) => {

                // TODO: here or on unit system 
                if (order.currentOrder == Orders.Order.Move)
                {
                    if (!EntityManager.HasComponent<MovementDestination>(e))
                    {
                        PostUpdateCommands.AddComponent(e, new MovementDestination
                        {
                            value = order.destination
                        });
                    }
                    else
                    {
                        PostUpdateCommands.SetComponent(e, new MovementDestination
                        {
                            value = order.destination
                        });
                    }

                    attack.target = new Target()
                    {
                        entity = Entity.Null
                    };
                }
                else if (order.currentOrder == Orders.Order.Stop)
                {
                    // movement.velocity = float3.zero;
                    PostUpdateCommands.RemoveComponent<MovementDestination>(e);
                    attack.target = new Target
                    {
                        entity = Entity.Null
                    };
                }
                else if (order.currentOrder == Orders.Order.Attack)
                {
                    PostUpdateCommands.RemoveComponent<MovementDestination>(e);
                    attack.target = order.target;
                }

                PostUpdateCommands.RemoveComponent<Orders>(e);
            });
        }
    }
}