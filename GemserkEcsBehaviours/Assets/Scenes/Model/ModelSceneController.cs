using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class ModelSceneController : MonoBehaviour
{
    public Animator animator;

    public RuntimeAnimatorController animatorController;
    
    // Start is called before the first frame update
    void Start()
    {
        animator.runtimeAnimatorController = animatorController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
