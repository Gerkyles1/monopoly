using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forBatton : MonoBehaviour
{
    public GameObject strela;
    private bool act = false;
    // Start is called before the first frame update
    public void swap()
    {
        strela.SetActive(!act);
        act = !act;
    }
}
