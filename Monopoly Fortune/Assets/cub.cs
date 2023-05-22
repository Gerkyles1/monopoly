using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class cub : MonoBehaviour
{

    private Rigidbody _rigidbody;
    private MeshRenderer meshRenderer;
    private Transform _transform;
    public bool isThrowing;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        _rigidbody.isKinematic = true;
        _transform.position = new Vector3(-30f, 15f, -15f);
        gameObject.SetActive(false);
    }
    public void Throw()
    {
        gameObject.SetActive(true);

        text.text = "0";
        _rigidbody.isKinematic = false;

       // _transform.position = new Vector3(-30f, 30f, -15f);
        _transform.rotation = Quaternion.LookRotation(UnityEngine.Random.insideUnitSphere);

        StartCoroutine(WaitForSleep());
    }

    private IEnumerator WaitForSleep()
    {
        yield return new WaitUntil(() => _rigidbody.IsSleeping());

        if (Vector3.Dot(transform.forward, Vector3.up) > 0.8f)
        {
            text.text = "4";//4
        }
        else if (Vector3.Dot(-transform.forward, Vector3.up) > 0.8f)
        {
            text.text = "3";//3

        }
        else if (Vector3.Dot(transform.right, Vector3.up) > 0.8f)
        {
            text.text = "5";//5

        }
        else if (Vector3.Dot(-transform.right, Vector3.up) > 0.8f)
        {
            text.text = "2";//2

        }
        else if (Vector3.Dot(transform.up, Vector3.up) > 0.8f)
        {
            text.text = "1";//1

        }
        else if (Vector3.Dot(-transform.up, Vector3.up) > 0.8f)
        {
            text.text = "6";//6

        }
        else 
        {
            Throw();
            yield break;
        }

        _rigidbody.isKinematic = true;
        _transform.position = new Vector3(-30f, 15f, -15f);
        gameObject.SetActive(false);



    }
 
}
