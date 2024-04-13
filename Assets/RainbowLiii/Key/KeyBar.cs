using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBar : MonoBehaviour
{
    public Image bar;
    public GameObject Keyboardlistenner;
    private KeyboradListen KL;
    //private float Maxvolume = 3f;
    // Start is called before the first frame update
    void Start()
    {
        KL = Keyboardlistenner.GetComponent<KeyboradListen>();
        bar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = KL.escapTime/KL.MaxTime;
    }
}
