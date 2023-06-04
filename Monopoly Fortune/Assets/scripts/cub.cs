using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class cub : MonoBehaviour
{

    public Rigidbody _rigidbody;
    public Transform _transform;
    private playerMoving playerScrt;
    public bool isThrowing;
    void Start()
    {
        //_transform = transform;
        //_rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        _transform.position = new Vector3(-30f, 15f, -15f);
        gameObject.SetActive(false);
    }
    public void Throw(playerMoving player)
    {
        playerScrt = player;

        gameObject.SetActive(true);

        _rigidbody.isKinematic = false;

        _transform.rotation = Quaternion.LookRotation(UnityEngine.Random.insideUnitSphere);

        StartCoroutine(WaitForSleep());

    }

    private IEnumerator WaitForSleep()
    {
        yield return new WaitUntil(() => _rigidbody.IsSleeping());


        if (Vector3.Dot(transform.up, Vector3.up) > 0.8f)
        {
            playerScrt.step(1);
        }
        else if (Vector3.Dot(-transform.right, Vector3.up) > 0.8f)
        {
            playerScrt.step(2);
        }
        else if (Vector3.Dot(-transform.forward, Vector3.up) > 0.8f)
        {
            playerScrt.step(3);
        }
        else if (Vector3.Dot(transform.forward, Vector3.up) > 0.8f)
        {
            playerScrt.step(4);
        }
        else if (Vector3.Dot(transform.right, Vector3.up) > 0.8f)
        {
            playerScrt.step(5);
        }
        else if (Vector3.Dot(-transform.up, Vector3.up) > 0.8f)
        {
            playerScrt.step(6);
        }
        else
        {
            Throw(playerScrt);
            yield break;
        }

        _rigidbody.isKinematic = true;
        _transform.position = new Vector3(-30f, 15f, -15f);
        gameObject.SetActive(false);
    }
 
}
