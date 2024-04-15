using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToRuiSECont : MonoBehaviour
{
    public AudioSource walk;

    void WalkSe()
    {
        if(!walk.isPlaying)
        {
            walk.pitch = Random.Range(0.7f, 1.2f);
            walk.Play();
        }
    }

    void StopSe()
    {
        walk.Stop();
    }
}
