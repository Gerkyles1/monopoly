using System.Collections;
using UnityEngine;

public class playerMoving : MonoBehaviour
{
    private Animator animator;
    public float animationDelay = 0.5f; // Задержка между анимациями

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void step(int count)
    {
        StartCoroutine(PlayAnimations(count));
    }

    private IEnumerator PlayAnimations(int count)
    {
        for (int i = 0; i < count; i++)
        {
            animator.applyRootMotion = true;
            animator.SetTrigger("jump");
            yield return new WaitForSeconds(animationDelay);
            animator.applyRootMotion = false;

        }
    }
}
