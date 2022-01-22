﻿using Unity.Entities;

public struct Unit : IComponentData
{
    public enum State
    {
        Idle,
        Walking,
        Attacking,
        Dying,
        Dead
    }

    public enum DestroyLogic
    {
        None,
        DestroyOnDeath
    }
    
    public State state;

    public int deathAnimationId;
    public DestroyLogic destroyLogic;
    
}