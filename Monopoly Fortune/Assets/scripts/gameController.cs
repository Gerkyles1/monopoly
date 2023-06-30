using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static playerMoving;


public class gameController : MonoBehaviour
{
    public List<playerMoving> playerScripts;
    public cub cub;
    private playerMoving nowplayer;
    public int nowPlayerIndex = 0;
    public companyDetalis detalis;
    public static Field[] map;
    public Text balance;
    public Text[] Botbalances;

    public List<MeshRenderer> ownerColors;
    public GameObject[] builds;
    public Sprite[] logotipos;
    public Image auctionlogo;
    public WheelOfFortune wheelOfFortune;
    public GameObject mapCanvasGoButton;
    public Text youNeedPay;
    public Material nullcolor;
    public GameObject prisonCoise;

    //private bool startauction = false;


    void Start()
    {
        map = new Field[]
        {
            new balanceOper(0, 0),//start
            new balanceOper(1, 1500),
            new Philia(2, "Tefaty", 500, new int[]{150, 300, 600}, new int[]{3, 4}, builds[0], logotipos[0], "A French company, manufacturer and inventor of non-stick cookware. The company is represented in 120 countries.", ownerColors[0]),
            new Philia(3, "Zuins", 700, new int[]{200, 350, 700}, new int[]{2, 4}, builds[1], logotipos[1], "Italian manufacturer of household appliances that has been exporting products from Italy since 1946", ownerColors[1]),
            new Philia(4, "PlusTech", 900, new int[]{250, 400, 800}, new int[]{2, 3}, builds[2], logotipos[2], "Is an international concern operating in the field of electronics, equipment for children, and lighting.", ownerColors[2]),
            new Chance(5),
            new balanceOper(6, -500),
            new Philia(7, "OgMa", 1000, new int[]{250, 500, 1000}, new int[]{8, 9}, builds[3], logotipos[3], "Manufacturer of luxury Swiss watches", ownerColors[3]),
            new Philia(8, "ZeHipe", 1200, new int[]{250, 500, 1000}, new int[]{7, 9}, builds[4], logotipos[4], "Swiss luxury watches for men and women, combining rich watchmaking traditions with bold innovations.", ownerColors[4]),
            new Philia(9, "Rotartime", 1500, new int[]{300, 600, 1200}, new int[]{7, 8}, builds[5], logotipos[5], "Swiss watchmaking company manufacturing watches and related accessories", ownerColors[5]),
            new balanceOper(10, 2000),
            new Spetial(11, (player)=>
            {
                wheelOfFortune.spin(player.thisMoving);
            }),//casino
            new Philia(12, "Foyt", 1800, new int[]{350, 700, 1400}, new int[]{13, 14}, builds[6], logotipos[6], "One of the leading industrialists in America, who radically changed the principle of automobile assembly line operation", ownerColors[6]),
            new Philia(13, "Jafar", 2000, new int[]{350, 700, 1400}, new int[]{12, 14}, builds[7], logotipos[7], "British automotive company specializing in the production of luxury limousines, sports coupes and racing cars", ownerColors[7]),
            new Philia(14, "MB", 2300, new int[]{400, 800, 1600}, new int[]{12, 13}, builds[8], logotipos[8], "German brand name for vehicles", ownerColors[8]),
            new Philia(15, "GBK", 2500, new int[]{400, 800, 1600}, new int[]{16, 17}, builds[9], logotipos[9], "A global biopharmaceutical company with a mission to combine science, technology and talent to prevent disease together", ownerColors[9]),
            new Philia(16, "Pyver", 2800, new int[]{450, 900, 1800}, new int[]{15, 17}, builds[10], logotipos[10], "An American pharmaceutical company. It was founded in 1849 and has been one of the world's market leaders ever since.", ownerColors[10]),
            new Philia(17, "JJ", 3100, new int[]{450, 900, 1800}, new int[]{15, 16}, builds[11], logotipos[11], "American company, a major manufacturer of cosmetic and sanitary products, as well as medical equipment.", ownerColors[11]),
            new Spetial(18, (player)=>
            {
                int tax = player.philies.Count * 500;
                if (player.CheckBalance(-tax))
                {
                    player.balanceOperation(-tax);
                    player.overmoving = true;
                }
                else
                    player.needPay(tax);
            }),//Tax Inspectorate
            new Chance(19),
            new Philia(20, "Repip", 3300, new int[]{500, 1000, 2000}, new int[]{21, 22}, builds[12], logotipos[12], "A company that produces cherry drinks", ownerColors[12]),
            new Philia(21, "Cocla", 3500, new int[]{500, 1000, 2000}, new int[]{20, 22}, builds[13], logotipos[13], "Carbonated soft drink and the American company Cocla of the same name", ownerColors[13]),
            new Philia(22, "Pins", 3700, new int[]{550, 1100, 2200}, new int[]{20, 21}, builds[14], logotipos[14], "A soft drink sold worldwide. The main competitor of Coca-Cola.", ownerColors[14]),
            new balanceOper(23, -500),
            new balanceOper(24, 5000),
            new Philia(25, "NP", 4000, new int[]{550, 1100, 2200}, new int[]{26, 27}, builds[15], logotipos[15], "American manufacturer of graphics processors, video adapters", ownerColors[15]),
            new Philia(26, "Radon", 4200, new int[]{600, 1200, 2400}, new int[]{25, 27}, builds[16], logotipos[16], "A brand of computer products, including graphics processors, random access memory, software for RAM disks and solid-state drives", ownerColors[16]),
            new Philia(27, "IMPT", 4300, new int[]{650, 1300, 2600}, new int[]{25, 26}, builds[17], logotipos[17], "The world's largest semiconductor and device company, best known as a developer and manufacturer of x86-series microprocessors, processors for IBM-compatible personal computers.", ownerColors[17]),
            new Chance(28),
            new Spetial(29, (player)=>
            {
                if (!player.isBot)
                {
                    if (player.CheckBalance(-1000))
                    {
                        prisonCoise.SetActive(true);
                    }
                    else
                    {
                        prisonSkipTurn();
                    }
                }
                    
                else
                {
                    if (player.CheckBalance(-1000))
                    {
                        prisonPay();
                    }
                    else
                    {
                        prisonSkipTurn();
                    }
                }
            }),//Prison
            new Philia(30, "RX", 4500, new int[]{700, 1400, 2800}, new int[]{31, 32}, builds[18], logotipos[18], "The largest commercial TV channel in Germany", ownerColors[18]),
            new Philia(31, "ESN", 4700, new int[]{750, 1500, 3000}, new int[]{30, 32}, builds[19], logotipos[19], "American cable sports television channel.", ownerColors[19]),
            new Philia(32, "Expat", 4800, new int[]{750, 1500, 3000}, new int[]{30, 31}, builds[20], logotipos[20], "Pan-European television sports network.", ownerColors[20]),
            new Philia(33, "WR", 5000, new int[]{800, 1600, 3000}, new int[]{34, 35}, builds[21], logotipos[21], "American company, one of the largest concerns in the production of films and television series", ownerColors[21]),
            new Philia(34, "Povga", 5500, new int[]{850, 1600, 3200}, new int[]{33, 35}, builds[22], logotipos[22], "An American film company based in Hollywood, California. It was founded in 1912 and is the oldest studio engaged in the production of motion pictures.", ownerColors[22]),
            new Philia(35, "PooWan", 6000, new int[]{850, 1700, 3400}, new int[]{33, 34}, builds[23], logotipos[23], "The oldest surviving movie studio in the United States", ownerColors[23]),
        };
        Field.game = this;
        Philia.detalis = detalis;
        nowplayer = playerScripts[nowPlayerIndex];
    }
    public void meinMethod()
    {
        if (nowplayer.skips > 0)
        {
            nowplayer.skips--;
            nowplayer.player.overmoving = true;
            return;
        }
        cub.Throw(nowplayer);
    }
    public GameObject mapCanvas;

