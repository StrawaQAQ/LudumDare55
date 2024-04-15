using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonVFX : MonoBehaviour
{
    public GameObject VFX;

    public void PlayVFX()
    {
        Instantiate(VFX,transform.position, transform.rotation);
    }
}
