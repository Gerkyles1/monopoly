using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static playerMoving;

public class sellMenu : MonoBehaviour
{
    public GameObject perfab;
    public GameObject This;
    public Transform parent;
    public Material nullcolor;
    public void dataForSell(playerMoving playerm)
    {
        Player player = playerm.player;
        if (player.philies.Count <= 0)
            return;

        foreach (Transform child in parent.transform)
            Destroy(child.gameObject); // Используйте Destroy(), если хотите удалить объекты во время выполнения игры
        

        for (int i = 0; i < player.philies.Count; i++)
        {
            GameObject cloneObject = Instantiate(perfab);

            Button button = cloneObject.GetComponentInChildren<Button>();
            int newprice = (int)(player.philies[i].price - (((float)player.philies[i].price) * 0.1));
            button.GetComponentInChildren<Text>().text = Convert.ToString(newprice);
            int j = i;
            button.onClick.AddListener(() =>
            {
                player.philies[j].ownerPlane.material = nullcolor;
                player.philies[j].owner = null;

                player.philies.RemoveAt(j);
                player.balanceOperation(newprice);
                This.SetActive(false);

            });

            Image image = cloneObject.GetComponentInChildren<Image>();
            image.sprite = player.philies[i].logotipies; 

            
            cloneObject.transform.SetParent(parent);
            cloneObject.SetActive(true);


        }
        This.SetActive(true);
    }
    
}