    public void FieldActive()
    {
        map[nowplayer.Field].active(nowplayer.player);
    }

    public void playerEndMoving()
    {

        nowPlayerIndex = (nowPlayerIndex + 1) % playerScripts.Count;
        nowplayer = playerScripts[nowPlayerIndex];
        if (nowplayer.player.isBot)
        {
            meinMethod();
            return;
        }
        else
        {
            mapCanvas.SetActive(true);
            return;
        }
    }
    public void buyPh()
    {
        if (nowplayer.player.CheckBalance(-((Philia)map[nowplayer.Field]).price))
        {
            nowplayer.player.balanceOperation(-((Philia)map[nowplayer.Field]).price);
            ((Philia)map[nowplayer.Field]).swichOwner(nowplayer.player);
            nowplayer.player.overmoving = true;

        }
        else
            FieldActive();
        //not enough money

    }
    public void Update()
    {
        if (nowplayer.player.overmoving)
        {
            nowplayer.player.overmoving = false;
            playerEndMoving();
        }/*
        if (startauction)
        {
            startauction = false;
            auction();
        }*/
    }

    public auctionDataUI auctionData;

    private List<Player> auctionPlayers;
    private int currentBid;
    private int currentPlayerIndex;
    private Philia nowAuctionphilia;
    private bool auctionIs = false;


