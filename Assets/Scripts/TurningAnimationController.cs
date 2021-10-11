using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningAnimationController : MonoBehaviour
{
    Animator animator;
    Movable movable;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movable = GetComponent<Movable>();
    }

    void Update()
    {
           
    }

    public void UpdateTurning()
    {
        if(animator != null && movable != null)
            animator.SetBool("Turning", movable.IsTurning());
    }
}
