﻿using Unity.Entities;

public interface IModelManager
{
    // TODO: use a custom identifier instead of entity

    IUnitModel GetModelInstance(Entity entity);

    void CreateModel(Entity entity, int modelId);

    void DestroyModel(Entity entity);
}
