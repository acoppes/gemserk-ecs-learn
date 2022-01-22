using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName ="Ironhide/Animation Definition")]
public class UnitModelDefinitionAsset : ScriptableObject
{
    // we could have specified each animation, like idle, walk, attack, etc, explicit in fields
    
    // also, we could cache animations in dictionary or array by id on enable.

    [SerializeField]
    private int _shadowSize;

    [SerializeField]
    private bool _hasShadow;

    [SerializeField]
    private bool _isProjectile;
    
    [SerializeField]
    private List<UnitAnimationDefinition> _animations;

    [SerializeField]
    private List<Vector3> _attachPoints = new List<Vector3>();
    
    public int ShadowSize => _shadowSize;

    public bool HasShadow => _hasShadow;

    public bool IsProjectile => _isProjectile;

    public List<Vector3> AttachPoints
    {
        get => _attachPoints;
        //set => _attachPoints = value;
    }

    // what if shadow depends on the animation's frame?
    
    public UnitAnimationDefinition GetAnimationDefinition(int id)
    {
        if (id < 0 || id >= _animations.Count)
            return null;
        return _animations[id];
    }

    // this sucks, change to list of animations and use index again...at least for now to get better perf
    /*    public UnityAnimationDefinition GetAnimationDefinition(string name)
        {
            return _animations.Find(a => a.name.Equals(name));
        }
        */


#if UNITY_EDITOR
    [ContextMenu("SortAnimationSprites")]
    public void SortAnimationSprites()
    {
        for (int i = 0; i < _animations.Count; i++)
        {
            var animation = _animations[i];
            var frames = animation.frames.ToList();
            frames.Sort((s1, s2) =>
            {
                return s1.name.CompareTo(s2.name);
            });
            animation.frames = frames.ToArray();
        }
        UnityEditor.EditorUtility.SetDirty(this);
    }

    public void OverrideAttachPoints(List<Vector3> attachPoints)
    {
        _attachPoints = attachPoints;
    }
#endif

}
