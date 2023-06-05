using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public playerMoving[] playerScripts;
    public cub cub;
    private playerMoving nowplayer;
    private int nowPlayerIndex=0;

    // Start is called before the first frame update
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
        nowPlayerIndex = (nowPlayerIndex + 1) % 4;
    }
}
