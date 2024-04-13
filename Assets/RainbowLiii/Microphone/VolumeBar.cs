using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeBar : MonoBehaviour
{
    public Image bar;
    public GameObject listenner;
    private MicrophoneInput Microphone;
    private float Maxvolume = 100f;
    // Start is called before the first frame update
    void Start()
    {
        Microphone = listenner.GetComponent<MicrophoneInput>();
        bar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = Microphone.realVolume / Maxvolume;
    }
}
