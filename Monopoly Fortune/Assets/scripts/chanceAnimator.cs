using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static playerMoving;
using static gameController;
using static gameController.Chance;

public class chanceAnimator : MonoBehaviour
{
    public GameObject textChance;
    public ChanceAction chanceAction;
    public Sprite otherSideChance;
    private Player player;
    public void chanceCtor(ChanceAction chanceAction, Player nowplayer)
    {

        this.chanceAction = chanceAction;
        this.player = nowplayer;
        this.textChance.GetComponent<Text>().text = chanceAction.text;

        gameObject.SetActive(true);
    }
    public void flip()
    {
        this.textChance.GetComponent<Text>().text = chanceAction.text;
        this.textChance.SetActive(true);
        GetComponent<Image>().sprite = null;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GetComponent<Animation>().Play("chanceEnd");
        });
    }
    public void onclic()
    {
        GetComponent<Animation>().Play("flip");
    }
    public void off()
    {
        textChance.SetActive(false);
        GetComponent<Image>().sprite = otherSideChance;
        gameObject.SetActive(false);
        GetComponent<Button>().onClick.AddListener(() => onclic());
        Chance.game.StartAction(chanceAction, player);
    }
}
