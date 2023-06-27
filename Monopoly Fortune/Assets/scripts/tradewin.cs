using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static gameController;


public class tradewin : MonoBehaviour
{
    private int giveMoney = 0;
    private int takeMoney = 0;
    private List<Philia> givePh = new List<Philia>();
    private List<Philia> takePh = new List<Philia>();

    public GameObject step1;
    public GameObject step2;

    public InputField GetInput1;
    public InputField GetInput2;

    public Scrollbar Scrollbar1;
    public Scrollbar Scrollbar2;


    playerMoving trader1;
    playerMoving trader2;
    public GameObject givePerfab;
    public GameObject takePerfab;

    public GameObject giveParent;
    public GameObject takeParent;

    void Start()
    {
        GetInput1.onValueChanged.AddListener(ValidateInput1);
        GetInput2.onValueChanged.AddListener(ValidateInput2);
    }
    public void ValidateInput1(string value)
    {
        int minValue = 0;
        int maxValue = trader1.player.Balance;
        int parsedValue;

        if (int.TryParse(value, out parsedValue))
        {
            if (parsedValue < minValue)
            {
                parsedValue = minValue;
            }
            else if (parsedValue > maxValue)
            {
                parsedValue = maxValue;
            }

            GetInput1.text = parsedValue.ToString();
        }
    }
    public void ValidateInput2(string value)
    {
        int minValue = 0;
        int maxValue = trader2.player.Balance;
        int parsedValue;

        if (int.TryParse(value, out parsedValue))
        {
            if (parsedValue < minValue)
            {
                parsedValue = minValue;
            }
            else if (parsedValue > maxValue)
            {
                parsedValue = maxValue;
            }

            GetInput2.text = parsedValue.ToString();
        }
    }

    public void tradeWith(playerMoving tr1, playerMoving tr2)
    {

        step1.SetActive(false);
        trader1 = tr1;
        trader2 = tr2;
        foreach (Transform child in giveParent.transform)
            Destroy(child.gameObject); 
        foreach (Transform child in takeParent.transform)
            Destroy(child.gameObject);

        for (int i = 0; i < trader1.player.philies.Count; i++)
        {
            GameObject cloneObject = Instantiate(givePerfab);

            Button button = cloneObject.GetComponent<Button>();
            int j = i;
            button.onClick.AddListener(() =>
            {
                cloneObject.GetComponent<forBatton>().swap();
                if (givePh.Contains(trader1.player.philies[j]))
                {
                    givePh.Remove(trader1.player.philies[j]);
                }
                else
                {
                    givePh.Add(trader1.player.philies[j]);
                }
            });

            Image image = cloneObject.GetComponent<Image>();
            image.sprite = trader1.player.philies[i].logotipies;


            cloneObject.transform.SetParent(giveParent.transform);
            cloneObject.SetActive(true);
        }
        for (int i = 0; i < trader2.player.philies.Count; i++)
        {
            GameObject cloneObject = Instantiate(takePerfab);

            Button button = cloneObject.GetComponent<Button>();
            int j = i;
            button.onClick.AddListener(() =>
            {
                cloneObject.GetComponent<forBatton>().swap();
                if (takePh.Contains(trader2.player.philies[j]))
                {
                    takePh.Remove(trader2.player.philies[j]);
                }
                else
                {
                    takePh.Add(trader2.player.philies[j]);
                }
            });

            Image image = cloneObject.GetComponent<Image>();
            image.sprite = trader2.player.philies[i].logotipies;


            cloneObject.transform.SetParent(takeParent.transform);
            cloneObject.SetActive(true);
        }

        Scrollbar1.value = 1;
        Scrollbar2.value = 1;
        step2.SetActive(true);
    }

    public void AddGive(int money)
    {
        giveMoney = money;
    }
    public void Addtake(int money)
    {
        takeMoney = money;
    }
    public void tradeReady()
    {
        if (trader2.player.isBot)
            if (trader2.player.tradeofferBot(givePh, takePh, giveMoney, takeMoney))
            {
                GoTrade();
            }

    }
    private void GoTrade()
    {
        giveMoney = GetInput1.text.Length == 0 ? 0 : Convert.ToInt32(GetInput1.text);
        takeMoney = GetInput2.text.Length == 0 ? 0 : Convert.ToInt32(GetInput2.text);
        int sum = giveMoney - takeMoney;
        trader1.player.balanceOperation(-sum);
        trader2.player.balanceOperation(sum);

        for (int i = 0; i < givePh.Count; i++)
            givePh[i].swichOwner(trader2.player);

        for (int i = 0; i < takePh.Count; i++)
            takePh[i].swichOwner(trader1.player);

        givePh = new List<Philia>();
        takePh = new List<Philia>();

    }
    
}