    public void auction()
    {
        auctionPlayers = new List<Player>();
        nowAuctionphilia = (Philia)map[nowplayer.Field];

        currentBid = nowAuctionphilia.price;
        foreach (playerMoving i in playerScripts)
        {
            if (i != nowplayer && i.player.CheckBalance(-currentBid))
            {
                auctionPlayers.Add(i.player);
            }
        }

        currentPlayerIndex = 0;

        if (auctionPlayers.Count == 0)
        {
            nowplayer.player.overmoving = true;
            return;
        }
        if (auctionPlayers.Count == 1)
        {
            if (auctionPlayers[currentPlayerIndex].isBot)
            {
                System.Random random = new System.Random();
                int variant = random.Next(1, 11);
                if (variant != 4 && variant != 6)
                {
                    OnYesButtonPressed();
                }
                else
                    OnNoButtonPressed();

            }
            else
            {
                auctionlogo.sprite = nowAuctionphilia.logotipies;
                auctionData.UpdateData(currentBid);
            }
            return;
        }

        StartAuction();
    }

    private void StartAuction()
    {

        while (auctionPlayers.Count > 1)
        {
            if (currentPlayerIndex >= auctionPlayers.Count)
            {
                currentPlayerIndex = 0;
            }

            Player currentPlayer = auctionPlayers[currentPlayerIndex];
            if (!currentPlayer.CheckBalance(-(currentBid + 20)))
            {
                auctionPlayers.RemoveAt(currentPlayerIndex);
                continue;
            }

            if (currentPlayer.isBot)
            {
                System.Random random = new System.Random();
                int variant = random.Next(1, 11);
                if (variant != 4 && variant != 6)
                {
                    OnYesButtonPressed();
                }
                else
                    OnNoButtonPressed();
                return;
            }
            else
            {
                auctionlogo.sprite = nowAuctionphilia.logotipies;
                auctionData.UpdateData(currentBid);
                return;
            }

            //currentPlayerIndex++; 
        }

        if (!auctionIs)
        {
            auctionIs = true;
            nowplayer.player.overmoving = true;
            return;
        }
        auctionData.cloth();
        Player lastAuctioneer = auctionPlayers[0];
        int lastBidPrice = currentBid;

        lastAuctioneer.balanceOperation(-lastBidPrice);
        ((Philia)map[nowplayer.Field]).swichOwner(lastAuctioneer);
        nowplayer.player.overmoving = true;


    }

    public void OnYesButtonPressed()
    {
        auctionIs = true;
        currentBid += 20;
        auctionData.UpdateData(currentBid);

        currentPlayerIndex++;

        StartAuction();
    }

    public void OnNoButtonPressed()
    {


        if (auctionPlayers.Count > 1)
            auctionPlayers.RemoveAt(currentPlayerIndex);

        else if (auctionPlayers.Count == 1)
        {
            nowplayer.player.overmoving = true;
            return;
        }

        auctionData.UpdateData(currentBid);

        StartAuction();
    }
    //********************trade

    public GameObject TradeParentForPlayers;
    public GameObject PerfabTrade;
    public tradewin tradewin;
    public void TradeButton()
    {
        if (playerScripts.Count == 2)
        {
            playerMoving trader2 = playerScripts[0] == nowplayer ? playerScripts[1] : playerScripts[0];

            tradewin.tradeWith(nowplayer, playerScripts[1]);
        }
        foreach (Transform child in TradeParentForPlayers.transform)
            Destroy(child.gameObject);

        for (int i = 1; i < playerScripts.Count; i++)
        {
            GameObject clone = Instantiate(PerfabTrade);
            Button cloneButton = clone.GetComponent<Button>();



            cloneButton.GetComponentInChildren<Text>().text = playerScripts[i].Name;
            int j = i;
            cloneButton.onClick.AddListener(() =>
            {
                tradewin.tradeWith(nowplayer, playerScripts[j]);

            });

            Image image = cloneButton.GetComponent<Image>();
            image.material = playerScripts[i].colorForUI;


            clone.transform.SetParent(TradeParentForPlayers.transform);
            clone.SetActive(true);


        }

    }

