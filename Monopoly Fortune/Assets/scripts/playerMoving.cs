using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoving : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void step(int count)
    {
        for (int i=0;i<count;i++)
        {
            animator.SetTrigger("jump");


        }


    }
}
