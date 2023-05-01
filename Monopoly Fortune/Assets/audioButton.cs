using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class audioButton : MonoBehaviour
{
    public AudioSource audiosrc;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        //audiosrc.volume = 0.1f;
    }
    void TaskOnClick()
    {
        audiosrc.volume = audiosrc.volume == 1f ? 0f : audiosrc.volume += 0.1f;
        button.GetComponentInChildren<Text>().text = (int)(audiosrc.volume * 100f) + "%";
    }
}
