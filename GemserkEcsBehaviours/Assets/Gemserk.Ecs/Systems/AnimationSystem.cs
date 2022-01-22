using Gemserk.Ecs.Components;
using Gemserk.Ecs.Models;
using Unity.Entities;

namespace Gemserk.Ecs.Systems
{
    // TODO: animation event given declared events in animation def

    [UpdateBefore(typeof(AnimationSystem))]
    public class AnimationCommandSystem : ComponentSystem
    {
        public IModelManager ModelManager { get; set; }
    
        protected override void OnUpdate()
        {
            // how and where should I put animation definition events?
        
            // TODO: configure animaiton loop (using model)

            Entities.WithAll<ModelInstance>().ForEach((Unity.Entities.Entity e, ref Animation animation, ref AnimationCommand command) => {

                if (command.command == AnimationCommand.Command.Play)
                {
                    var model = ModelManager.GetModelInstance(e);
                    var totalFrames = model.GetAnimationFrames(command.animationId);

                    animation.animationId = command.animationId;
                    // animation.currentFrame = command.startingFrame;
                    animation.currentFrame = -1;
                    animation.loopCount = command.loopCount;
                    animation.state = Animation.State.Playing;
                    animation.speed = 1.0f;
                    animation.totalFrames = totalFrames;
                    animation.fps = command.fps > 0 ? command.fps : animation.fps;
                    animation.currentFrameTime = 1.0f / animation.fps;
                
                    EntityManager.AddComponent<AnimationPlaying>(e);
                }

                if (command.command == AnimationCommand.Command.Stop)
                {
                    animation.currentFrameTime = 0.0f;
                    animation.currentFrame = 0;
                    animation.state = Animation.State.Stopped;
                
                    EntityManager.RemoveComponent<AnimationPlaying>(e);
                }

                if (command.command == AnimationCommand.Command.Pause)
                {
                    if (animation.state == Animation.State.Paused)
                    {
                        animation.state = Animation.State.Playing;
                        EntityManager.AddComponent<AnimationPlaying>(e);
                    }
                    else
                    {
                        animation.state = Animation.State.Paused;
                        EntityManager.RemoveComponent<AnimationPlaying>(e);
                    }
                }

                PostUpdateCommands.RemoveComponent<AnimationCommand>(e);
            });
        }
    }

    public class AnimationSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            // how and where should I put animation definition events?
        
            // TODO: use animation Playing state
//        Entities.WithAllReadOnly<AnimationPlaying, AnimationFrameChanged>().ForEach(e =>
//        {
//            PostUpdateCommands.RemoveComponent<AnimationFrameChanged>(e);
//        });
        
            Entities.WithAllReadOnly<AnimationPlaying>().ForEach((Unity.Entities.Entity e, ref Animation animation) => {
                var frameTime = 1.0f / animation.fps;
            
                animation.currentFrameTime += Time.DeltaTime * animation.speed;
            
                animation.changedFrame = false;
                animation.previousFrame = animation.currentFrame;

                if (animation.currentFrameTime > frameTime)
                {
                    // we can update more than one time the same animation, however, we should store
                    // the creation time of each event since they must know that information when performing 
                    // the reactive logic.

                    animation.currentFrameTime -= frameTime;

                    // increase frame, reduce current frame time
                    int frame = animation.currentFrame + 1;

                    if (frame >= animation.totalFrames)
                    {
                        animation.loopCount--;

                        if (animation.loopCount == 0)
                        {
                            frame = animation.totalFrames;
                            animation.state = Animation.State.Stopped;

                            var completedEvent = PostUpdateCommands.CreateEntity();
                            PostUpdateCommands.AddComponent(completedEvent, new AnimationCompletedEvent()
                            {
                                entity = e,
                                animationId = animation.animationId
                            });
                            PostUpdateCommands.AddComponent(completedEvent, new DestroyAtFrameEnd());
                        
                        }
                        else
                        {
                            frame -= animation.totalFrames;
                        }
                    }
                
//                PostUpdateCommands.AddComponent(e, new AnimationFrameChanged());
                
                    animation.changedFrame = true;
                    // update frame metadata?
                    animation.currentFrame = frame;
                }
            
            });

            // set animation frame in one of the possible visual systems (call this in late update)
            // this could be moved to another system

        }
    }
}