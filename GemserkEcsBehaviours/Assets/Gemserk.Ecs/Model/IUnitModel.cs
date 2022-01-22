using Unity.Mathematics;
using UnityEngine;

public interface IUnitModel
{
    void SetLookingDirection(float3 direction);

    // float GetStateTransitionTime(int state);

    float3 GetAttachPoint(int attachPointId);

    void SetPosition(float3 position);

    int GetAnimationFrames(int animationId);

    void SetAnimationFrame(int animationId, int frame);
    
    // void SetShadowSize(int shadowSize);
}