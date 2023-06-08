using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public playerMoving[] playerScripts;
    public cub cub;
    private playerMoving nowplayer;
    private int nowPlayerIndex=0;
    public companyDetalis detalis;
    public static Field[] map;

    void Start()
    {
        map = new Field[]
        {
            new Chance(0),
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
        map[nowplayer.Field].active(nowplayer);//     +
        nowPlayerIndex = (nowPlayerIndex + 1) % 4;
    }


    public abstract class Field
    {
        public int nomer;
        public abstract void active(playerMoving nowplayer);
    }

    public class Philia : Field
    {
        public static companyDetalis detalis;
        public playerMoving owner=null;
        public string name;
        public int price;
        public int[] rent;
        public int[] colabs;
        public string description;
        public Philia(int nomer, string name, int price, int[] rent, int[] colabs, string description)
        {
            this.name = name;
            this.nomer = nomer;
            this.price = price;
            this.rent = rent;
            this.colabs = colabs;
            this.description = description;
        }

        public override void active(playerMoving nowplayer)
        {
            if (!nowplayer.isBot /* && owner == null */)
            {
                detalis.show(this);
            }
            else if (owner != null)
            {
                nowplayer.needPay(getRent());
            }
        }

        private int getRent()
        {
            int count = 0;

            foreach (Philia philia in owner.philies)
            {
                foreach (int colab in this.colabs)
                {
                    if (philia.nomer == colab)
                    {
                        count++;
                        break;
                    }
                }
            }

            count = count > 2 ? 2 : count;

            return this.rent[count];
        }

        public void Buy(playerMoving buyer)
        {
            if (owner == null)
            {
                if (buyer.CheckBalance(-price))
                {
                    owner = buyer;
                    buyer.balanceOperation(-price);

                    buyer.philies.Add(this);

                    // Дополнительные действия, связанные с покупкой филии
                }
                else
                {
                    // Недостаточно средств для покупки филии
                }
            }
            else
            {
                // Филия уже имеет владельца
            }
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
        public override void active(playerMoving nowplayer)
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
        public override void active(playerMoving nowplayer)
        {

        }
    }
}