    public void PlayerLost(playerMoving player)
    {
        nowPlayerIndex = nowPlayerIndex == 0 ? playerScripts.Count - 1 : nowPlayerIndex - 1;
        foreach (Philia philia in player.player.philies)
        {
            philia.swichOwner(null);
        }
        playerScripts.Remove(player);
        if (player.avatarOnCanvas != null)
            Destroy(player.avatarOnCanvas);
        Destroy(player.balanceTextOnCanvas);
        Destroy(player.phisicPlayer);
        nowplayer.player.overmoving = true;
    }
    public GameObject chanceCard;
    public chanceAnimator chanceAnimator;

    public abstract class Field
    {
        public int nomer;
        public static gameController game;
        public abstract void active(Player nowplayer);
    }
    public void StartAction(Chance.ChanceAction action, Player nowplayer)
    {
        action.action(nowplayer);
    }
    public void prisonSkipTurn()
    {
        nowplayer.skips += 3;
        nowplayer.player.overmoving = true;
        prisonCoise.SetActive(false);
    }
    public void prisonPay()
    {
        nowplayer.player.balanceOperation(-1000);
        nowplayer.player.overmoving = true;
        prisonCoise.SetActive(false);

    }

    public class Philia : Field
    {
        public static companyDetalis detalis;
        public Player owner = null;
        public string name;
        public int price;
        public int[] rent;
        public int[] colabs;
        public string description;
        public bool isMortgaged = false;

        public MeshRenderer ownerPlane;
        public GameObject build;
        public Sprite logotipies;



        public Philia(int nomer, string name, int price, int[] rent, int[] colabs, GameObject build, Sprite logotipies, string description, MeshRenderer plane)
        {
            this.name = name;
            this.nomer = nomer;
            this.price = price;
            this.rent = rent;
            this.colabs = colabs;
            this.description = description;
            this.build = build;
            this.logotipies = logotipies;
            this.ownerPlane = plane;
        }

        public override void active(Player nowplayer)
        {
            if (owner != null)
            {
                if (nowplayer == owner)
                {
                    nowplayer.overmoving = true;
                    return;
                }
                int rent = getRent();
                if (nowplayer.CheckBalance(-rent))
                {
                    nowplayer.balanceOperation(-rent);
                    nowplayer.overmoving = true;
                }
                else
                    nowplayer.needPay(rent);

                owner.balanceOperation(rent);
            }
            else if (!nowplayer.isBot)
            {
                detalis.show(this);
            }
            else if (nowplayer.isBot)
            {
                if (nowplayer.CheckBalance(-this.price))
                {
                    System.Random random = new System.Random();
                    int variant = random.Next(1, 11);
                    if (variant == 4)
                    {
                        game.auction();
                        return;
                    }

                    game.buyPh();
                }
                else
                {

                    game.auction();

                }
            }
        }

        private int getRent()
        {
            //if (this.isMortgaged)
            //    return 0;
            int count = 0;

            foreach (Philia philia in owner.philies)
            {
                foreach (int colab in this.colabs)
                {
                    if (philia.nomer == colab && !philia.isMortgaged)
                    {
                        count++;
                        break;
                    }
                }
            }

            count = count > 2 ? 2 : count;

            return this.rent[count];
        }

        public void swichOwner(Player newOwner)
        {
            if (newOwner == null)
            {
                ownerPlane.material = game.nullcolor;
                owner = null;
                return;
            }
            if (owner != null)
                owner.philies.Remove(this);

            owner = newOwner;
            owner.philies.Add(this);
            ownerPlane.material = owner.color;
        }

        /*
        public void mortgage()
        {
            isMortgaged = true;
            owner.balanceOperation(price/2);
        }
        public void demortgage()
        {
            owner.needPay((int)(price*2+price*0.1f));
            isMortgaged = false;
        }
        */
    }

    public class balanceOper : Field
    {
        int sum;
        public balanceOper(int nomer, int sum)
        {
            this.nomer = nomer;
            this.sum = sum;
        }
        public override void active(Player nowplayer)
        {
            if (nowplayer.CheckBalance(sum))
            {
                nowplayer.balanceOperation(sum);
                nowplayer.overmoving = true;
            }
            else
                nowplayer.needPay(-sum);
        }
    }

    public class Chance : Field
    {
        public class ChanceAction
        {
            public string text;
            public Action<Player> action;

            public ChanceAction(string text, Action<Player> action)
            {
                this.text = text;
                this.action = action;
            }
        }

