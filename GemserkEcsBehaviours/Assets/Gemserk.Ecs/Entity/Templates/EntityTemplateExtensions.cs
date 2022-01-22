using Unity.Entities;

public static class EntityTemplateExtensions
{
    public static Entity CreateEntityFromTemplate(this EntityManager em, IEntityTemplate t, IEntityTemplateParameters parameters)
    {
        var template = t as EntityTemplateBase;
        if (template)
        {
            template.EntityManager = em;
            template.Parameters = parameters;
        }

        var entity = em.CreateEntity();
        t.Apply(entity);

        if (template)
        {
            template.EntityManager = null;
            template.Parameters = null;
        }
        
        return entity;
    }
    
    public static Entity CreateEntityFromTemplate(this EntityCommandBuffer ecb, IEntityTemplate t, IEntityTemplateParameters parameters)
    {
        var template = t as EntityTemplateBase;
        if (template)
        {
            template.EntityManager = null;
            template.Parameters = parameters;
            template.EntityCommandBuffer = ecb;
        }

        var entity = ecb.CreateEntity();
        t.Apply(entity);

        if (template)
        {
            template.Parameters = null;
        }
        
        return entity;
    }
}