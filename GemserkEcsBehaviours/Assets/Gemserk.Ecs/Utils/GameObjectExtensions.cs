using UnityEngine;

namespace Gemserk.Ecs.Utils
{
    public static class GameObjectExtensions 
    {
        public static T GetInterface<T>(this Object o) where T : class
        {
            return o switch
            {
                T t => t,
                GameObject go => go.GetComponentInChildren<T>(),
                _ => null
            };
        }
    }
}
