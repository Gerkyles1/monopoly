using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static gameController;


public class companyDetalis : MonoBehaviour
{
    public GameObject thisObg;
    public Text description;
    public Text price;
    public Text rent1;
    public Text rent2;
    public Text rent3;
    public Button buy;
    public Button auction;
    public Button back;
    public Image logo;


    public void show(Philia philia)
    {
        buy.gameObject.SetActive(true);
        auction.gameObject.SetActive(true);
        back.gameObject.SetActive(false);


        logo.sprite = philia.logotipies;
        description.text = philia.description;
        price.text = "price " + philia.price;
        rent1.text = "" + philia.rent[0];
        rent2.text = "" + philia.rent[1];
        rent3.text = "" + philia.rent[2];

        thisObg.SetActive(true);
    }
    public void show(int nomer)
    {
        Philia philia = ((Philia)gameController.map[nomer]);

        buy.gameObject.SetActive(false);
        auction.gameObject.SetActive(false);
        back.gameObject.SetActive(true);


        logo.sprite = philia.logotipies;
        description.text = philia.description;
        price.text = "price " + philia.price;
        rent1.text = "" + philia.rent[0];
        rent2.text = "" + philia.rent[1];
        rent3.text = "" + philia.rent[2];

        thisObg.SetActive(true);
    }
}
