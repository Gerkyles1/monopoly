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
    //public Text[] rent;
    public Text rent1;
    public Text rent2;
    public Text rent3;

    public void show(Philia philia)
    {
        description.text = philia.description;
        price.text = "price " + philia.price;
        rent1.text = "" + philia.rent[0];
        rent2.text = "" + philia.rent[1];
        rent3.text = "" + philia.rent[2];

        thisObg.SetActive(true);
    }
}