        private static ChanceAction[] chanceActions = new ChanceAction[]
        {
            new ChanceAction("A gift of 1000", player =>
            {
                player.balanceOperation(1000);
                player.overmoving = true;
            }),
            new ChanceAction("Go to the sector 'start'", player =>
            {
                player.thisMoving.moveTo(0);
            }),
            new ChanceAction("Happy Birthday (each player gives 300 each)", player =>
            {
                int totalPlayers = game.playerScripts.Count;
                int amount = (totalPlayers - 1) * 300;
                foreach (playerMoving otherPlayer in game.playerScripts)
                {
                    if (otherPlayer.player != player)
                    {
                        if (otherPlayer.player.CheckBalance(-300))
                            otherPlayer.player.balanceOperation(-300);
                        else
                            otherPlayer.player.balanceOperation(-otherPlayer.player.Balance);
                    }
                }
                player.balanceOperation(amount);
                player.overmoving = true;
            }),
            new ChanceAction("Exemption from taxes for one round", player =>
            {
                player.taxFreeRound = true;
                player.overmoving = true;
            }),
            new ChanceAction("Postal order 1500", player =>
            {
                player.balanceOperation(1500);
                player.overmoving = true;
            }),
            new ChanceAction("Banking error - not in your favor -1000", player =>
            {
                int sum = -1000;
                if (player.CheckBalance(sum))
                {

                    player.balanceOperation(sum);
                    player.overmoving = true;
                }
                else
                    player.needPay(-sum);
            }),
            new ChanceAction("Pay for mobile services connection 500", player =>
            {
               int sum = -500;
                if (player.CheckBalance(sum))
                {

                    player.balanceOperation(sum);
                    player.overmoving = true;
                }
                else
                    player.needPay(-sum);
            }),
            new ChanceAction("You are lucky!\nYou have found a treasure of 1500", player =>
            {
                player.balanceOperation(1500);
                player.overmoving = true;
            }),
            new ChanceAction("A fine of 2000", player =>
            {
               int sum = -2000;
                if (player.CheckBalance(sum))
                {

                    player.balanceOperation(sum);
                    player.overmoving = true;
                }
                else
                    player.needPay(-sum);
            }),
            new ChanceAction("Go to the tax office", player =>
            {
                player.thisMoving.moveTo(18);
            }),
            new ChanceAction("Go 3 sectors forward", player =>
            {
                player.thisMoving.step(3);
            }),
            new ChanceAction("Go to the 'start' sector", player =>
            {
                player.thisMoving.moveTo(0);
            }),
            new ChanceAction("Skip one turn", player =>
            {
                player.thisMoving.skips++;
                player.overmoving = true;
            }),
            new ChanceAction("Legacy 5000", player =>
            {
                player.balanceOperation(5000);
                player.overmoving = true;
            }),
            new ChanceAction("Go 4 sectors forward", player =>
            {
                player.thisMoving.step(4);
            }),
            new ChanceAction("Bank error in your favor +1000", player =>
            {
                player.balanceOperation(1000);
                player.overmoving = true;
            }),
            new ChanceAction("Fire inspection fine 1500", player =>
            {
               int sum = -1500;
                if (player.CheckBalance(sum))
                {

                    player.balanceOperation(sum);
                    player.overmoving = true;
                }
                else
                    player.needPay(-sum);
            }),
            new ChanceAction("You are lucky, you found a treasure of 1500", player =>
            {
                player.balanceOperation(1500);
                player.overmoving = true;
            }),
            new ChanceAction("Repair your branches (200 each)", player =>
            {
                int repairCost = player.philies.Count * 200;

                if (player.CheckBalance(-repairCost))
                {
                    player.balanceOperation(-repairCost);
                    player.overmoving = true;
                }
                else
                    player.needPay(repairCost);
            }),
            new ChanceAction("Skip one move", player =>
            {
                player.thisMoving.skips++;
                player.overmoving = true;
            }),
            //new ChanceAction("Draw the next card", player => player.thisMoving.drawNextCard()),    
            // Добавьте остальные элементы массива с текстом и действиями шанса
        };


        public Chance(int nomer)
        {
            this.nomer = nomer;
        }
        public override void active(Player nowplayer)
        {
            //nowplayer.overmoving = true;
            ChanceAction randomAction = chanceActions[UnityEngine.Random.Range(0, chanceActions.Length)];
            if (nowplayer.isBot)
            {
                game.StartAction(randomAction, nowplayer);
                return;
            }
            game.chanceAnimator.chanceCtor(randomAction, nowplayer);
        }
    }

    public class Spetial : Field
    {
        private Action<Player> Method;
        public Spetial(int nomer, Action<Player> Method)
        {
            this.nomer = nomer;
            this.Method = Method;
        }
        public override void active(Player nowplayer)
        {
            Method(nowplayer);
        }
    }
}