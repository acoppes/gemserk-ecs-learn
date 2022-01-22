using UnityEngine;

namespace Gemserk.Ecs.Models
{
    public class UnitSpriteEditModel : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _model;
    
        public UnitModelDefinitionAsset unitDefinition;

        [SerializeField] 
        private Transform _attachPointsContainer;

        public SpriteRenderer Model
        {
            get => _model;
            set => _model = value;
        }

        public Transform AttachPointsContainer
        {
            get => _attachPointsContainer;
            set => _attachPointsContainer = value;
        }

        private void OnValidate()
        {
            if (unitDefinition != null)
            {
                // set sprite
                if (unitDefinition.GetAnimationDefinition(0) != null)
                {
                    Model.sprite = unitDefinition.GetAnimationDefinition(0).frames[0];    
                }
            }
        }
    }
}