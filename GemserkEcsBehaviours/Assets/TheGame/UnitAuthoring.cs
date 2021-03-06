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

        public int player;
        public GameObject playerInputPrefab;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            if (behaviour != null && playerInputPrefab == null)
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

            if (playerInputPrefab != null)
            {
                dstManager.AddComponentData(entity, new ControlledByPlayer
                {
                    playerInputPrefab = playerInputPrefab,
                    player = player
                });
            }
            dstManager.AddComponentData(entity, new Attack());
        }
    }
}