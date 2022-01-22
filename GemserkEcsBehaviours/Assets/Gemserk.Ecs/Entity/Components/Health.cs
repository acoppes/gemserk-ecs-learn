using Unity.Entities;

public struct Health : IComponentData
{
    public float total;
    public float current;
}
