using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gemserk.Ecs.Components
{
    public class ControlledByPlayer : IComponentData
    {
        public int player;
        public GameObject playerInputPrefab;

        public PlayerInput playerInputInstance;
    }
}