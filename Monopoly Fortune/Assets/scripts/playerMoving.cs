using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static gameController;

public class playerMoving : MonoBehaviour
{
    public class Player
    {
        public static gameController game;
        public bool isBot;
        public bool overmoving = false;
        public List<Philia> philies = new List<Philia>();
        public Material color;

        private int balance = 10000;




        public Player(bool bot, Material c)
        {
            isBot = bot;
            color = c;
        }




        public int Balance
        {
            get { return balance; }
        }
        public bool CheckBalance(int value)
        {
            return balance + value >= 0;
        }
        public void balanceOperation(int value)
        {
            balance += value;
        }
        public void needPay(int sum)
        {
            if (this.isBot)
            {

            }
            else
            {

            }
            this.overmoving = true;
        }

        public bool tradeofferBot(List<Philia> takePh, List<Philia> givePh, int takeM, int giveM)
        {
            return true;
        }


    }

    //*********GENERAL**********

    public gameController game;
    public bool isBot;
    public Player player;
    public Material color;
    public Material colorForUI;
    public string Name;

    //*********MOVING***********
    private Animator animator;
    private int field = 0;
    public int Field
    {
        get { return field; }
    }
    public int skips;
    public float animationDelay = 0.7f;
    public int numberPlayer;
    public float moveSpeed = 5f;
    private float delay;
    Vector3 targetPosition;
    Vector3 newRotation;
    private Rigidbody _rigidbody;

    void Start()
    {
        Player.game = game;
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        numberPlayer--;
        player = new Player(isBot, color);
    }
    public void step(int count)
    {
        StartCoroutine(PlayAnimations(count));
    }
    private IEnumerator PlayAnimations(int count)
    {
        _rigidbody.useGravity = false;
        for (int i = 0; i < count; i++)
        {
            //_rigidbody.isKinematic = true;
            animator.applyRootMotion = true;
            animator.SetTrigger("jump");
            yield return new WaitForSeconds(animationDelay);
            animator.applyRootMotion = false;
            //_rigidbody.isKinematic = false;

            field++;
            //yield return new WaitForSeconds(0.3f);
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
                    targetPosition = new Vector3(-36, transform.position.y, -10.6f - (0.25f * numberPlayer));
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
                    targetPosition = new Vector3(-21.75f, transform.position.y, -21f + (0.25f * numberPlayer));
                    StartCoroutine(MoveToTarget(targetPosition));
                    newRotation = transform.rotation.eulerAngles;
                    newRotation.y = -90f;
                    transform.rotation = Quaternion.Euler(newRotation);
                    player.balanceOperation(500);
                    field = 0;
                    break;
                default:
                    delay = 0f;
                    break;

            }
            yield return new WaitForSeconds(delay);
            delay = 0.7f;
        }
        _rigidbody.useGravity = true;
        game.FieldActive();
        //game.playerEndMoving(); 
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