using System;
using Unity.Entities;

public struct Animation : IComponentData
{
    // could use other components for animation states and multiple foreachs instead
    public enum State
    {
        Playing,
        Paused,
        Stopped
    }

    public float currentFrameTime;
    
    // public int lastEventId;

    public State state;
    public int loopCount;

    public int animationId;
    
    public int totalFrames;
    public int currentFrame;

    public float speed;
    public float fps;

    public int previousFrame;
    public bool changedFrame;
}

public struct AnimationPlaying : IComponentData
{
    
}

[Serializable]
public struct AnimationCommand : IComponentData
{
    // enum command : Play, Stop, Pause
    public enum Command
    {
        Play,
        Stop,
        Pause
    }

    public Command command;
    public int animationId;
    public int loopCount;

    // move to definition

    // public int totalFrames;
    public float fps;

    // public int startingFrame;
    // public float startingTime;
}

public struct DestroyAtFrameEnd : IComponentData
{

}

public struct AnimationCompletedEvent : IComponentData
{
    // public float engineTime;
    // public int engineFrame;
    public Entity entity;
    public int animationId;
}
