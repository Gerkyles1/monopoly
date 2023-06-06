using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public playerMoving[] playerScripts;
    public cub cub;
    private playerMoving nowplayer;
    private int nowPlayerIndex=0;

    void Start()
    {
        
    }
    public void meinMethod()
    {
        m:
        nowplayer = playerScripts[nowPlayerIndex];
        if (nowplayer.skips == 0)
            cub.Throw(nowplayer);
        else
        {
            nowplayer.skips--;
            nowPlayerIndex = (nowPlayerIndex + 1) % 4;
            goto m;
        }
        //nowplayer.Field;
        nowPlayerIndex = (nowPlayerIndex + 1) % 4;

    }
    public abstract class Field
    {
        public abstract void active(playerMoving nowplayer);
    }
}
