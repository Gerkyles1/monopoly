using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static playerMoving;
using System;
using UnityEngine.UI;
using static gameController;



public class WheelOfFortune : MonoBehaviour
{

    public GameObject casino;
    public GameObject pointer;
    public GameObject wheel;
    public Text Text;

    private float targetRotation; // Целевое значение поворота колеса
    private float currentRotation; // Текущий поворот колеса
    private float rotationSpeed = 1000f; // Скорость вращения колеса
    private bool isSpinning = false; // Флаг, указывающий, вращается ли колесо
    private int[] resultRotation = new int[] { 355, 120, 160, 200, 250 };
    private float deceleration;

    private playerMoving who;


    int countR = 10;
    int COUNTR = 10;
    System.Random random = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        currentRotation = wheel.transform.rotation.z;
    }
    public void spin(playerMoving nowplayer)
    {
        casino.SetActive(true);
        who = nowplayer;
        int result = random.Next(1, 9);
        result = result <= 4 ? 0 : result - 4;
        targetRotation = resultRotation[result];

        if (nowplayer.player.isBot)
        {
            ready((int)targetRotation);
            return;
        }

        countR = random.Next(10, 13);
        COUNTR = countR;
        rotationSpeed = 1000f;
        deceleration = 1000f - targetRotation;
        currentRotation = wheel.transform.rotation.z;
        isSpinning = true;
    }
    private void Update()
    {
        if (isSpinning)
        {
            if (countR >= 0)
            {
                currentRotation += rotationSpeed * Time.deltaTime;
                wheel.transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
                if (currentRotation >= 360)
                {
                    countR--;
                    currentRotation %= 360;
                    rotationSpeed -= (deceleration / COUNTR);
                }
                Text.text = rotationSpeed.ToString();
            }

            else if (!(countR >= 0))
            {
                currentRotation += rotationSpeed * Time.deltaTime;
                wheel.transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
                if (currentRotation >= 360)
                {
                    currentRotation %= 360;
                }

                float difference = targetRotation - currentRotation > 0 ? targetRotation - currentRotation : targetRotation + 360 - currentRotation;

                if (difference < 5)
                {
                    isSpinning = false;
                    ready((int)targetRotation);
                    casino.SetActive(false);

                    return;
                }

                rotationSpeed = difference - 1;


                Text.text = rotationSpeed.ToString();
            }
        }
    }
    private void ready(int angle)
    {
        switch (angle)
        {
            case 355:
                who.step(7);
                break;
            case 120:
                map[5].active(who.player);
                //?
                break;
            case 160:
                who.skips++;
                who.player.overmoving = true;
                break;
            case 200:
                who.player.balanceOperation(5000);
                who.player.overmoving = true;
                break;
            case 250:
                who.step(18);
                break;
        }
        casino.SetActive(false);
        return;
    }
}