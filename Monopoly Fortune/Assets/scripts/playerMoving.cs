using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static gameController;
using static sellMenu;

public class playerMoving : MonoBehaviour
{
    public class Player
    {
        public bool isNeedPay = false;
        public bool inPrison = false;
        public int howMachNeedPay = 0;
        public static gameController game;
        public bool isBot;
        public bool overmoving = false;
        public List<Philia> philies = new List<Philia>();
        public Material color;
        public playerMoving thisMoving;
        public bool taxFreeRound = false;



        private int balance = 10000;




        public Player(bool bot, Material c, playerMoving thisMoving)
        {
            isBot = bot;
            color = c;
            this.thisMoving = thisMoving;
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
                int remainingSum = sum;
                howMachNeedPay = sum;
                isNeedPay = true;
                List<Philia> philiesToSell = new List<Philia>();

                foreach (Philia philia in philies)
                {
                    float philiaPrice = (float)philia.price * 0.9f;

                    if (philiaPrice >= remainingSum)
                    {
                        philiesToSell.Add(philia);
                        break;
                    }
                    else
                    {
                        philiesToSell.Add(philia);
                        remainingSum -= (int)philiaPrice;
                    }
                }

                foreach (Philia philia in philiesToSell)
                {
                    philia.ownerPlane.material = game.nullcolor;
                    philia.owner = null;
                    philies.Remove(philia);

                    int newPrice = (int)((float)philia.price * 0.9f);
                    balanceOperation(newPrice);
                    remainingSum -= newPrice;

                    if (remainingSum <= 0)
                        break;


                }
                if (remainingSum > 0)
                {
                    game.PlayerLost(this.thisMoving);
                }
            }
            else
            {
                game.mapCanvas.SetActive(true);
                game.mapCanvasGoButton.SetActive(false);
                game.youNeedPay.text = "You need pay\n" + sum;
                game.youNeedPay.gameObject.SetActive(true);

                howMachNeedPay = sum;
                isNeedPay = true;
            }
        }

        public bool tradeofferBot(List<Philia> takePh, List<Philia> givePh, int takeM, int giveM)
        {
            float randomFactor = UnityEngine.Random.Range(0.8f, 1.2f); // Ёлемент случайности

            // ќценка стоимости получаемых и отдаваемых филий
            float takePhiliaValue = 0f;
            foreach (Philia philia in takePh)
            {
                takePhiliaValue += (float)philia.price;
            }

            float givePhiliaValue = 0f;
            foreach (Philia philia in givePh)
            {
                givePhiliaValue += (float)philia.price;
            }

            // ќценка суммы получаемых и отдаваемых денег
            float takeMoneyValue = takeM * randomFactor;
            float giveMoneyValue = giveM * randomFactor;

            // ≈сли обща€ стоимость получаемых филий и денег превышает общую стоимость отдаваемых филий и денег,
            // то бот совершает сделку


            return (takePhiliaValue + takeMoneyValue) > (givePhiliaValue + giveMoneyValue);
        }


    }

    //*********GENERAL**********

    public gameController game;
    public bool isBot;
    public Player player;
    public Material color;
    public Material colorForUI;
    public string Name;
    public GameObject phisicPlayer;
    public GameObject avatarOnCanvas;
    public GameObject balanceTextOnCanvas;


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
        player = new Player(isBot, color, this);
    }
    private void Update()
    {
        if (player.isNeedPay)
        {
            if (player.CheckBalance(-player.howMachNeedPay))
            {
                player.isNeedPay = false;
                player.balanceOperation(-player.howMachNeedPay);
                player.howMachNeedPay = 0;
                player.overmoving = true;
                if (!isBot)
                {
                    game.youNeedPay.gameObject.SetActive(false);
                    game.mapCanvas.SetActive(false);
                    game.mapCanvasGoButton.SetActive(true);
                }
            }
        }

        if (balanceTextOnCanvas != null)
            balanceTextOnCanvas.GetComponentInChildren<Text>().text = player.Balance.ToString();

    }
    public void moveTo(int where)
    {
        step((36 - field + where) % 36);
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