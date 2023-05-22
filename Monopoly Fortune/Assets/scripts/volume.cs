using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volume : MonoBehaviour
{
    public AudioSource audiosrc;
    public float volum = 1f;
    public GameObject imgmute;

    void Start()
    {
        audiosrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        audiosrc.volume = volum;
        imgmute.SetActive(volum == 0);
    }
    public void SetVolum(float vol)
    {
        volum = vol;
    }
        
}
