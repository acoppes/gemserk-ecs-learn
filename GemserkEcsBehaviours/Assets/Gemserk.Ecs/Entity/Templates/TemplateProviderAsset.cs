using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ironhide/Templates/Provider")]
public class TemplateProviderAsset : ScriptableObject, ITemplateProvider
{
    [SerializeField]
    private List<EntityTemplateBase> _templateObjects = new List<EntityTemplateBase>();
    
    public IEntityTemplate GetTemplate(int id)
    {
        return _templateObjects[id];
    }

    public T GetTemplate<T>(int id) where T : class, IEntityTemplate
    {
        return _templateObjects[id] as T;
    }
}