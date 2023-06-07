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
    public Text[] rent;
    //public Text rent1;
    //public Text rent2;
    //public Text rent3;

    public void show(Philia philia)
    {
        description.text = philia.description;
        price.text = "price " + philia.price;
        for (int i = 0; i > rent.Length; i++)
        {
            rent[i].text = philia.rent[i].ToString();
        }
        thisObg.SetActive(true);
    }
}
