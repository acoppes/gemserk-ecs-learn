using UnityEngine;

public class ParticleDestroyCallback : MonoBehaviour
{
   [SerializeField]
   private GameObject _destroyObject;
   
   public void OnParticleSystemStopped()
   {
      if (_destroyObject != null)
         GameObject.Destroy(_destroyObject);
   }
}
