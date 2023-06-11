using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class auctionDataUI : MonoBehaviour
{
    public GameObject window;
    public Text price;
    
    public void UpdateData(int _price)
    {
        window.SetActive(true);
        this.price.text = "Now price is " + _price;
    }
    public void cloth()
    {
        window.SetActive(false);
    }
}
