using Unity.Entities;
using Unity.Mathematics;

public struct Projectile : IComponentData
{
    public float3 target;
    public Entity targetEntity;

   // public float speed;
   // public float3 velocity;

    public float damage;
    public int particleId;

    public float distance;

    // target ground, target attach point?
    // should that be projectile template configuration?
}

public struct ProjectileInitData : IComponentData
{
    public float duration;
}

/*
public struct ProjectileRuntimeData : IComponentData
{
    // this is useful to detect some stuff?
    public float3 origin;
}
*/

public struct ProjectileLinearTrajectory : IComponentData
{
    
}

public struct ProjectileParabolaTrajectory : IComponentData
{
    public float t;
    public float t0;
    public float t1;
    public float g;
    public float velocity;
    public float initialZ;
    public float3 internalSpeed;
}