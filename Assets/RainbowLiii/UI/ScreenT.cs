using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenT : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(Screen.width, (int)(Screen.width / 1920f * 1080), true);
    }
}
