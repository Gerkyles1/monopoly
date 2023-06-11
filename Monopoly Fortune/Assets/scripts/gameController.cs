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
    private int nowPlayerIndex=0;
    public companyDetalis detalis;
    public static Field[] map;
    public Text balance;

    void Start()
    {
        map = new Field[]
        {
            new Chance(0),//start
            new balanceOper(1, 1500),
            new Philia(2, "Tefaty", 500, new int[]{150, 300, 600},new int[]{ 3, 4}, "sdfsef1"),
            new Philia(3, "Zuins", 700, new int[]{200, 350, 700},new int[]{ 2, 4}, "sdfsef2"),
            new Philia(4, "PlusTech", 900, new int[]{250, 400, 800},new int[]{ 2, 3}, "sdfsef3"),
            new Chance(5),
            new balanceOper(6, -500),
            new Philia(4, "OgMa", 1000, new int[]{250, 500, 1000},new int[]{ 2, 3}, "sdfsef3"),
            new Philia(4, "ZeHipe", 900, new int[]{250, 500, 1000},new int[]{ 2, 3}, "sdfsef3"),
            new Philia(4, "Rotartime", 900, new int[]{300, 600, 1200},new int[]{ 2, 3}, "sdfsef3"),
            new balanceOper(6, 2000),

        };
        Philia.detalis = detalis;
    }
    public void meinMethod()
    {
        nowplayer = playerScripts[nowPlayerIndex];

        while (nowplayer.skips > 0)
        {
            nowplayer.skips--;
            nowPlayerIndex = (nowPlayerIndex + 1) % 4;
        }
        cub.Throw(nowplayer);


    }
    public void playerEndMoving()
    {
        map[nowplayer.Field].active(nowplayer.player);
        nowPlayerIndex = (nowPlayerIndex + 1) % 4;
    }
    public void buyPh()
    {
        nowplayer.player.needPay(((Philia)map[nowplayer.Field]).price);
        ((Philia)map[nowplayer.Field]).swichOwner(nowplayer.player);
    }








    //*********************************************chat gpt





    public auctionDataUI auctionData;

    private List<Player> auctionPlayers; // Список игроков, участвующих в аукционе
    private int currentBid; // Текущая цена на аукционе
    private int currentPlayerIndex; // Индекс текущего игрока в списке auctionPlayers

    public void auction()
    {
        auctionPlayers = new List<Player>();

        currentBid = ((Philia)map[nowplayer.Field]).price; // Начальная цена аукциона
                                                           // Добавьте всех игроков, кроме nowplayer, в список auctionPlayers
        foreach (playerMoving i in playerScripts)
        {
            if (i != nowplayer && i.player.CheckBalance(currentBid))
            {
                auctionPlayers.Add(i.player);
            }
        }

        currentPlayerIndex = 0; // Индекс текущего игрока

        StartAuction();
    }

    private void StartAuction()
    {
        
        while (auctionPlayers.Count > 1)
        {
            if (currentPlayerIndex >= auctionPlayers.Count)
            {
                currentPlayerIndex = 0; // Возвращаемся к первому игроку
            }

            Player currentPlayer = auctionPlayers[currentPlayerIndex];
            if (!currentPlayer.CheckBalance(currentBid + 20))
            {
                auctionPlayers.RemoveAt(currentPlayerIndex); // Удаляем текущего игрока с аукциона
                continue; // Пропускаем остаток кода в текущей итерации цикла
            }

            if (currentPlayer.isBot)
            {
                // Реализация аукциона для бота
                // ...
            }
            else
            {
                auctionData.UpdateData(currentBid); // Активируйте часть канваса с кнопками "Да" и "Нет"
                                                    // Ожидайте нажатия кнопки
                return; // Выходим из метода и продолжим выполнение после получения ответа от игрока
            }

            currentPlayerIndex++; // Переходим к следующему игроку
        }

        // Остался только один игрок на аукционе
        auctionData.cloth();
        Player lastAuctioneer = auctionPlayers[0];
        int lastBidPrice = currentBid;

        lastAuctioneer.needPay(lastBidPrice);
        ((Philia)map[nowplayer.Field]).swichOwner(lastAuctioneer);

    }

    // Метод, вызываемый при нажатии кнопки "Да"
    public void OnYesButtonPressed()
    {
        currentBid += 20; // Повышаем текущую цену на 20
        auctionData.UpdateData(currentBid);

        currentPlayerIndex++; // Переходим к следующему игроку

        StartAuction(); // Запускаем аукцион для следующего игрока
    }

    // Метод, вызываемый при нажатии кнопки "Нет"
    public void OnNoButtonPressed()
    {

        //auctionPlayers.RemoveAt(currentPlayerIndex); // Удаляем текущего игрока с аукциона
        if (currentPlayerIndex < auctionPlayers.Count)
        {
            auctionPlayers.RemoveAt(currentPlayerIndex); // Удаляем текущего игрока с аукциона
        }
        auctionData.UpdateData(currentBid);

        StartAuction(); // Запускаем аукцион для следующего игрока
    }








    //*********************************************chat gpt










    public void Update()
    {
        balance.text = playerScripts[0].player.Balance.ToString();
    }

    public abstract class Field
    {
        public int nomer;
        public gameController game;
        public abstract void active(Player nowplayer);
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

        public Philia(int nomer, string name, int price, int[] rent, int[] colabs, string description)
        {
            this.name = name;
            this.nomer = nomer;
            this.price = price;
            this.rent = rent;
            this.colabs = colabs;
            this.description = description;
        }

        public override void active(Player nowplayer)
        {
            if (owner != null)
            {
                nowplayer.needPay(getRent());
            }
            else if (!nowplayer.isBot)
            {
                detalis.show(this);
            }
            else if (nowplayer.isBot)
            {
                if (nowplayer.CheckBalance(this.price))
                {
                    game.buyPh();
                }
                else
                {

                }
            }
        }

        private int getRent()
        {
            if (this.isMortgaged)
                return 0;
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
            if (owner != null)
                owner.philies.Remove(this);

            owner = newOwner;
            owner.philies.Add(this);
        }

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
            if (sum >= 0)
                nowplayer.balanceOperation(sum);
            else
                nowplayer.needPay(sum);
        }
    }

    public class Chance : Field
    {
        public Chance(int nomer)
        {
            this.nomer = nomer;
        }
        public override void active(Player nowplayer)
        {

        }
    }

}
