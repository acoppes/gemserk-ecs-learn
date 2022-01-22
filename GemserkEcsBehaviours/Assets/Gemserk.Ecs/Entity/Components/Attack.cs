using Unity.Entities;

public struct Attack : IComponentData
{
    // what if I have multiple attack frames?? maybe use multiple AttackFrame component?

    // this might by a separated component, set by current ia behaviour
    public int attackFrame;
    public int animationId;
    public int attachPoint;
    
    public float damage;
    public int projectileId;
    public Target target;
    public int projectiles;
    
}

