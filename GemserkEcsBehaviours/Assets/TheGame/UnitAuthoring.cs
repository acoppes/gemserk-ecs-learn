using Gemserk.Ecs.Components;
using Unity.Entities;
using UnityEngine;

namespace TheGame
{
    public class UnitAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField]
        private EntityBehaviour behaviour;

        [SerializeField]
        private GameObject modelPrefab;

        [SerializeField]
        private RuntimeAnimatorController modelController;
    
        public float speed;

        public bool controlledByPlayer;
        public int player;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            if (behaviour != null && !controlledByPlayer)
            {
                dstManager.AddComponentData(entity, new EntityBehaviourComponent
                {
                    behaviour = behaviour
                });
            }
        
            dstManager.AddComponentData(entity, new Movement
            {
                speed = speed
            });
            dstManager.AddComponentData(entity, new Model
            {
                prefab = modelPrefab,
                controller = modelController
            });
            dstManager.AddComponentData(entity, new LookAt());

            if (controlledByPlayer)
            {
                dstManager.AddComponentData(entity, new ControlledByPlayer
                {
                    player = player
                });
            }
        }
    }
}