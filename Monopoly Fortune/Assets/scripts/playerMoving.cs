using System.Collections;
using UnityEngine;

public class playerMoving : MonoBehaviour
{
    private Animator animator;
    private int field=0;
    public int Field
    {
        get { return field; }
        //set { field = value % 36; }
    }
    public int skips;

    public float animationDelay = 0.7f;
    public int numberPlayer;
    public float moveSpeed = 5f;
    private float delay;
    Vector3 targetPosition;
    Vector3 newRotation;

    void Start()
    {
        animator = GetComponent<Animator>();
        numberPlayer--;
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
            field++;
            switch (field)
            {
                case 11:
                    targetPosition = new Vector3(-36.5f + (0.25f * numberPlayer), transform.position.y, -20.25f);
                    StartCoroutine(MoveToTarget(targetPosition));
                    newRotation = transform.rotation.eulerAngles;
                    newRotation.y = 0f;
                    transform.rotation = Quaternion.Euler(newRotation);
                    break;
                case 18:
                    targetPosition = new Vector3(-36 , transform.position.y, -10.6f - (0.25f * numberPlayer));
                    StartCoroutine(MoveToTarget(targetPosition));
                    newRotation = transform.rotation.eulerAngles;
                    newRotation.y = 90f;
                    transform.rotation = Quaternion.Euler(newRotation);
                    break;
                case 29:
                    targetPosition = new Vector3(-20.75f - (0.25f * numberPlayer), transform.position.y, -11.5f);
                    StartCoroutine(MoveToTarget(targetPosition));
                    newRotation = transform.rotation.eulerAngles;
                    newRotation.y = 180f;
                    transform.rotation = Quaternion.Euler(newRotation);
                    break;
                case 36:
                    targetPosition = new Vector3(-21.75f , transform.position.y, -21f+ (0.25f * numberPlayer));
                    StartCoroutine(MoveToTarget(targetPosition));
                    newRotation = transform.rotation.eulerAngles;
                    newRotation.y = -90f;
                    transform.rotation = Quaternion.Euler(newRotation);
                    field = 0;
                    break;
                default:
                    delay = 0f;
                    break;

            }
            yield return new WaitForSeconds(delay);
            delay = 0.5f;
            

        }
    }
    private System.Collections.IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        float elapsedTime = 0f;

        while (elapsedTime < distance / moveSpeed)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / (distance / moveSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}